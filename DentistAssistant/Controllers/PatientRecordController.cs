using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentistAssistant.Models;
using DentistAssistant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentistAssistant.Controllers
{
    public class PatientRecordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePatientRecord(string id)
        {
            using (var def = new DoctorContext())
            {
                var patient = def.Patients.Where(c => c.Id.ToUpper().Equals(id.ToUpper())).FirstOrDefault();
                CreatePatientRecordViewModel createPatientRecordViewModel = new CreatePatientRecordViewModel()
                {
                    Users = (from u in def.Users
                             select new SelectListItem()
                             {
                                 Text = u.UserName,
                                 Value = u.UserNo,
                                 Selected = false
                             }).ToList(),
                    PatientId = patient.Id
                };
                return View(createPatientRecordViewModel);
            }
        }


        [HttpPost]
        public IActionResult CreatePatientRecord(CreatePatientRecordViewModel createPatientRecordViewModel)
        {
            using (var def = new DoctorContext())
            {
                using (var daef = new DentistAssistantContext())
                {
                    DateTime createTime = DateTime.Now;
                    var patientSettings = daef.PatientSettings.Find(createPatientRecordViewModel.PatientId);
                    createPatientRecordViewModel.patientRecord.CreateTime = createTime;
                    patientSettings.PatientRecords.Add(createPatientRecordViewModel.patientRecord);
                    daef.SaveChanges();
                    return RedirectToAction("Assistant", "Patient", new { id = createPatientRecordViewModel.PatientId });
                }
            }
        }

        [HttpGet]
        public IActionResult EditPatientRecord(string patientId, int patientRecordId)
        {
            using (var def = new DoctorContext())
            {
                var users = def.Users.ToList();
                using (var daef = new DentistAssistantContext())
                {
                    PatientRecordUnit patientRecordUnit = (from pr in daef.PatientRecords
                                                           where pr.Id.Equals(patientRecordId)
                                                           select new PatientRecordUnit()
                                                           {
                                                               Id = pr.Id,
                                                               Room = pr.Room,
                                                               UserNo = pr.UserNo,
                                                               UserName = users.Where(c => c.UserNo.Equals(pr.UserNo)).FirstOrDefault().UserName,
                                                               OrderTime = pr.OrderTime,
                                                               ArriveTime = pr.ArriveTime,
                                                               DrArriveTime = pr.DrArriveTime,
                                                               DrLeaveTime = pr.DrLeaveTime,
                                                               PtLeaveTime = pr.PtLeaveTime,
                                                               IsFirst = pr.IsFirst,
                                                               CreateTime = pr.CreateTime,
                                                               PatientSettingId = pr.PatientSettingId,
                                                               FdiUnitsF = (from f in pr.Fdis
                                                                            where f.Type.Equals("F")
                                                                            orderby f.CreateDate descending
                                                                            select new FdiUnit()
                                                                            {
                                                                                Fdi = f,
                                                                                FdiDetails = f.FdiDetails.ToList()
                                                                            }).ToList(),
                                                               FdiUnitsN = (from f in pr.Fdis
                                                                            where f.Type.Equals("N")
                                                                            orderby f.CreateDate descending
                                                                            select new FdiUnit()
                                                                            {
                                                                                Fdi = f,
                                                                                FdiDetails = f.FdiDetails.ToList()
                                                                            }).ToList(),
                                                               RecordUserUnit = (from ru in pr.RecordUsers
                                                                                 orderby ru.CreateDate descending
                                                                                 select new RecordUserUnit()
                                                                                 {
                                                                                     Id = ru.Id,
                                                                                     UserNo = ru.UserNo,
                                                                                     UserName = users.Where(c => c.UserNo.Equals(ru.UserNo)).FirstOrDefault().UserName,
                                                                                     CreateDate = ru.CreateDate,
                                                                                     PatientRecordId = ru.PatientRecordId
                                                                                 }).ToList()

                                                           }).FirstOrDefault();
                    ViewBag.PatientId = patientId;
                    return View(patientRecordUnit);
                }
            }
        }

        [HttpPost]
        public JsonResult RemovePatientRecord(int patientRecordId)
        {
            using (var daef = new DentistAssistantContext())
            {

                try
                {
                    //var patientRecord = daef.PatientRecords.Find(patientRecordId);

                    var patientRecord = daef.PatientRecords.Find(patientRecordId);

                    var fdi = (from f in daef.Fdis
                               where f.PatientRecordId.Equals(patientRecord.Id)
                               select f).ToList();

                    var fdiDetail = (from fd in daef.FdiDetails
                                     where fd.Fdi.PatientRecordId.Equals(patientRecord.Id)
                                     select fd).ToList();

                    var recordUsers = (from ru in daef.RecordUsers
                                       where ru.PatientRecordId.Equals(patientRecord.Id)
                                       select ru).ToList();

                    daef.RecordUsers.RemoveRange(recordUsers);
                    daef.FdiDetails.RemoveRange(fdiDetail);
                    daef.Fdis.RemoveRange(fdi);
                    daef.PatientRecords.Remove(patientRecord);
                    daef.SaveChanges();

                    var jsonResult = new
                    {
                        status = true
                    };
                    return Json(jsonResult);
                }
                catch
                {
                    var jsonResultError = new
                    {
                        status = false,
                        message = "系統發生問題"
                    };
                    return Json(jsonResultError);
                }
            }
        }
    }
}