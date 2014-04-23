using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    [Table("Dossiers")]
    public class DossierData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual ICollection<DossierDetailData> Details { get; set; }
        public virtual ICollection<UserData> Watchers { get; set; }
        public virtual ICollection<NoteData> Notes { get; set; }
    }
}
