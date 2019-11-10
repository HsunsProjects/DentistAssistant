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
    public class EditFirstTimeModalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FdiDescription(int patientRecordId, int fdiId, string type)
        {
            using (var daef = new DentistAssistantContext())
            {
                FdiDescriptionViewModel fdiDescriptionViewModel = new FdiDescriptionViewModel();
                fdiDescriptionViewModel.PatientRecordId = patientRecordId;
                fdiDescriptionViewModel.Type = type;

                if (!fdiId.Equals(0))
                {
                    fdiDescriptionViewModel.FdiUnit = (from f in daef.Fdis
                                                       where f.Id.Equals(fdiId)
                                                       select new FdiUnit()
                                                       {
                                                           Fdi = f,
                                                           FdiDetails = f.FdiDetails.ToList()
                                                       }).FirstOrDefault();
                }
                fdiDescriptionViewModel.PhraseGroups = (from pg in daef.PhraseGroups
                                                        select new SelectListItem()
                                                        {
                                                            Text = pg.Name,
                                                            Value = pg.Id.ToString(),
                                                            Selected = false
                                                        }).ToList();
                return View(fdiDescriptionViewModel);
            }
        }

        public IActionResult AssistantList(int patientRecordId)
        {
            using (var def = new DoctorContext())
            {
                AssistantListViewModel assistantListViewModel = new AssistantListViewModel()
                {
                    PatientRecordId = patientRecordId,
                    Users = (from u in UsersInfo.Users
                             select new SelectListItem()
                             {
                                 Text = u.UserName,
                                 Value = u.UserNo,
                                 Selected = false
                             }).ToList()
                };
                return View(assistantListViewModel);
            }
        }

        [HttpPost]
        public JsonResult GetPhrases(int phraseGroupId)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    var data = (from p in daef.Phrases
                                where p.PhraseGroupId.Equals(phraseGroupId)
                                select new
                                {
                                    id = p.Id,
                                    description = p.Description
                                }).ToList();

                    var jsonResult = new
                    {
                        status = true,
                        message = "", 
                        data = data
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

        [HttpPost]
        public JsonResult AddUpdateFdi(int patientRecordId, int fdiId, string type, bool fm, bool ub, bool ua, bool ur, bool ul,
             bool lb, bool la, bool lr, bool ll, bool indescribable,
            string fdiDescription, List<string> data)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    if (fdiId.Equals(0))
                    {
                        //insert
                        var patinetRecord = daef.PatientRecords.Find(patientRecordId);
                        Fdis fdis = new Fdis();
                        fdis.Fm = fm;
                        fdis.Ub = ub;
                        fdis.Ua = ua;
                        fdis.Ur = ur;
                        fdis.Ul = ul;
                        fdis.Lb = lb;
                        fdis.La = la;
                        fdis.Lr = lr;
                        fdis.Ll = ll;
                        fdis.Indescribable = indescribable;
                        fdis.Description = fdiDescription;
                        fdis.CreateDate = DateTime.Now;
                        fdis.Type = type;
                        if (data.Count > 0) 
                        {
                            fdis.FdiDetails = (from d in data
                                               select new FdiDetails()
                                               {
                                                   FdiArea = d.Substring(0, 1),
                                                   FdiPosition = d.Substring(1, 1)
                                               }).ToList();
                        }
                        patinetRecord.Fdis.Add(fdis);
                    }
                    else
                    {
                        //update
                        var updateFdi = daef.Fdis.Find(fdiId);
                        updateFdi.Fm = fm;
                        updateFdi.Ub = ub;
                        updateFdi.Ua = ua;
                        updateFdi.Ur = ur;
                        updateFdi.Ul = ul;
                        updateFdi.Lb = lb;
                        updateFdi.La = la;
                        updateFdi.Lr = lr;
                        updateFdi.Ll = ll;
                        updateFdi.Indescribable = indescribable;
                        updateFdi.Description = fdiDescription;
                        updateFdi.CreateDate = DateTime.Now;
                        updateFdi.Type = type;

                        var fdiDetail = from fd in daef.FdiDetails
                                        where fd.FdiId.Equals(fdiId)
                                        select fd;
                        daef.FdiDetails.RemoveRange(fdiDetail);

                        if (data.Count > 0)
                        {
                            foreach (string d in data)
                            {
                                updateFdi.FdiDetails.Add(new FdiDetails()
                                {
                                    FdiArea = d.Substring(0, 1),
                                    FdiPosition = d.Substring(1, 1)
                                });
                            }
                        }

                    }

                    daef.SaveChanges();

                    var jsonResult = new
                    {
                        status = true,
                        message = "",
                        data = new
                        {
                            //fdiId = fdis.Id
                        }
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

        [HttpPost]
        public JsonResult RemoveFdiDescription(int id)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    var fdi = daef.Fdis.Find(id);
                    var fdiDetail = from fd in daef.FdiDetails
                                    where fd.FdiId.Equals(id)
                                    select fd;
                    daef.FdiDetails.RemoveRange(fdiDetail);
                    daef.Fdis.Remove(fdi);
                    daef.SaveChanges();

                    var jsonResult = new
                    {
                        status = true,
                        message = ""
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

        /*
         * 
         *
         * 
         */


        [HttpPost]
        public JsonResult AddAssistant(int patientRecordId, string userNo)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    var patientRecord = daef.PatientRecords.Find(patientRecordId);
                    RecordUsers recordUsers = new RecordUsers()
                    {
                        UserNo = userNo,
                        CreateDate = DateTime.Now
                    };
                    patientRecord.RecordUsers.Add(recordUsers);

                    daef.SaveChanges();

                    var jsonResult = new
                    {
                        status = true,
                        message = "",
                        data = new
                        {
                            id = recordUsers.Id
                        }
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

        [HttpPost]
        public JsonResult RemoveAssistant(int id)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    var recordUser = daef.RecordUsers.Find(id);
                    daef.RecordUsers.Remove(recordUser);
                    daef.SaveChanges();

                    var jsonResult = new
                    {
                        status = true,
                        message = ""
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