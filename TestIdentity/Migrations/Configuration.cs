namespace TestIdentity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using TestIdentity.Infrastructure;
    using TestIdentity.Models;
    
    internal sealed class Configuration : DbMigrationsConfiguration<TestIdentity.Infrastructure.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TestIdentity.Infrastructure.AppIdentityDbContext";
        }

        protected override void Seed(TestIdentity.Infrastructure.AppIdentityDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Administrators";
            string userName = "Admin";
            string password = "Admin11";
            string e_mail = "admin@mail.com";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }

            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = e_mail }, password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            foreach (AppUser usr in userMgr.Users)
            {
                usr.City = Cities.MOSCOW;
            }

            context.SaveChanges();
        }
    }
}
