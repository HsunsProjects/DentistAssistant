using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class Phrases
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? SeqNo { get; set; }
        public int PhraseGroupId { get; set; }

        public virtual PhraseGroups PhraseGroup { get; set; }
    }
}
