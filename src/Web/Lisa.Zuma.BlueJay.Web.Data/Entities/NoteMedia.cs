using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    public class NoteMedia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MediaLocation { get; set; }

        [Required]
        public int NoteId { get; set; }
        public virtual Note Note { get; set; }
    }
}
