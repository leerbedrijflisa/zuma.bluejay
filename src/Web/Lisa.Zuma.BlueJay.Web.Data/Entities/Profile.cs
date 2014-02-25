using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    public class Profile
    {
        [Key, ForeignKey("Dossier")]
        public int Id { get; set; }

        public virtual Dossier Dossier { get; set; }
    }
}
