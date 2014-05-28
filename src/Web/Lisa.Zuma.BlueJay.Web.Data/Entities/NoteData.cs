using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    [Table("Notes")]
    public class NoteData
    {
        public NoteData()
        {
            Media = new List<NoteMediaData>();
        }

        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        [Required]
        [Column(TypeName="datetime2")]
        public DateTime DateCreated { get; set; }

        [Required]
        public int DossierId { get; set; }

        [Required]
        [MaxLength(128)]
        public string PosterId { get; set; }

        public virtual DossierData Dossier { get; set; }
        public virtual ICollection<NoteMediaData> Media { get; set; }
    }
}
