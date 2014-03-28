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
        public static TUser GetCurrentUser<TUser>(this UserManager<TUser> userManager) where TUser : IdentityUser
        {
            var userId = GetCurrentUserId();
            return userManager.FindById(userId);
        }

        public static string GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }
    }
}