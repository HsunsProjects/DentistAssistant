using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentistAssistant.Models;
using DentistAssistant.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DentistAssistant.Controllers
{
    public class PhrasesController : Controller
    {
        public IActionResult Index(int phraseGroupId)
        {
            using (var daef = new DentistAssistantContext())
            {
                var phraseGroup = daef.PhraseGroups.Find(phraseGroupId);
                var phraseList = from p in daef.Phrases
                                 where p.PhraseGroupId.Equals(phraseGroupId)
                                 select p;

                ViewBag.PhraseGroupName = phraseGroup.Name;
                ViewBag.PhraseGroupId = phraseGroup.Id;
                return View(phraseList.ToList());
            }
        }

        [HttpGet]
        public IActionResult PhraseGroup()
        {
            using (var daef = new DentistAssistantContext())
            {
                return View(daef.PhraseGroups.ToList());
            }
        }

        [HttpGet]
        public IActionResult EditPhrases(int id)
        {
            using (var daef = new DentistAssistantContext())
            {
                var phrase = daef.Phrases.Find(id);
                PhrasesEditPhrasesViewModel phrasesEditPhrasesViewModel = new PhrasesEditPhrasesViewModel()
                {
                    Id = phrase.Id,
                    Description = phrase.Description
                };
                return View(phrasesEditPhrasesViewModel);
            }
        }

        [HttpPost]
        public IActionResult EditPhrases(PhrasesEditPhrasesViewModel phrasesEditPhrasesViewModel)
        {
            using (var daef = new DentistAssistantContext())
            {
                var phrase = daef.Phrases.Find(phrasesEditPhrasesViewModel.Id);
                phrase.Description = phrasesEditPhrasesViewModel.Description;
                daef.SaveChanges();
                return View("Index", daef.Phrases.ToList());
            }
        }

        [HttpGet]
        public IActionResult DeletePhrases(int phraseGroupId, int id)
        {
            using (var daef = new DentistAssistantContext())
            {
                var phrases = daef.Phrases.Find(id);
                daef.Phrases.Remove(phrases);
                daef.SaveChanges();
                return Redirect(Url.Action("Index", "Phrases", new { phraseGroupId = phraseGroupId }));
            }
        }

        public IActionResult AddPhrases(int phraseGroupId)
        {
            var phrasesAddPhrasesViewModel = new PhrasesAddPhrasesViewModel()
            {
                PhraseGroupId = phraseGroupId
            };
            return View(phrasesAddPhrasesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhrases(PhrasesAddPhrasesViewModel phrasesAddPhrasesViewModel)
        {
            using (var daef = new DentistAssistantContext())
            {
                //var newParases = (from p in phrasesAddPhrasesViewModel.Description
                //                  select new Phrases()
                //                  {
                //                      Description = p
                //                  }).ToList();

                //daef.Phrases.AddRange(newParases);
                var phraseGroup = daef.PhraseGroups.Find(phrasesAddPhrasesViewModel.PhraseGroupId);
                foreach (string description in phrasesAddPhrasesViewModel.Description)
                {
                    phraseGroup.Phrases.Add(new Phrases()
                    {
                        Description = description
                    });
                }
                await daef.SaveChangesAsync();
                return Redirect(Url.Action("Index", "Phrases", new { phraseGroupId = phrasesAddPhrasesViewModel.PhraseGroupId }));
            }
        }


        [HttpPost]
        public JsonResult GetPhrases(string searchString)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        var phrases = (from p in daef.Phrases
                                       where p.Description.Contains(searchString)
                                       select p).ToList();
                        var jsonResultAdd = new
                        {
                            status = true,
                            info = (from p in daef.Phrases
                                    where p.Description.Contains(searchString)
                                    select new
                                    {
                                        id = p.Id,
                                        description = p.Description
                                    }).ToList()
                        };
                        return Json(jsonResultAdd);
                    }
                    else
                    {
                        var jsonResultNoId = new
                        {
                            status = false,
                            message = "請輸入片語關鍵字"
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


        [HttpPost]
        public JsonResult CrearePhraseGroupName(string phraseGroupName)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    if (!string.IsNullOrEmpty(phraseGroupName))
                    {
                        var phraseGroup = new PhraseGroups()
                        {
                            Name = phraseGroupName
                        };
                        daef.PhraseGroups.Add(phraseGroup);
                        daef.SaveChanges();
                        var jsonResultAdd = new
                        {
                            status = true,
                            info = new
                            {
                                phraseGroupId = phraseGroup.Id,
                                phraseGroupName = phraseGroup.Name
                            }
                        };
                        return Json(jsonResultAdd);
                    }
                    else
                    {
                        var jsonResultNoId = new
                        {
                            status = false,
                            message = "請輸入名稱"
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

        [HttpPost]
        public JsonResult DeletePhraseGroup(int phraseGroupId)
        {
            using (var daef = new DentistAssistantContext())
            {
                try
                {
                    var phrases = from p in daef.Phrases
                                      where p.PhraseGroupId.Equals(phraseGroupId)
                                      select p;
                    daef.Phrases.RemoveRange(phrases);
                    var phraseGroup = daef.PhraseGroups.Find(phraseGroupId);
                    daef.PhraseGroups.Remove(phraseGroup);
                    daef.SaveChanges();
                    var jsonResultAdd = new
                    {
                        status = true
                    };
                    return Json(jsonResultAdd);
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