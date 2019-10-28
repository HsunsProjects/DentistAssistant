using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class PhraseGroups
    {
        public PhraseGroups()
        {
            Phrases = new HashSet<Phrases>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SeqNo { get; set; }

        public virtual ICollection<Phrases> Phrases { get; set; }
    }
}
