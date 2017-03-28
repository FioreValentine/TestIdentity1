using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using TestIdentity.Models;
using System.Threading.Tasks;

namespace TestIdentity.Infrastructure
{
    public class CustomPasswordValidator: PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string pass)
        {
            //запуск базовой реализации
            IdentityResult res = await base.ValidateAsync(pass);

            if (pass.Contains("12345"))
            {
                var errors = res.Errors.ToList();
                errors.Add("Пароль не должен содержать последовательности чисел");
                //объединение ошибок из базовой проверки с пользовательской
                res = new IdentityResult(errors); 
            }

            return res;
        }
    }
}