namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using Lisa.Zuma.BlueJay.Web.Data.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Lisa.Zuma.BlueJay.Web.Data.BlueJayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Lisa.Zuma.BlueJay.Web.Data.BlueJayContext context)
        {
            //  This method will be called after migrating to the latest version.
            var userStore = new UserStore<UserData>(context);
            var userManager = new UserManager<UserData>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var dossier = new DossierData
            {
                Details = new List<DossierDetailData>(),
                Name = "Martijn"
            };

            var parent = new UserData
            {
                Type = "PARENT",
                UserName = "Bert"
            };

            var mentor = new UserData
            {
                Type = "OTHER",
                UserName = "Frank"
            };

            try
            {
                context.SaveChanges();

                userManager.Create(parent, "password123");
                userManager.Create(mentor, "password123");

                // Add default roles
                roleManager.Create(new IdentityRole("User"));
                roleManager.Create(new IdentityRole("Begeleider"));
                roleManager.Create(new IdentityRole("Parent"));
                roleManager.Create(new IdentityRole("Admin"));

                // Add parent to parent role and user role
                userManager.AddToRole(parent.Id, "User");
                userManager.AddToRole(parent.Id, "Parent");

                userManager.AddToRole(mentor.Id, "User");
                userManager.AddToRole(mentor.Id, "Begeleider");

                dossier.OwnerId = parent.Id;
                parent.Dossiers = new List<DossierData>()
                {
                    dossier
                };

                context.SaveChanges();
            }
            catch { }
        }
    }
}
