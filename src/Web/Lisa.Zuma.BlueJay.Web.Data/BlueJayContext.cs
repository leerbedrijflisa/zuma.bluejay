using Lisa.Zuma.BlueJay.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.Web.Data
{
    public class BlueJayContext : DbContext
    {
        public BlueJayContext()
            : this("DefaultConnection")
        {
        }

        public BlueJayContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<NoteData> Notes { get; set; }
        public DbSet<UserData> Users { get; set; }
        public DbSet<NoteMediaData> NoteMedia { get; set; }
        public DbSet<DossierData> Dossiers { get; set; }
        public DbSet<DossierDetailData> DossierDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<UserData>()
                .HasMany<DossierData>(u => u.Dossiers)
                .WithMany(d => d.Watchers)
                .Map(p =>
                {
                    p.MapLeftKey("UserId");
                    p.MapRightKey("DossierId");
                    p.ToTable("UserDossiers");
                });
        }
    }
}
