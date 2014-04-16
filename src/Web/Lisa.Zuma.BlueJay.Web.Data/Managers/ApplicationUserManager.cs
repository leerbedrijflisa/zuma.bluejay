using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.Web.Data.Managers
{
    public class ApplicationUserManager<TUser> : UserManager<TUser> where TUser : IdentityUser
    {
        public ApplicationUserManager(IUserStore<TUser> store)
            : base(store)
        {
            context = ((UserStore<TUser>)base.Store).Context;
        }

        /// <summary>
        /// Gets all the users available in the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TUser> GetAll()
        {
            return context.Set<TUser>().AsEnumerable();
        }

        public bool Exists(string id)
        {
            return context.Set<TUser>().Count(u => u.Id == id) > 0;
        }

        private DbContext context;
    }
}
