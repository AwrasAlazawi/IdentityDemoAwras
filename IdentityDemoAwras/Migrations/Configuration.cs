namespace IdentityDemoAwras.Migrations
{
    using IdentityDemoAwras.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityDemoAwras.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IdentityDemoAwras.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
           var userManager = new ApplicationUserManager(userStore);

            var emails = new[] { "user@lexicon.se", "oras@lexicon.se", "admin@lexicon.se" , "oras2@leicon.se" };
            foreach(var email in emails)
            {
                if (context.Users.Any(u => u.UserName == email)) continue;

                var user = new ApplicationUser { UserName = email, Email = email };
                var result = userManager.Create(user, "foobar");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));

                }
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roleNames = new[] { "Editor", "Admin" };
            foreach(var roleName in roleNames)
            {
                if (context.Roles.Any(r => r.Name == roleName)) continue;

                var role = new IdentityRole { Name = roleName };
                var result = roleManager.Create(role);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));

                }
            }
            var adminUser = userManager.FindByName("admin@lexicon.se");
            userManager.AddToRole(adminUser.Id, "Admin");

            var editorUser = userManager.FindByName("user@lexicon.se");
            userManager.AddToRole(editorUser.Id, "Editor");

            var oras = userManager.FindByName("oras@lexicon.se");
            userManager.AddToRoles(oras.Id, "Admin", "Editor");


        }
    }
}
