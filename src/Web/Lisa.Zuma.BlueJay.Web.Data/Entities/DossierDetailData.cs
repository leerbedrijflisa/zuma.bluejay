using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    [Table("DossierDetails")]
    public class DossierDetailData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Contents { get; set; }

        [Required]
        public int DossierId { get; set; }
        public virtual DossierData Dossier { get; set; }
    }
}
