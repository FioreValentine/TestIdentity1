using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Models
{
    public enum Cities
    {
        [Display(Name = "Лондон")]
        LONDON,
        [Display(Name = "Париж")]
        PARIS,
        [Display(Name = "Москва")]
        MOSCOW
    };
    public class AppUser: IdentityUser
    {
        public Cities City { get; set; }
    }
}