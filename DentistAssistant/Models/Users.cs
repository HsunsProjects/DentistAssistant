using System;
using System.Collections.Generic;

namespace DentistAssistant.Models
{
    public partial class Users
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public DateTime? ArrDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public string EmpCard { get; set; }
        public DateTime? DueDate { get; set; }
        public string Id { get; set; }
        public DateTime? Birth { get; set; }
        public string Zip1 { get; set; }
        public string Zip2 { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Moble1 { get; set; }
        public string Moble2 { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Role { get; set; }
        public string ApplyDoctor { get; set; }
        public string AdoctorId { get; set; }
        public string License { get; set; }
        public string Skill { get; set; }
        public bool? IsFdi { get; set; }
        public bool? Is14 { get; set; }
        public bool? IsTourMount { get; set; }
        public bool? Is15 { get; set; }
        public bool? Is16 { get; set; }
        public string Notes { get; set; }
        public string Pass { get; set; }
        public string Layout { get; set; }
        public decimal? QueryPat { get; set; }
        public string OrderViewDay { get; set; }
        public string OrderViewSec { get; set; }
        public bool? Enable { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string DeleteUser { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string DealHistory { get; set; }
        public string Googlemail { get; set; }
        public string Googlepass { get; set; }
        public string GoogleNewWork { get; set; }
        public bool? GoogleSynC { get; set; }
        public int? GoogleSynCtime { get; set; }
        public bool? IsGenUpload { get; set; }
        public bool? IsClinicOwner { get; set; }
        public int? PhraseValue { get; set; }
        public int? PhraseValue1 { get; set; }
        public int? PhraseValue2 { get; set; }
        public int? PhraseValue3 { get; set; }
        public string DoctorCardNo { get; set; }
        public string MarkColumn { get; set; }
        public string UserImg1 { get; set; }
        public string UserImg2 { get; set; }
        public string DoctorDetail { get; set; }
        public string OrderDoctor { get; set; }
        public bool? IsOralPmd { get; set; }
        public bool? IsXerostomia { get; set; }
        public bool? IsUserLogin { get; set; }
        public string DrugHistory { get; set; }
        public bool? IsNurseTeach { get; set; }
        public bool? IsReSmoke { get; set; }
        public bool? GoogleAuthen { get; set; }
        public string GoogleAccess { get; set; }
        public string GoogleRefresh { get; set; }
        public string GoogleIssued { get; set; }
        public string GoogleTokenType { get; set; }
        public int? GoogleExpiresInSec { get; set; }
        public bool? IsNoRepChkCode { get; set; }
    }
}
