using System;
using System.Collections.Generic;
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

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public List<UserRole> Roles { get; set; }
    }

    public class UserRole 
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
