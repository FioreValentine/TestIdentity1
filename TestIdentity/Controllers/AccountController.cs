using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using TestIdentity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TestIdentity.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;

namespace TestIdentity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            AppUser user = await UserManager.FindAsync(details.Name, details.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Некорректный логин или пароль");
            }
            else //создание файла куки
            {
                //идентификация пользователя
                ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, 
                                            DefaultAuthenticationTypes.ApplicationCookie);
                //удаление старых куков
                AuthManager.SignOut();
                //создание куков. IsPersistent - сохранение между сессиями
                AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false },
                                                            ident);
                return Redirect(returnUrl);
            }

            return View(details);
        }

        public ActionResult LogOut()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //возвращает экземпляр класса AppUserManager
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        
    }
}