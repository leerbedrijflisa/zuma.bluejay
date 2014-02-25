using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    public class Dossier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Profile Profile { get; set; }

        public virtual ICollection<User> Watchers { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}
