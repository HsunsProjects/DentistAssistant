using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class RecordUsers
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public DateTime CreateDate { get; set; }
        public int PatientRecordId { get; set; }

        public virtual PatientRecords PatientRecord { get; set; }
    }
}
