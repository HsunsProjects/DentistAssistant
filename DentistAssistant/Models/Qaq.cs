using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class Qaq
    {
        public Qaq()
        {
            Qaa = new HashSet<Qaa>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public bool IsValue { get; set; }
        public string ValueDescription { get; set; }
        public int? SeqNo { get; set; }
        public int? ParentId { get; set; }
        public int QagroupId { get; set; }

        public virtual Qagroups Qagroup { get; set; }
        public virtual ICollection<Qaa> Qaa { get; set; }
    }
}
