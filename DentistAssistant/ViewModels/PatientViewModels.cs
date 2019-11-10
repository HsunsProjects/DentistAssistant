using DentistAssistant.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentistAssistant.ViewModels
{
    public class PatientRecordViewModel
    {
        public Patients Patient { get; set; }
        public List<QACategorys> QACategorys { get; set; }
        public string Introduce { get; set; }
        public IEnumerable<SelectListItem> QADoctorList { get; set; }
        public string QADoctor { get; set; }
        public ShareViewModel ShareViewModel { get; set; }
        public bool IsFirstTimeExist { get; set; } = false;
        public PatientSettingRecordViewModel PatientSettingRecordViewModel { get; set; }
    }

    public class PatientSettingRecordViewModel
    {
        public PatientSettings PatientSetting { get; set; }
        public PatientRecordUnit PatientRecordUnit { get; set; }
    }

    public class PatientRecordUnit
    {
        public int Id { get; set; }
        public string Room { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public DateTime? OrderTime { get; set; }
        public DateTime? ArriveTime { get; set; }
        public DateTime? DrArriveTime { get; set; }
        public DateTime? DrLeaveTime { get; set; }
        public DateTime? PtLeaveTime { get; set; }
        public bool IsFirst { get; set; }
        public bool IsSuggest { get; set; }
        public DateTime CreateTime { get; set; }
        public string PatientSettingId { get; set; }
        public List<FdiUnit> FdiUnitsF { get; set; }
        public List<FdiUnit> FdiUnitsN { get; set; }
        public List<RecordUserUnit> RecordUserUnit { get; set; }
    }

    public class PatientRecordSuggestUnit
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public string PatientSettingId { get; set; }
        public List<FdiUnit> FdiUnitsS { get; set; }
    }

    public class ShareViewModel
    {
        public bool IsCheckedImages { get; set; }
        public bool IsCheckedVideos { get; set; }
        public ShareUnit PANOB { get; set; }
        public ShareUnit PANOA { get; set; }
        public ShareUnit UCTB { get; set; }
        public ShareUnit UCTA { get; set; }
        public ShareUnit LCTB { get; set; }
        public ShareUnit LCTA { get; set; }
        public ShareUnit MouseInB { get; set; }
        public ShareUnit MouseInA { get; set; }
        public ShareUnit HeadTopB { get; set; }
        public ShareUnit HeadTopA { get; set; }
        public ShareUnit NineB { get; set; }
        public ShareUnit NineA { get; set; }
        public ShareUnit BlackB { get; set; }
        public ShareUnit BlackA { get; set; }
        public ShareUnit WhiteB { get; set; }
        public ShareUnit WhiteA { get; set; }
        public ShareUnit MirrorB { get; set; }
        public ShareUnit MirrorA { get; set; }
        public ShareUnit ScanB { get; set; }
        public ShareUnit ScanA { get; set; }
        public ShareUnit ReferenceModelB { get; set; }
        public ShareUnit ReferenceModelA { get; set; }
        public ShareUnit SizeU { get; set; }
        public ShareUnit SizeD { get; set; }
        public ShareUnit Print { get; set; }
        public ShareUnit Take { get; set; }
        public ShareUnit Wax { get; set; }
        public ShareUnit Sticker { get; set; }
        public ShareUnit PREP { get; set; }
    }

    public class AssistantViewModel
    { 
        public Patients Patient { get; set; }
        public string Notes { get; set; }
        public bool IsFinishFirstTime { get; set; } = false;
        public List<PatientRecordUnit> PatientRecordUnits { get; set; }
    }
    public class SuggestionViewModel
    {
        public Patients Patient { get; set; }
        public string SuggestionNote { get; set; }
        public PatientRecordSuggestUnit PatientRecordSuggestUnit { get; set; }
    }
}
