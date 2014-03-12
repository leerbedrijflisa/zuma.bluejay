using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    [Table("NoteMedias")]
    public class NoteMediaData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MediaLocation { get; set; }

        [Required]
        public int NoteId { get; set; }
        public virtual NoteData Note { get; set; }
    }
}
