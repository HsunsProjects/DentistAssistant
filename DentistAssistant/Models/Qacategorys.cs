using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class Qacategorys
    {
        public Qacategorys()
        {
            Qagroups = new HashSet<Qagroups>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? SeqNo { get; set; }

        public virtual ICollection<Qagroups> Qagroups { get; set; }
    }
}
