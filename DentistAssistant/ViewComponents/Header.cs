using DentistAssistant.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DentistAssistant.ViewComponents
{
    public class Header : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = new CookieUserViewModel()
            {
                UserNo = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserName = HttpContext.User.FindFirst("DisplayName").Value
            };
            //var user = await HttpContext.Session.GetAsync<SessionUserViewModel>("User");
            return await Task.FromResult((IViewComponentResult)View("Default", user));
        }
    }
}
