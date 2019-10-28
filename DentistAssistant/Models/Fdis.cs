using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class Fdis
    {
        public Fdis()
        {
            FdiDetails = new HashSet<FdiDetails>();
        }

        public int Id { get; set; }
        public bool? Fm { get; set; }
        public bool? Ub { get; set; }
        public bool? Ua { get; set; }
        public bool? Ur { get; set; }
        public bool? Ul { get; set; }
        public bool? Lb { get; set; }
        public bool? La { get; set; }
        public bool? Lr { get; set; }
        public bool? Ll { get; set; }
        public bool? Indescribable { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Type { get; set; }
        public int PatientRecordId { get; set; }

        public virtual PatientRecords PatientRecord { get; set; }
        public virtual ICollection<FdiDetails> FdiDetails { get; set; }
    }
}
