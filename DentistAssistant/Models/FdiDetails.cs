using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class FdiDetails
    {
        public string FdiArea { get; set; }
        public string FdiPosition { get; set; }
        public int FdiId { get; set; }

        public virtual Fdis Fdi { get; set; }
    }
}
