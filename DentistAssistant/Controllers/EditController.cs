using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DentistAssistant.Extensions;
using DentistAssistant.Models;
using DentistAssistant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentistAssistant.Controllers
{
    public class EditController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult QA(string id)
        {
            using (var def = new DoctorContext())
            {
                var users = def.Users.ToList();
                using (var daef = new DentistAssistantContext())
                {
                    var patient = (from p in def.Patients
                                   where p.Id.ToLower().Equals(id.ToLower()) &&
                                         p.Enable.Equals(true)
                                   select p).FirstOrDefault();
                    var patientSetting = daef.PatientSettings.Find(patient.Id);
                    var patientRecord = new PatientRecordViewModel()
                    {
                        Patient = patient,
                        QACategorys = (from qac in daef.Qacategorys
                                       select new QACategorys()
                                       {
                                           Id = qac.Id,
                                           Title = qac.Title,
                                           SeqNo = qac.SeqNo,
                                           QAGroupUnits = (from qag in qac.Qagroups
                                                           select new QAGroupUnit()
                                                           {
                                                               Id = qag.Id,
                                                               Title = qag.Title,
                                                               SeqNo = qag.SeqNo,
                                                               QAQParentUnits = (from qaq in qag.Qaq
                                                                                 where qaq.ParentId.Equals(0)
                                                                                 select new QAQParentUnit()
                                                                                 {
                                                                                     QAQUnit = new QAQUnit()
                                                                                     {
                                                                                         IsChecked = daef.Qaa.Where(c => c.PatientId.Equals(id) && c.Qaqid.Equals(qaq.Id)).Count() > 0 ? true : false,
                                                                                         QAAValueDescription = daef.Qaa.Where(c => c.PatientId.Equals(id) && c.Qaqid.Equals(qaq.Id)).FirstOrDefault().ValueDescription,
                                                                                         Id = qaq.Id,
                                                                                         Type = qaq.Type,
                                                                                         Title = qaq.Title,
                                                                                         IsValue = qaq.IsValue,
                                                                                         ValueDescription = qaq.ValueDescription,
                                                                                         SeqNo = qaq.SeqNo
                                                                                     },
                                                                                     QAQUnits = (from qaqc in daef.Qaq
                                                                                                 where qaqc.ParentId.Equals(qaq.Id)
                                                                                                 select new QAQUnit()
                                                                                                 {
                                                                                                     IsChecked = daef.Qaa.Where(c => c.PatientId.Equals(id) && c.Qaqid.Equals(qaqc.Id)).Count() > 0 ? true : false,
                                                                                                     QAAValueDescription = daef.Qaa.Where(c => c.PatientId.Equals(id) && c.Qaqid.Equals(qaqc.Id)).FirstOrDefault().ValueDescription,
                                                                                                     Id = qaqc.Id,
                                                                                                     Type = qaqc.Type,
                                                                                                     Title = qaqc.Title,
                                                                                                     IsValue = qaqc.IsValue,
                                                                                                     ValueDescription = qaqc.ValueDescription,
                                                                                                     SeqNo = qaqc.SeqNo,
                                                                                                     ParentId = qaqc.ParentId
                                                                                                 }).ToList()
                                                                                 }).ToList()
                                                           }).ToList()
                                       }).ToList()
                    };
                    if (patientSetting != null)
                    {
                        patientRecord.Introduce = patientSetting.Introduce;
                        if (!string.IsNullOrEmpty(patientSetting.QadoctorNo))
                        {
                            patientRecord.QADoctorList = (from d in users
                                                          select new SelectListItem
                                                          {
                                                              Text = d.UserName,
                                                              Value = d.UserNo,
                                                              Selected = patientSetting.QadoctorNo.Equals(d.UserNo) ? true : false
                                                          }).ToList();
                        }
                        else
                        {
                            patientRecord.QADoctorList = (from d in users
                                                          select new SelectListItem
                                                          {
                                                              Text = d.UserName,
                                                              Value = d.UserNo,
                                                              Selected = false
                                                          }).ToList();
                        }
                    }
                    else
                    {
                        patientRecord.QADoctorList = (from d in users
                                                      select new SelectListItem
                                                      {
                                                          Text = d.UserName,
                                                          Value = d.UserNo,
                                                          Selected = false
                                                      }).ToList();
                    }
                    return View(patientRecord);
                }
            }
        }

        [HttpPost]
        public IActionResult QA(PatientRecordViewModel patientRecordViewModel)
        {
            using (var daef = new DentistAssistantContext())
            {
                var qAQParentUnit = patientRecordViewModel.QACategorys.SelectMany(
                    c => c.QAGroupUnits.SelectMany(
                        c2 => c2.QAQParentUnits));
                List<Qaa> qaas = new List<Qaa>();
                foreach (var qaqp in qAQParentUnit)
                {
                    if (qaqp.QAQUnit.IsChecked || qaqp.QAQUnit.Type.Equals("T"))
                    {
                        qaas.Add(new Qaa()
                        {
                            PatientId = patientRecordViewModel.Patient.Id,
                            Qaqid = qaqp.QAQUnit.Id,
                            ValueDescription = qaqp.QAQUnit.QAAValueDescription
                        });
                    }
                    if (qaqp.QAQUnits != null)
                    {
                        if (qaqp.QAQUnits.Count > 0)
                        {
                            foreach (var qaq in qaqp.QAQUnits)
                            {
                                if (qaq.IsChecked || qaq.Type.Equals("T"))
                                {
                                    qaas.Add(new Qaa()
                                    {
                                        PatientId = patientRecordViewModel.Patient.Id,
                                        Qaqid = qaq.Id,
                                        ValueDescription = qaq.QAAValueDescription
                                    });
                                }
                            }
                        }
                    }
                }

                var qaa = from a in daef.Qaa
                          where a.PatientId.Equals(patientRecordViewModel.Patient.Id)
                          select a;
                //移除所有原有的選項
                daef.Qaa.RemoveRange(qaa);
                //新增新的勾選的選項
                daef.Qaa.AddRange(qaas);


                var patientSetting = daef.PatientSettings.Find(patientRecordViewModel.Patient.Id);
                if (patientSetting != null)
                {
                    patientSetting.Introduce = patientRecordViewModel.Introduce;
                    patientSetting.QadoctorNo = patientRecordViewModel.QADoctor;
                }
                else
                {
                    daef.PatientSettings.Add(new PatientSettings()
                    {
                        Id = patientRecordViewModel.Patient.Id,
                        Introduce = patientRecordViewModel.Introduce,
                        QadoctorNo = patientRecordViewModel.QADoctor
                    });
                }

                daef.SaveChanges();

                return RedirectToAction("Record", "Patient", new { id = patientRecordViewModel.Patient.Id });
            }
        }

        [HttpGet]
        public IActionResult Share(string id, string shareTypeId)
        {
            using (var def = new DoctorContext())
            {
                using (var daef = new DentistAssistantContext())
                {
                    var patient = (from p in def.Patients
                                   where p.Id.ToLower().Equals(id.ToLower()) &&
                                         p.Enable.Equals(true)
                                   select p).FirstOrDefault();
                    var users = def.Users.ToList();
                    //Share
                    var patientSettings = daef.PatientSettings.Find(id);
                    var share = new Share()
                    {
                        Users = (from u in users
                                 select new SelectListItem()
                                 {
                                     Text = u.UserName,
                                     Value = u.UserNo,
                                     Selected = false
                                 }).ToList(),
                        Patient = patient,
                        ShareType = shareTypeId,
                        IsCheckedImages = patientSettings != null ? patientSettings.IsShareImage : false,
                        IsCheckedVideos = patientSettings != null ? patientSettings.IsShareVideo : false,
                        Shares = (from s in daef.Shares
                                  where s.ShareTypeId.ToLower().Trim().Equals(shareTypeId.ToLower().Trim()) &&
                                  s.PatId.Equals(patient.Id) &&
                                  s.IsEnable.Equals(true)
                                  orderby s.CreateDate descending
                                  select new ShareEditUnit()
                                  {
                                      Id = s.Id,
                                      PatId = s.PatId,
                                      ValueDescription = s.ValueDescription,
                                      UserNo = s.UserNo,
                                      UserName = (from u in users
                                                  where u.UserNo.Equals(s.UserNo)
                                                  select u.UserName).FirstOrDefault(),
                                      CreateDate = s.CreateDate.ToString("yyyy/MM/dd HH:mm"),
                                      ShareTypeId = s.ShareTypeId
                                  }).ToList()
                    };
                    return View(share);
                }
            }
        }

        [HttpGet]
        public IActionResult CreateShare(string patId, string shareTypeId)
        {
            using (var def = new DoctorContext())
            {
                var UserNo = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var UserName = HttpContext.User.FindFirst("DisplayName").Value;
                var users = def.Users.ToList();
                CreateShare createShare = new CreateShare()
                {
                    PatId = patId,
                    Users = (from u in users
                             select new SelectListItem()
                             {
                                 Text = u.UserName,
                                 Value = u.UserNo,
                                 Selected = false
                             }).ToList(),
                    UserName = UserName,
                    CreateDate = DateTime.Now,
                    ShareTypeId = shareTypeId

                };
                return View(createShare);
            }
        }

        [HttpPost]
        public IActionResult CreateShare(string patId, string valueDescription, string userNo, string createDate , string shareTypeId)
        {
            using (var def = new DoctorContext())
            {
                using (var daef = new DentistAssistantContext())
                {
                    try
                    {
                        var UserNo = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        var UserName = HttpContext.User.FindFirst("DisplayName").Value;
                        switch (shareTypeId)
                        {
                            case "Sticker":
                                //Sticker
                                var user = def.Users.Find(userNo);
                                var shareSticker = new Shares()
                                {
                                    PatId = patId,
                                    ValueDescription = valueDescription,
                                    UserNo = userNo,
                                    CreateDate = DateTime.Parse(createDate),
                                    IsEnable = true,
                                    ShareTypeId = shareTypeId,
                                };
                                daef.Shares.Add(shareSticker);
                                daef.SaveChanges();
                                var jsonResultSticker = new
                                {
                                    status = true,
                                    info = new
                                    {
                                        userName = user.UserName,
                                        time = shareSticker.CreateDate.ToString("yyyy/MM/dd")
                                    }
                                };
                                return Json(jsonResultSticker);
                            default:
                                //SizeU
                                //SizeD
                                var shareSize = new Shares()
                                {
                                    PatId = patId,
                                    ValueDescription = valueDescription,
                                    UserNo = UserNo,
                                    CreateDate = DateTime.Parse(createDate),
                                    IsEnable = true,
                                    ShareTypeId = shareTypeId,
                                };
                                daef.Shares.Add(shareSize);
                                daef.SaveChanges();
                                var jsonResultSize = new
                                {
                                    status = true,
                                    info = new
                                    {
                                        userName = UserName,
                                        valueDescription = shareSize.ValueDescription,
                                        time = shareSize.CreateDate.ToString("yyyy/MM/dd")
                                    }
                                };
                                return Json(jsonResultSize);
                        }
                    }
                    catch (Exception ex)
                    {
                        var jsonResult = new
                        {
                            status = false,
                            message = ex.ToString()
                        };
                        return Json(jsonResult);
                    }
                }
            }
        }

        [HttpPost]
        public JsonResult ShareImageChanged(string patId, bool isChecked)
        {
            using (var daef = new DentistAssistantContext())
            {
                var patientSetting = daef.PatientSettings.Find(patId);
                try
                {
                    if (patientSetting != null)
                    {
                        patientSetting.IsShareImage = isChecked;
                        daef.SaveChanges();
                    }
                    else
                    {
                        daef.PatientSettings.Add(new PatientSettings()
                        {
                            Id = patId,
                            IsShareImage = isChecked
                        });
                        daef.SaveChanges();
                    }
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

        [HttpPost]
        public JsonResult ShareVideoChanged(string patId, bool isChecked)
        {
            using (var daef = new DentistAssistantContext())
            {
                var patientSetting = daef.PatientSettings.Find(patId);
                try
                {
                    if (patientSetting != null)
                    {
                        patientSetting.IsShareVideo = isChecked;
                        daef.SaveChanges();
                    }
                    else
                    {
                        daef.PatientSettings.Add(new PatientSettings()
                        {
                            Id = patId,
                            IsShareVideo = isChecked
                        });
                        daef.SaveChanges();
                    }
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

        [HttpPost]
        public JsonResult AddShareType(string patId, string shareTypeId)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    var UserNo = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var UserName = HttpContext.User.FindFirst("DisplayName").Value;
                    var share = new Shares()
                    {
                        PatId = patId,
                        UserNo = UserNo,
                        CreateDate = DateTime.Now,
                        IsEnable = true,
                        ShareTypeId = shareTypeId,
                    };
                    daef.Shares.Add(share);
                    daef.SaveChanges();
                    var jsonResult = new
                    {
                        status = true,
                        info = new
                        {
                            userName = UserName,
                            time = share.CreateDate.ToString("yyyy/MM/dd")
                        }
                    };
                    return Json(jsonResult);
                }
                catch (Exception ex)
                {
                    var jsonResult = new
                    {
                        status = false,
                        message = ex.ToString()
                    };
                    return Json(jsonResult);
                }
            }
        }


        [HttpPost]
        public JsonResult UpdateShareCreateDate(int id, DateTime dateTime)
        {
            using (var daef = new DentistAssistantContext())
            {
                var shares = daef.Shares.Find(id);
                var lasetShare = from s in daef.Shares
                                 where s.PatId.Equals(shares.PatId) &&
                                 s.ShareTypeId.Equals(shares.ShareTypeId) &&
                                 s.IsEnable.Equals(true)
                                 orderby s.CreateDate descending
                                 select s.Id;
                try
                {
                    if (shares != null)
                    {
                        shares.CreateDate = dateTime;
                        daef.SaveChanges();
                        var jsonResult = new
                        {
                            status = true,
                            isUpdate = lasetShare.FirstOrDefault().Equals(shares.Id) ? true : false,
                            updateShareType = shares.ShareTypeId,
                            updateCreateDate = dateTime.ToString("yyyy/MM/dd")
                        };
                        return Json(jsonResult);
                    }
                    var jsonResultNull = new
                    {
                        status = false,
                        message = ""
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

        [HttpPost]
        public JsonResult UpdateShareValueDescription(int id, string valueDescription)
        {
            using (var daef = new DentistAssistantContext())
            {
                var shares = daef.Shares.Find(id);
                var lasetShare = from s in daef.Shares
                                 where s.PatId.Equals(shares.PatId) &&
                                 s.ShareTypeId.Equals(shares.ShareTypeId) &&
                                 s.IsEnable.Equals(true)
                                 orderby s.CreateDate descending
                                 select s.Id;
                try
                {
                    if (shares != null)
                    {
                        shares.ValueDescription = valueDescription;
                        daef.SaveChanges();
                        var jsonResult = new
                        {
                            status = true,
                            isUpdate = lasetShare.FirstOrDefault().Equals(shares.Id) ? true : false,
                            updateShareType = shares.ShareTypeId,
                            updateCreateDate = shares.CreateDate.ToString("yyyy/MM/dd"),
                            updateValueDescription = shares.ValueDescription
                        };
                        return Json(jsonResult);
                    }
                    var jsonResultNull = new
                    {
                        status = false,
                        message = ""
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

        [HttpPost]
        public JsonResult UpdateShareUserNo(int id, string userNo)
        {
            using (var def = new DoctorContext())
            {
                using (var daef = new DentistAssistantContext())
                {
                    var shares = daef.Shares.Find(id);
                    var lasetShare = from s in daef.Shares
                                     where s.PatId.Equals(shares.PatId) &&
                                     s.ShareTypeId.Equals(shares.ShareTypeId) &&
                                     s.IsEnable.Equals(true)
                                     orderby s.CreateDate descending
                                     select s.Id;
                    try
                    {
                        if (shares != null)
                        {
                            shares.UserNo = userNo;
                            daef.SaveChanges();
                            var userName = def.Users.Find(shares.UserNo).UserName;
                            var jsonResult = new
                            {
                                status = true,
                                isUpdate = lasetShare.FirstOrDefault().Equals(shares.Id) ? true : false,
                                updateShareType = shares.ShareTypeId,
                                updateCreateDate = shares.CreateDate.ToString("yyyy/MM/dd"),
                                updateUserName = userName
                            };
                            return Json(jsonResult);
                        }
                        var jsonResultNull = new
                        {
                            status = false,
                            message = ""
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

        [HttpPost]
        public JsonResult RemoveShare(string id)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var shares = daef.Shares.Find(int.Parse(id));
                        shares.IsEnable = false;
                        daef.SaveChanges();

                        var sharesCount = (from s in daef.Shares
                                           where s.PatId.Equals(shares.PatId) &&
                                                s.ShareTypeId.Equals(shares.ShareTypeId) &&
                                                s.IsEnable.Equals(true)
                                           select s).Count();


                        var jsonResultAdd = new
                        {
                            status = true,
                            isUpdate = sharesCount > 0 ? false : true,
                            updateShareType = shares.ShareTypeId
                        };
                        return Json(jsonResultAdd);
                    }
                    else
                    {
                        var jsonResultNoId = new
                        {
                            status = false,
                            message = "尚未有圖片資訊"
                        };
                        return Json(jsonResultNoId);
                    }
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