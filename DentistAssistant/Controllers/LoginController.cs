using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DentistAssistant.Extensions;
using DentistAssistant.Models;
using DentistAssistant.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentistAssistant.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index(string pageInfo = null)
        {
            //HttpContext.Session.Clear();

            //LoginInfo.SetLoginUser(string.Empty, string.Empty);
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            using (var def = new DoctorContext())
            {
                var users = new LoginIndexViewModel()
                {
                    Users = (from u in def.Users
                             select new SelectListItem()
                             {
                                 Text = u.UserName,
                                 Value = u.UserNo,
                                 Selected = false
                             }).ToList()
                };
                if(!string.IsNullOrEmpty(pageInfo))
                {
                    ModelState.AddModelError("PageInfo", pageInfo);
                }
                return View(users);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginIndexViewModel loginIndexViewModel)
        {
            using (var def = new DoctorContext())
            {
                if (loginIndexViewModel.UserNo != null)
                {
                    var user = await def.Users.FindAsync(loginIndexViewModel.UserNo);
                    string inputPassword = string.IsNullOrEmpty(loginIndexViewModel.Password) ? string.Empty : loginIndexViewModel.Password;
                    string dbPassword = string.IsNullOrEmpty(user.Pass) ? string.Empty : user.Pass;
                    if (inputPassword.Equals(dbPassword))
                    {
                        //SessionUserViewModel sessionUserViewModel = new SessionUserViewModel()
                        //{
                        //    UserNo = user.UserNo,
                        //    UserName = user.UserName
                        //};
                        //await HttpContext.Session.SetAsync("User", sessionUserViewModel);

                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, user.UserNo),
                                new Claim("DisplayName", user.UserName)
                            };

                        var claimsIdentity = new ClaimsIdentity(
                            claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        //LoginInfo.SetLoginUser(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value, claimsIdentity.FindFirst("DisplayName").Value);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "密碼錯誤");
                    }
                }
                else
                {
                    ModelState.AddModelError("Account", "請選擇帳號");
                }
                loginIndexViewModel.Users = (from u in def.Users
                                             select new SelectListItem()
                                             {
                                                 Text = u.UserName,
                                                 Value = u.UserNo,
                                                 Selected = false
                                             }).ToList();
                return View(loginIndexViewModel);
            }
        }
    }
}