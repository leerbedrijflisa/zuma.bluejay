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

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NoteMedia> NoteMedia { get; set; }
        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<Profile> Profiles { get; set; }
    }
}
