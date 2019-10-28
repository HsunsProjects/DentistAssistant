using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class Qaa
    {
        public string PatientId { get; set; }
        public int Qaqid { get; set; }
        public string ValueDescription { get; set; }

        public virtual Qaq Qaq { get; set; }
    }
}
