namespace Lisa.Zuma.BlueJay.Web.Data.Migrations
{
    using Lisa.Zuma.BlueJay.Web.Data.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<Lisa.Zuma.BlueJay.Web.Data.BlueJayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Lisa.Zuma.BlueJay.Web.Data.BlueJayContext context)
        {
            //  This method will be called after migrating to the latest version.
            var dossier = new DossierData
            {
                Details = new List<DossierDetailData>()
            };

            var parent = new UserData
            {
                Type = "PARENT",
                Dossiers = new List<DossierData>
                {
                    dossier
                }
            };

            var mentor = new UserData
            {
                Type = "MENTOR",
                Dossiers = new List<DossierData>
                {
                    dossier
                }
            };

            context.Users.AddOrUpdate(
                u => u.Type,
                parent,
                mentor
            );

            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                        DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));

                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }

                System.IO.File.AppendAllLines(@"d:\errors.txt", outputLines);
                
                throw;
            }
        }
    }
}
