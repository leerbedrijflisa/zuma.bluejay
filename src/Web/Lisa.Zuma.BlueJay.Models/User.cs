using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.Models
{
    public class User
    {
        public User()
        {
            Roles = new List<UserRole>();
        }

        [Display(Name = "Id")]
        public string Id { get; set; }
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
        public bool Deleted { get; set; }
        public List<UserRole> Roles { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", Id, UserName, Type);
        }
    }

    public class UserRole 
    {
        [Display(Name = "Id")]
        public string Id { get; set; }
        [Display(Name = "Rol")]
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
