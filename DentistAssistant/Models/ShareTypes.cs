using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class ShareTypes
    {
        public ShareTypes()
        {
            Shares = new HashSet<Shares>();
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Shares> Shares { get; set; }
    }
}
