using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentistAssistant.ViewModels
{
    public class AssistantListViewModel
    {
        public int PatientRecordId { get; set; }
        public string UserNo { get; set; }
        [Display(Name ="助理清單")]
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
