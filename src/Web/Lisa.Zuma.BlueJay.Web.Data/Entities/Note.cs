using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        [Required]
        public int DossierId { get; set; }

        public virtual Dossier Dossier { get; set; }
        public virtual ICollection<NoteMedia> Media { get; set; }
    }
}
