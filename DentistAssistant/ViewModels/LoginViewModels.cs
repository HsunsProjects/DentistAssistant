using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DentistAssistant.ViewModels
{
    public class LoginIndexViewModel
    {
        public IEnumerable<SelectListItem> Users { get; set; }
        public string UserNo { get; set; }
        public string Password { get; set; }
    }

    //public class SessionUserViewModel
    //{
    //    public string UserNo { get; set; }
    //    public string UserName { get; set; }
    //}

    public class CookieUserViewModel
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
    }
}
