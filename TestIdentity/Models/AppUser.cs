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

    public enum Countries
    {
        [Display(Name = "Не указано")]
        NONE,
        [Display(Name = "Англия")]
        ENG,
        [Display(Name = "Франция")]
        FRA,
        [Display(Name = "Россия")]
        RUS
    };
    public class AppUser: IdentityUser
    {
        public Cities City { get; set; }
        public Countries Country { get; set; }

        public void SetCountryGromCity(Cities city)
        {
            switch(city)
            {
                case Cities.LONDON:
                    Country = Countries.ENG;
                    break;
                case Cities.MOSCOW:
                    Country = Countries.RUS;
                    break;
                case Cities.PARIS:
                    Country = Countries.FRA;
                    break;
                default:
                    Country = Countries.NONE;
                    break;
            }
        }
    }
}