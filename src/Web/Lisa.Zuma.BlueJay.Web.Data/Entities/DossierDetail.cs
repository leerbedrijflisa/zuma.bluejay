using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    public class DossierDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Contents { get; set; }

        [Required]
        public int DossierId { get; set; }
        public virtual Dossier Dossier { get; set; }
    }
}
