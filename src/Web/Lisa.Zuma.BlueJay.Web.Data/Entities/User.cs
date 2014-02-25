using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DefaultValue("PARENT")]
        public string Type { get; set; }

        public virtual ICollection<Dossier> Dossiers { get; set; }
    }
}
