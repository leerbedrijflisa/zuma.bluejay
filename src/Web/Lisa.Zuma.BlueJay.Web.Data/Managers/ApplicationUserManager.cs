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

        public bool Exists(string id)
        {
            return context.Set<TUser>().Count(u => u.Id == id) > 0;
        }

        /// <summary>
        /// Removes the user with the <paramref name="userId"/> from a role specified by the <paramref name="roleId"/>.
        /// This method should be use instead of the default RemoveFromRoleAsync because of a bug in the Identity framework.
        /// This method runs asynchronous.
        /// </summary>
        /// <param name="userId">The id of the user to remove the role from.</param>
        /// <param name="roleId">The id of the role that should be removed from the user specified by <paramref name="userId"/></param>.
        /// <returns></returns>
        public async Task<IdentityResult> RemoveFromRoleByIdAsync(string userId, string roleId)
        {
            return await RemoveFromRoleByNameOrIdAsync(userId, roleId, true);
        }

        /// <summary>
        /// Removed the user with the <paramref name="userId"/> from a role specified by the <paramref name="roleName"/>.
        /// This method should be used instead of the default RemoveFromRoleAsync because of a bug in the Identity framework.
        /// This method runs asynchronous.
        /// </summary>
        /// <param name="userId">The id of the user to remove the role from.</param>
        /// <param name="roleName">The name of the role that should be removed from the user specified by <paramref name="userId"/></param>.
        /// <returns></returns>
        public async Task<IdentityResult> RemoveFromRoleByNameAsync(string userId, string roleName)
        {
            return await RemoveFromRoleByNameOrIdAsync(userId, roleName, false);
        }

        private async Task<IdentityResult> RemoveFromRoleByNameOrIdAsync(string userId, string search, bool byRoleId)
        {
            var user = await Users.FirstOrDefaultAsync(u => u.Id == userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("User Id not found!");
            }

            var roleSet = GetRoleSet();
            IdentityResult identityResult;
            IdentityRole identityRole;

            if (byRoleId)
            {
                identityRole = await roleSet.SingleOrDefaultAsync(r => r.Id == search).ConfigureAwait(false);                
            }
            else
            {
                identityRole = await roleSet.SingleOrDefaultAsync(r => r.Name.ToUpper() == search.ToUpper()).ConfigureAwait(false);
            }

            if (identityRole != null)
            {
                var userRole = user.Roles.FirstOrDefault(r => r.RoleId == identityRole.Id);
                if (userRole != null)
                {
                    user.Roles.Remove(userRole);
                    identityRole.Users.Remove(userRole);
                }

                identityResult = await UpdateAsync(user).ConfigureAwait(false);
            } 
            else
            {
                identityResult = new IdentityResult(string.Format("The role with name or id \"{0}\" could not be found!", search));
            }
            
            return identityResult;
        }

        private DbSet<IdentityRole> GetRoleSet()
        {
            return context.Set<IdentityRole>();
        }

        private DbContext context;
    }
}
