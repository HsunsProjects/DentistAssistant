using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DentistAssistant.Models;
using DentistAssistant.Extensions;
using DentistAssistant.ViewModels;

namespace DentistAssistant.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (var def = new DoctorContext())
            {
                using (var daef = new DentistAssistantContext())
                {
                    var x = daef.PatientSettings.ToList();

                    var patientSetting = (from ps in daef.PatientSettings
                                          where ps.IsCompleted != null && ps.IsCompleted != true
                                          select ps.Id).ToList();

                    var patients = (from p in def.Patients
                                    where patientSetting.Contains(p.PatNo)
                                    select p).ToList();
                    return View(patients);
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
