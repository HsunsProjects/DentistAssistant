using DentistAssistant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentistAssistant.ViewModels
{
    public class PhrasesAddPhrasesViewModel
    {
        public int PhraseGroupId { get; set; }
        [Display(Name = "輸入片語")]
        public List<string> Description { get; set; }
    }

    public class PhrasesEditPhrasesViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "輸入片語")]
        public string Description { get; set; }
    }
}
