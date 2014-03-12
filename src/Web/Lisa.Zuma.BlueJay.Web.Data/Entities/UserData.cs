using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.Web.Data.Entities
{
    [Table("Users")]
    public class UserData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DefaultValue("PARENT")]
        public string Type { get; set; }

        public virtual ICollection<DossierData> Dossiers { get; set; }
    }
}
