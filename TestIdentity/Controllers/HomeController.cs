using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using TestIdentity.Models;
using TestIdentity.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Reflection;
using System.Threading.Tasks;

namespace TestIdentity.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("Пользователь", HttpContext.User.Identity.Name);
            dict.Add("Аутентифицирован?", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Тип аутентификации", HttpContext.User.Identity.AuthenticationType);
            dict.Add("В роли User", HttpContext.User.IsInRole("User"));

            return dict;
        }

        [Authorize(Roles = "User")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }

        //метод для загрузки названия элемента перечисления
        [NonAction]
        public static string GetCityName<TEnum>(TEnum item)
            where TEnum: struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("Тип TEnum должен быть перечислением");
            }
            else
            {
                return item.GetType()
                    .GetMember(item.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    .Name;
            }
        }

        [Authorize]
        public ActionResult UserProps()
        {
            return View(CurrentUser);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UserProps(Cities city)
        {
            AppUser user = CurrentUser;
            user.City = city;
            user.SetCountryGromCity(city);

            await UserManager.UpdateAsync(user);

            return View(user);
        }
    }
}