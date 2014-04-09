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
            var userStore = new UserStore<UserData>(new BlueJayContext());
            var userManager = new UserManager<UserData>(userStore);

            var dossier = new DossierData
            {
                Details = new List<DossierDetailData>()
            };

            var parent = new UserData
            {
                Type = "PARENT",
                UserName = "Bert",
                Dossiers = new List<DossierData>()
                {
                    dossier
                }
            };

            var mentor = new UserData
            {
                Type = "MENTOR",
                UserName = "Frank",
                Dossiers = new List<DossierData>()
                {
                    dossier
                }
            };

            userManager.Create(parent, "password123");
            userManager.Create(mentor, "password123");
        }
    }
}
