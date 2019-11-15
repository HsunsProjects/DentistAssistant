using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentistAssistant.Extensions;
using DentistAssistant.Models;
using DentistAssistant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentistAssistant.Controllers
{
    public class FirstTimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateFirstTime(string id)
        {
            using (var def = new DoctorContext())
            {
                var patient = def.Patients.Where(c => c.PatNo.ToUpper().Equals(id.ToUpper())).FirstOrDefault();
                DateTime createTime = DateTime.Now;
                CreateFirstTimeViewModel createFirstTimeViewModel = new CreateFirstTimeViewModel()
                {
                    Users = (from u in UsersInfo.Users
                             select new SelectListItem()
                             {
                                 Text = u.UserName,
                                 Value = u.UserNo,
                                 Selected = false
                             }).ToList(),
                    PatientNo = patient.PatNo,
                    FirstTime = createTime
                };
                return View(createFirstTimeViewModel);
            }
        }

        [HttpPost]
        public IActionResult CreateFirstTime(CreateFirstTimeViewModel createFirstTimeViewModel)
        {
            using (var daef = new DentistAssistantContext())
            {
                var ps = daef.PatientSettings.Find(createFirstTimeViewModel.PatientNo);
                if (ps == null)
                {
                    //var patientSettings = daef.PatientSettings.Find(createFirstTimeViewModel.PatientId);
                    PatientSettings patientSettings = new PatientSettings();
                    patientSettings.Id = createFirstTimeViewModel.PatientNo;
                    patientSettings.FirstTimeTime = createFirstTimeViewModel.FirstTime;

                    createFirstTimeViewModel.patientRecord.IsFirst = true;
                    createFirstTimeViewModel.patientRecord.IsSuggest = false;
                    createFirstTimeViewModel.patientRecord.CreateTime = createFirstTimeViewModel.FirstTime;
                    patientSettings.PatientRecords.Add(createFirstTimeViewModel.patientRecord);
                    daef.PatientSettings.Add(patientSettings);
                }
                else
                {
                    PatientRecords pr = new PatientRecords();
                    pr.IsFirst = true;
                    pr.IsSuggest = false;
                    pr.CreateTime = createFirstTimeViewModel.FirstTime;
                    pr.UserNo = createFirstTimeViewModel.patientRecord.UserNo;
                    pr.PatientSettingId = createFirstTimeViewModel.PatientNo;
                    pr.Room = createFirstTimeViewModel.patientRecord.Room;
                    ps.FirstTimeTime = createFirstTimeViewModel.FirstTime;
                    ps.PatientRecords.Add(pr);
                    //daef.PatientRecords.Add(pr);
                }
                daef.SaveChanges();
                return RedirectToAction("Record", "Patient", new { id = createFirstTimeViewModel.PatientNo });
            }
        }

        [HttpGet]
        public IActionResult EditFirstTime(string patId)
        {
            using (var def = new DoctorContext())
            {
                var users = UsersInfo.Users;
                using (var daef = new DentistAssistantContext())
                {
                    var patientSettingFirstTimeRecord = (from ps in daef.PatientSettings
                                                         where ps.Id.ToUpper().Equals(patId.ToUpper())
                                                         select new PatientSettingRecordViewModel()
                                                         {
                                                             PatientSetting = ps,
                                                             PatientRecordUnit = (from pr in ps.PatientRecords
                                                                                  where pr.IsFirst.Equals(true)
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
                                                                                  }).FirstOrDefault()
                                                         }).FirstOrDefault();
                    return View(patientSettingFirstTimeRecord);
                }
            }
        }


        [HttpPost]
        public JsonResult UpdatePatientRecords(int id, string timeType, DateTime dateTime)
        {
            using (var daef = new DentistAssistantContext())
            {
                var patientRecord = daef.PatientRecords.Find(id);
                try
                {
                    if (patientRecord != null)
                    {
                        switch (timeType)
                        {
                            case "1":
                                patientRecord.OrderTime = dateTime;
                                break;
                            case "2":
                                patientRecord.ArriveTime = dateTime;
                                break;
                            case "3":
                                patientRecord.DrArriveTime = dateTime;
                                break;
                            case "4":
                                patientRecord.DrLeaveTime = dateTime;
                                break;
                            case "5":
                                patientRecord.PtLeaveTime = dateTime;
                                break;
                        }
                        daef.SaveChanges();
                        var jsonResult = new
                        {
                            status = true
                        };
                        return Json(jsonResult);
                    }
                    var jsonResultNull = new
                    {
                        status = false,
                        message = "尚未建立初診詢問單"
                    };
                    return Json(jsonResultNull);
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