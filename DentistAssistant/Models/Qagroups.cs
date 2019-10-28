using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class Qagroups
    {
        public Qagroups()
        {
            Qaq = new HashSet<Qaq>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? SeqNo { get; set; }
        public int QacategoryId { get; set; }

        public virtual Qacategorys Qacategory { get; set; }
        public virtual ICollection<Qaq> Qaq { get; set; }
    }
}
