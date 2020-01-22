using DentistAssistant.Extensions;
using DentistAssistant.Models;
using DentistAssistant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DentistAssistant.Controllers
{
    public class PatientController : Controller
    {
        [HttpGet]
        public IActionResult Index(string id)
        {
            using (var def = new DoctorContext())
            {
                var patient = (from p in def.Patients
                               where p.PatNo.ToLower().Equals(id.ToLower()) &&
                                     p.Enable.Equals(true)
                               select p).FirstOrDefault();
                return View(patient);
            }
        }

        [HttpGet]
        public IActionResult Record(string id)
        {
            using (var def = new DoctorContext())
            {
                var users = UsersInfo.Users;
                using (var daef = new DentistAssistantContext())
                {
                    var patient = (from p in def.Patients
                                   where p.PatNo.ToLower().Equals(id.ToLower()) &&
                                         p.Enable.Equals(true)
                                   select p).FirstOrDefault();
                    var patientSettingFirstTimeRecord = (from ps in daef.PatientSettings
                                                         where ps.Id.ToUpper().Equals(patient.PatNo.ToUpper())
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
                                                                                                   select new FdiUnit()
                                                                                                   {
                                                                                                       Fdi = f,
                                                                                                       FdiDetails = f.FdiDetails.ToList()
                                                                                                   }).ToList(),
                                                                                      FdiUnitsN = (from f in pr.Fdis
                                                                                                   where f.Type.Equals("N")
                                                                                                   select new FdiUnit()
                                                                                                   {
                                                                                                       Fdi = f,
                                                                                                       FdiDetails = f.FdiDetails.ToList()
                                                                                                   }).ToList(),
                                                                                      RecordUserUnit = (from ru in pr.RecordUsers
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

                    //QA
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
                                       }).ToList(),
                    };
                    //patientRecord.ShareViewModel = GetShareImage(patient.Id);
                    patientRecord.ShareViewModel = GetShare(patient.PatNo);

                    if(patientSettingFirstTimeRecord != null)
                    {
                        patientRecord.Introduce = patientSettingFirstTimeRecord.PatientSetting.Introduce;
                        if (!string.IsNullOrEmpty(patientSettingFirstTimeRecord.PatientSetting.QadoctorNo))
                        {
                            patientRecord.QADoctor = def.Users.Find(patientSettingFirstTimeRecord.PatientSetting.QadoctorNo).UserName;
                        }
                        DateTime firstTimeTime;
                        if (DateTime.TryParse(patientSettingFirstTimeRecord.PatientSetting.FirstTimeTime.ToString(), out firstTimeTime))
                        {
                            patientRecord.IsFirstTimeExist = true;
                            patientRecord.PatientSettingRecordViewModel = patientSettingFirstTimeRecord;
                        }
                    }

                    if (patientSettingFirstTimeRecord.PatientSetting.IsCompleted == null || patientSettingFirstTimeRecord.PatientSetting.IsCompleted.Equals(true))
                    {
                        patientRecord.IsComplete =  true;
                    }
                    else
                    {
                        patientRecord.IsComplete =  false;
                    }
                    return View(patientRecord);
                }
            }
        }

        [HttpGet]
        public IActionResult Suggestion(string id)
        {
            using (var def = new DoctorContext())
            {
                var users = UsersInfo.Users;
                using (var daef = new DentistAssistantContext())
                {
                    SuggestionViewModel suggestionViewModel = new SuggestionViewModel();
                    suggestionViewModel.Patient = (from p in def.Patients
                                                   where p.PatNo.ToLower().Equals(id.ToLower()) &&
                                                         p.Enable.Equals(true)
                                                   select p).FirstOrDefault();

                    PatientRecordSuggestUnit patientRecordSuggestUnit = new PatientRecordSuggestUnit();
                    var patientSetting = daef.PatientSettings.Find(suggestionViewModel.Patient.PatNo);
                    if (patientSetting != null)
                    {
                        ViewBag.NoPatientSetting = false;
                        var isPatientRecord = (from pr in daef.PatientRecords
                                               where pr.IsSuggest.Equals(true) &&
                                               pr.PatientSettingId.ToLower().Equals(id.ToLower())
                                               select new PatientRecordSuggestUnit()
                                               {
                                                   Id = pr.Id,
                                                   UserNo = pr.UserNo,
                                                   UserName = users.Where(c => c.UserNo.Equals(pr.UserNo)).FirstOrDefault().UserName,
                                                   CreateTime = pr.CreateTime,
                                                   PatientSettingId = pr.PatientSettingId,
                                                   FdiUnitsS = (from f in pr.Fdis
                                                                where f.Type.Equals("S")
                                                                select new FdiUnit()
                                                                {
                                                                    Fdi = f,
                                                                    FdiDetails = f.FdiDetails.ToList()
                                                                }).ToList()
                                               }).FirstOrDefault();

                        if (isPatientRecord == null)
                        {
                            PatientRecords patientRecords = new PatientRecords();
                            patientRecords.UserNo = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                            patientRecords.IsSuggest = true;
                            patientRecords.CreateTime = DateTime.Now;
                            patientRecords.PatientSettingId = id;
                            daef.PatientRecords.Add(patientRecords);
                            daef.SaveChanges();

                            patientRecordSuggestUnit.Id = patientRecords.Id;
                            patientRecordSuggestUnit.UserNo = patientRecords.UserNo;
                            patientRecordSuggestUnit.UserName = users.Where(c => c.UserNo.Equals(patientRecords.UserNo)).FirstOrDefault().UserName;
                            patientRecordSuggestUnit.CreateTime = patientRecords.CreateTime;
                            patientRecordSuggestUnit.PatientSettingId = id;
                            patientRecordSuggestUnit.FdiUnitsS = new List<FdiUnit>();
                        }
                        else
                        {
                            patientRecordSuggestUnit = isPatientRecord;
                        }

                        suggestionViewModel.PatientRecordSuggestUnit = patientRecordSuggestUnit;
                        suggestionViewModel.SuggestionNote = patientSetting.SuggestionNote;
                    }
                    else
                    {
                        ViewBag.NoPatientSetting = true;
                    }

                    return View(suggestionViewModel);
                }
            }
        }

        [HttpPost]
        public JsonResult SaveSuggestionNote(string patientId, string suggestionNote)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    var patientSetting = daef.PatientSettings.Find(patientId);
                    patientSetting.SuggestionNote = suggestionNote;
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

        [HttpGet]
        public IActionResult Assistant(string id)
        {
            using (var def = new DoctorContext())
            {
                var users = UsersInfo.Users;
                using (var daef = new DentistAssistantContext())
                {
                    var Notes = (from qaa in daef.Qaa
                                 where qaa.PatientId.ToLower().Equals(id.ToLower()) &&
                                 qaa.Qaqid.Equals(30)
                                 select qaa).FirstOrDefault();
                    var patientRecords = (from pr in daef.PatientRecords
                                          where pr.PatientSetting.Id.ToUpper().Equals(id.ToUpper())
                                          orderby pr.CreateTime descending
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
                                              IsSuggest = pr.IsSuggest,
                                              CreateTime = pr.CreateTime,
                                              PatientSettingId = pr.PatientSettingId,
                                              FdiUnitsF = (from f in pr.Fdis
                                                           where f.Type.Equals("F")
                                                           select new FdiUnit()
                                                           {
                                                               Fdi = f,
                                                               FdiDetails = f.FdiDetails.ToList()
                                                           }).ToList(),
                                              FdiUnitsN = (from f in pr.Fdis
                                                           where f.Type.Equals("N")
                                                           select new FdiUnit()
                                                           {
                                                               Fdi = f,
                                                               FdiDetails = f.FdiDetails.ToList()
                                                           }).ToList(),
                                              RecordUserUnit = (from ru in pr.RecordUsers
                                                                select new RecordUserUnit()
                                                                {
                                                                    Id = ru.Id,
                                                                    UserNo = ru.UserNo,
                                                                    UserName = users.Where(c => c.UserNo.Equals(ru.UserNo)).FirstOrDefault().UserName,
                                                                    CreateDate = ru.CreateDate,
                                                                    PatientRecordId = ru.PatientRecordId
                                                                }).ToList()
                                          }).ToList();
                    AssistantViewModel assistantViewModel = new AssistantViewModel()
                    {
                        Patient = (from p in def.Patients
                                   where p.PatNo.ToLower().Equals(id.ToLower()) &&
                                         p.Enable.Equals(true)
                                   select p).FirstOrDefault(),
                        Notes = Notes == null ? null : Notes.ValueDescription,
                        IsFinishFirstTime = patientRecords.Where(c => c.IsFirst.Equals(true)).Count() > 0 ? true : false,
                        PatientRecordUnits = patientRecords.Where(c => c.IsFirst.Equals(false) && c.IsSuggest.Equals(false)).ToList()
                    };
                    return View(assistantViewModel);
                }
            }
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            using (var def = new DoctorContext())
            {
                using (var daef = new DentistAssistantContext())
                {
                    List<PatientCompleteViewModel> patients = new List<PatientCompleteViewModel>();
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        var patientSetting = daef.PatientSettings.ToList();
                        patients = (from p in def.Patients
                                    where (p.PatName.ToLower().Contains(searchString.ToLower()) ||
                                    TransBirth((DateTime)p.Birth).Contains(searchString) ||
                                    (string.IsNullOrEmpty(p.Id) ? false : p.Id.ToLower().Contains(searchString.ToLower())) ||
                                    p.PatNo.ToLower().Contains(searchString.ToLower())) &&
                                         p.Enable.Equals(true)
                                    select new PatientCompleteViewModel()
                                    {
                                        Patient = p,
                                        PatientSettings = (from ps in patientSetting
                                                           where ps.Id.Equals(p.PatNo)
                                                           select ps).FirstOrDefault()
                                    }).ToList();
                    }
                    return View(patients);
                }
            }
        }

        private string TransBirth(DateTime birth)
        {
            string newBirth = "0000000";
            DateTime parseBirth;
            if (DateTime.TryParse(birth.ToString(), out parseBirth))
            {
                newBirth = (parseBirth.Year - 1911).ToString().PadLeft(3, '0') + parseBirth.Month.ToString().PadLeft(2, '0') + parseBirth.Day.ToString().PadLeft(2, '0');
            }


            return newBirth;
        }

        [HttpGet]
        public IActionResult AdvancedSearch()
        {
            using (var def = new DoctorContext())
            {
                return View();
            }
        }

        private ShareViewModel GetShare(string patId)
        {
            using (var def = new DoctorContext())
            {
                using (var daef = new DentistAssistantContext())
                {
                    ShareViewModel shareViewModel = new ShareViewModel();
                    if (!string.IsNullOrEmpty(patId))
                    {
                        var users = UsersInfo.Users;
                        var patientSettings = daef.PatientSettings.Find(patId);
                        var shareType = from st in daef.ShareTypes
                                        select new LastOneShareType()
                                        {
                                            Id = st.Id,
                                            Type = st.Type,
                                            Name = st.Name,
                                            Share = (from s in st.Shares
                                                     where s.PatId.Equals(patId) &&
                                                     s.IsEnable.Equals(true)
                                                     orderby s.CreateDate descending
                                                     select new ShareUnit()
                                                     {
                                                         Id = s.Id,
                                                         PatId = s.PatId,
                                                         ValueDescription = s.ValueDescription,
                                                         UserNo = s.UserNo,
                                                         UserName = users.Where(c => c.UserNo.Equals(s.UserNo)).FirstOrDefault().UserName,
                                                         CreateDate = s.CreateDate,
                                                         ShareTypeId = s.ShareTypeId
                                                     }).FirstOrDefault()
                                        };

                        shareViewModel.IsCheckedImages = patientSettings != null ? patientSettings.IsShareImage : false;
                        shareViewModel.IsCheckedVideos = patientSettings != null ? patientSettings.IsShareVideo : false;
                        foreach (var st in shareType)
                        {
                            if (st.Share != null)
                            {
                                switch (st.Id)
                                {
                                    case "BlackA":
                                        shareViewModel.BlackA = st.Share;
                                        break;
                                    case "BlackB":
                                        shareViewModel.BlackB = st.Share;
                                        break;
                                    case "HeadTopA":
                                        shareViewModel.HeadTopA = st.Share;
                                        break;
                                    case "HeadTopB":
                                        shareViewModel.HeadTopB = st.Share;
                                        break;
                                    case "LCTA":
                                        shareViewModel.LCTA = st.Share;
                                        break;
                                    case "LCTB":
                                        shareViewModel.LCTB = st.Share;
                                        break;
                                    case "MirrorA":
                                        shareViewModel.MirrorA = st.Share;
                                        break;
                                    case "MirrorB":
                                        shareViewModel.MirrorB = st.Share;
                                        break;
                                    case "MouseInA":
                                        shareViewModel.MouseInA = st.Share;
                                        break;
                                    case "MouseInB":
                                        shareViewModel.MouseInB = st.Share;
                                        break;
                                    case "NineA":
                                        shareViewModel.NineA = st.Share;
                                        break;
                                    case "NineB":
                                        shareViewModel.NineB = st.Share;
                                        break;
                                    case "PanoA":
                                        shareViewModel.PANOA = st.Share;
                                        break;
                                    case "PanoB":
                                        shareViewModel.PANOB = st.Share;
                                        break;
                                    case "PREP":
                                        shareViewModel.PREP = st.Share;
                                        break;
                                    case "Print":
                                        shareViewModel.Print = st.Share;
                                        break;
                                    case "ReferenceModelA":
                                        shareViewModel.ReferenceModelA = st.Share;
                                        break;
                                    case "ReferenceModelB":
                                        shareViewModel.ReferenceModelB = st.Share;
                                        break;
                                    case "ScanA":
                                        shareViewModel.ScanA = st.Share;
                                        break;
                                    case "ScanB":
                                        shareViewModel.ScanB = st.Share;
                                        break;
                                    case "SizeD":
                                        shareViewModel.SizeD = st.Share;
                                        break;
                                    case "SizeU":
                                        shareViewModel.SizeU = st.Share;
                                        break;
                                    case "Sticker":
                                        shareViewModel.Sticker = st.Share;
                                        break;
                                    case "Take":
                                        shareViewModel.Take = st.Share;
                                        break;
                                    case "UCTA":
                                        shareViewModel.UCTA = st.Share;
                                        break;
                                    case "UCTB":
                                        shareViewModel.UCTB = st.Share;
                                        break;
                                    case "Wax":
                                        shareViewModel.Wax = st.Share;
                                        break;
                                    case "WhiteA":
                                        shareViewModel.WhiteA = st.Share;
                                        break;
                                    case "WhiteB":
                                        shareViewModel.WhiteB = st.Share;
                                        break;
                                }
                            }
                        }
                    }
                    return shareViewModel;
                }
            }
        }

        [HttpPost]
        public JsonResult SetPatientComplete(string patNo)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    var patientSetting = daef.PatientSettings.Find(patNo);
                    if (patientSetting != null)
                    {
                        patientSetting.IsCompleted = false;
                        patientSetting.CreateTime = DateTime.Now;
                    }
                    else
                    {
                        daef.PatientSettings.Add(new PatientSettings()
                        {
                            Id = patNo,
                            IsShareImage = false,
                            IsShareVideo = false,
                            IsCompleted = false,
                            CreateTime = DateTime.Now
                        });
                    }

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

        [HttpPost]
        public JsonResult SetPatientFinish(string patNo)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    var patientSetting = daef.PatientSettings.Find(patNo);
                    if (patientSetting != null)
                    {
                        patientSetting.IsCompleted = true;
                    }
                    else
                    {
                        daef.PatientSettings.Add(new PatientSettings()
                        {
                            Id = patNo,
                            IsShareImage = false,
                            IsShareVideo = false,
                            IsCompleted = true
                        });
                    }

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