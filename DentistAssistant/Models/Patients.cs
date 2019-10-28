using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class Patients
    {
        public string PatNo { get; set; }
        public string PatName { get; set; }
        public string Id { get; set; }
        public string Sex { get; set; }
        public DateTime? Birth { get; set; }
        public string TelH { get; set; }
        public string TelC { get; set; }
        public string Moble1 { get; set; }
        public string Moble2 { get; set; }
        public string Email { get; set; }
        public string Zip1 { get; set; }
        public string Zip2 { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Job { get; set; }
        public string PicPath { get; set; }
        public byte[] Picture { get; set; }
        public string Introducer { get; set; }
        public string Doctor { get; set; }
        public string Blood { get; set; }
        public string Oral { get; set; }
        public DateTime? OralDate { get; set; }
        public string Notes { get; set; }
        public string CardId { get; set; }
        public string Vip { get; set; }
        public string DrugAllergy { get; set; }
        public string Questions { get; set; }
        public string FirstTeeth { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? ScDate { get; set; }
        public DateTime? ExDate { get; set; }
        public DateTime? ImDate { get; set; }
        public DateTime? EnDate { get; set; }
        public DateTime? LookAmtDate { get; set; }
        public string Mark { get; set; }
        public DateTime? MailDate { get; set; }
        public string BurdenNo { get; set; }
        public string BigSickNo { get; set; }
        public string BigSickStart { get; set; }
        public string BigSickEnd { get; set; }
        public string EmergPhone { get; set; }
        public int? PatLevel { get; set; }
        public bool? Enable { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string DeleteUser { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string CountryCode { get; set; }
        public string IsAreca { get; set; }
        public string IsSmoke { get; set; }
        public bool PatMerge { get; set; }
        public string OPatNo { get; set; }
        public decimal? ExemptAmt { get; set; }
        public string SpatNo { get; set; }
        public string FirstDoc { get; set; }
        public string LastDoc { get; set; }
        public bool? IsGenUpload { get; set; }
        public bool? IsTrackPat { get; set; }
        public string MobleC { get; set; }
        public string QuickCode { get; set; }
        public string MessageSet { get; set; }
        public string IntroducerNote { get; set; }
        public string EmHuman { get; set; }
        public string EmRelationShip { get; set; }
        public string EmPhone { get; set; }
        public string Minority { get; set; }
        public string NotesEx { get; set; }
        public bool? IsFreezeData { get; set; }
        public string BlackList { get; set; }
        public string OtherNote { get; set; }
        public string OeNotes { get; set; }
        public string OeNotesEx { get; set; }
        public string Education { get; set; }
        public bool? FamilyTag { get; set; }
        public string Matter { get; set; }
        public string Notice { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string SpType { get; set; }
        public string Sp { get; set; }
        public DateTime? SpValidDate { get; set; }
    }
}
