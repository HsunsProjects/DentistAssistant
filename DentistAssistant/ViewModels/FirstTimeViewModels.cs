using DentistAssistant.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace DentistAssistant.ViewModels
{
    public class CreateFirstTimeViewModel
    {
        public IEnumerable<SelectListItem> Users { get; set; }
        public DateTime FirstTime { get; set; }
        public string PatientId { get; set; }
        public PatientRecords patientRecord { get; set; }
    }

    public class RecordUserUnit
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public int PatientRecordId { get; set; }
    }

    public class FdiDescriptionViewModel
    {
        public int PatientRecordId { get; set; }
        public string Type { get; set; }
        public int PhraseGroupId1 { get; set; }
        public int PhraseGroupId2 { get; set; }
        public int PhraseGroupId3 { get; set; }
        public IEnumerable<SelectListItem> PhraseGroups { get; set; }
        public FdiUnit FdiUnit { get; set; }
    }

    public class FdiUnit
    {
        public Fdis Fdi { get; set; }
        public List<FdiDetails> FdiDetails { get; set; }
    }

    public class FdiChecked
    {
        public string Description { get; set; }
        public bool FM { get; set; } = false;
        public bool UB { get; set; } = false;
        public bool UA { get; set; } = false;
        public bool UR { get; set; } = false;
        public bool UL { get; set; } = false;
        public bool LB { get; set; } = false;
        public bool LA { get; set; } = false;
        public bool LR { get; set; } = false;
        public bool LL { get; set; } = false;
        public bool Indescribable { get; set; } = false;
        public bool OO { get; set; } = false;
        public bool OTw { get; set; } = false;
        public bool OTh { get; set; } = false;
        public bool OFo { get; set; } = false;
        public bool OFi { get; set; } = false;
        public bool OSi { get; set; } = false;
        public bool OSe { get; set; } = false;
        public bool OE { get; set; } = false;
        public bool ON { get; set; } = false;
        public bool TwO { get; set; } = false;
        public bool TwTw { get; set; } = false;
        public bool TwTh { get; set; } = false;
        public bool TwFo { get; set; } = false;
        public bool TwFi { get; set; } = false;
        public bool TwSi { get; set; } = false;
        public bool TwSe { get; set; } = false;
        public bool TwE { get; set; } = false;
        public bool TwN { get; set; } = false;
        public bool ThO { get; set; } = false;
        public bool ThTw { get; set; } = false;
        public bool ThTh { get; set; } = false;
        public bool ThFo { get; set; } = false;
        public bool ThFi { get; set; } = false;
        public bool ThSi { get; set; } = false;
        public bool ThSe { get; set; } = false;
        public bool ThE { get; set; } = false;
        public bool ThN { get; set; } = false;
        public bool FoO { get; set; } = false;
        public bool FoTw { get; set; } = false;
        public bool FoTh { get; set; } = false;
        public bool FoFo { get; set; } = false;
        public bool FoFi { get; set; } = false;
        public bool FoSi { get; set; } = false;
        public bool FoSe { get; set; } = false;
        public bool FoE { get; set; } = false;
        public bool FoN { get; set; } = false;
        public bool FiO { get; set; } = false;
        public bool FiTw { get; set; } = false;
        public bool FiTh { get; set; } = false;
        public bool FiFo { get; set; } = false;
        public bool FiFi { get; set; } = false;
        public bool SiO { get; set; } = false;
        public bool SiTw { get; set; } = false;
        public bool SiTh { get; set; } = false;
        public bool SiFo { get; set; } = false;
        public bool SiFi { get; set; } = false;
        public bool SeO { get; set; } = false;
        public bool SeTw { get; set; } = false;
        public bool SeTh { get; set; } = false;
        public bool SeFo { get; set; } = false;
        public bool SeFi { get; set; } = false;
        public bool EO { get; set; } = false;
        public bool ETw { get; set; } = false;
        public bool ETh { get; set; } = false;
        public bool EFo { get; set; } = false;
        public bool EFi { get; set; } = false;
    }
}
