using DentistAssistant.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace DentistAssistant.ViewModels
{
    public class QACategorys
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? SeqNo { get; set; }
        public List<QAGroupUnit> QAGroupUnits { get; set; }
    }

    public class QAGroupUnit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? SeqNo { get; set; }
        public List<QAQParentUnit> QAQParentUnits { get; set; }
    }

    public class QAQParentUnit
    {
        public QAQUnit QAQUnit { get; set; }
        public List<QAQUnit> QAQUnits { get; set; }
    }

    public class QAQUnit
    {
        public bool IsChecked { get; set; }
        public string QAAValueDescription { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public bool IsValue { get; set; }
        public string ValueDescription { get; set; }
        public int? SeqNo { get; set; }
        public int? ParentId { get; set; }
    }

    public class Share
    {
        //public string UserNo { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public Patients Patient { get; set; }
        public string ShareType { get; set; }
        public bool IsCheckedImages { get; set; }
        public bool IsCheckedVideos { get; set; }
        public List<ShareEditUnit> Shares { get; set; }
    }

    public class CreateShare
    {
        public string PatId { get; set; }
        public string ValueDescription { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public DateTime CreateDate { get; set; }
        public string ShareTypeId { get; set; }
    }

    public class LastOneShareType
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public ShareUnit Share { get; set; }
    }

    public class ShareUnit
    {
        public int Id { get; set; }
        public string PatId { get; set; }
        public string ValueDescription { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string ShareTypeId { get; set; }
    }

    public class ShareEditUnit
    {
        public int Id { get; set; }
        public string PatId { get; set; }
        public string ValueDescription { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string CreateDate { get; set; }
        public string ShareTypeId { get; set; }
    }
}
