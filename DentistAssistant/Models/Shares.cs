using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class Shares
    {
        public int Id { get; set; }
        public string PatId { get; set; }
        public string ValueDescription { get; set; }
        public string UserNo { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsEnable { get; set; }
        public string ShareTypeId { get; set; }

        public virtual ShareTypes ShareType { get; set; }
    }
}
