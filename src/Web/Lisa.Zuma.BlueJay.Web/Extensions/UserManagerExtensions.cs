using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lisa.Zuma.BlueJay.Web.Extensions
{
    public static class UserManagerExtensions
    {
        /// <summary>
        /// Gets the currently logged-in user.
        /// </summary>
        /// <typeparam name="TUser">A class that inherits from the IdentityUser class.</typeparam>
        public static TUser GetCurrentUser<TUser>(this UserManager<TUser> userManager) where TUser : IdentityUser
        {
            var userId = GetCurrentUserId();
            return userManager.FindById(userId);
        }

        /// <summary>
        /// Gets the user id of the currently logged-in user.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }
    }
}