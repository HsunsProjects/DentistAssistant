using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class PatientSettings
    {
        public PatientSettings()
        {
            PatientRecords = new HashSet<PatientRecords>();
        }

        public string Id { get; set; }
        public string QadoctorNo { get; set; }
        public bool IsShareImage { get; set; }
        public bool IsShareVideo { get; set; }
        public string Introduce { get; set; }
        public DateTime? FirstTimeTime { get; set; }
        public string SuggestionNote { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual ICollection<PatientRecords> PatientRecords { get; set; }
    }
}
