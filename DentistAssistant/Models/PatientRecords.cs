using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class PatientRecords
    {
        public PatientRecords()
        {
            Fdis = new HashSet<Fdis>();
            RecordUsers = new HashSet<RecordUsers>();
        }

        public int Id { get; set; }
        public string Room { get; set; }
        public string UserNo { get; set; }
        public DateTime? OrderTime { get; set; }
        public DateTime? ArriveTime { get; set; }
        public DateTime? DrArriveTime { get; set; }
        public DateTime? DrLeaveTime { get; set; }
        public DateTime? PtLeaveTime { get; set; }
        public bool IsFirst { get; set; }
        public bool IsSuggest { get; set; }
        public DateTime CreateTime { get; set; }
        public string PatientSettingId { get; set; }

        public virtual PatientSettings PatientSetting { get; set; }
        public virtual ICollection<Fdis> Fdis { get; set; }
        public virtual ICollection<RecordUsers> RecordUsers { get; set; }
    }
}
