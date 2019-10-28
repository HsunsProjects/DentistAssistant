using DentistAssistant.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentistAssistant.ViewModels
{
    public class CreatePatientRecordViewModel
    {
        public IEnumerable<SelectListItem> Users { get; set; }
        public string PatientId { get; set; }
        public PatientRecords patientRecord { get; set; }
    }
}
