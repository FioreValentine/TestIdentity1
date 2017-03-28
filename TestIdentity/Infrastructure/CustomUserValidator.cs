using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using TestIdentity.Models;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TestIdentity.Infrastructure
{
    public class CustomUserValidator: UserValidator<AppUser>
    {
        public CustomUserValidator(AppUserManager manager) : base(manager) { }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult res = await base.ValidateAsync(user);

            if (!user.Email.ToLower().EndsWith("@mail.com")/*Regex.IsMatch(user.Email.ToLower(),@"\S+@\S+\.\S+")*/)
            {
                var errors = res.Errors.ToList();
                errors.Add("Запрещены адреса, отличные от mail.com");
                res = new IdentityResult(errors);
            }

            return res;
        }
    }
}