using Eticaret.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Eticaret2.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            // Roller
            if (!context.Roles.Any(i=>i.Name=="admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var role = new ApplicationRole("admin", "yönetici rolü");

                manager.Create(role);
            }
            
            if (!context.Roles.Any(i=>i.Name=="user"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var role = new ApplicationRole("user", "kullanıcı rolü");

                manager.Create(role);
            }

            // User

            if (!context.Users.Any(i => i.Name == "Dogukan"))
            {
                var store = new UserStore<IdentityUser>(context);
                var manager = new UserManager<IdentityUser>(store);

                var user = new ApplicationUser() { Name = "Dogukan", Surname = "Sandikci", UserName = "dogukansan", Email = "dogukansandikci.53.53@gmail.com" };

                manager.Create(user, "12345");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }

            if (!context.Users.Any(i => i.Name == "Abuzer"))
            {
                var store = new UserStore<IdentityUser>(context);
                var manager = new UserManager<IdentityUser>(store);

                var user = new ApplicationUser() { Name = "Abuzer", Surname = "Kömürcü", UserName = "ordinaryüs", Email = "abuzer_komurcu@gmail.com" };

                manager.Create(user, "12345");
                manager.AddToRole(user.Id, "user");
            }








            base.Seed(context);
        }
            
    }
}