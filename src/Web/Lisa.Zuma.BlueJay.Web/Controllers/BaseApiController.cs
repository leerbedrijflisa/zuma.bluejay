using Lisa.Zuma.BlueJay.Web.Data;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Extensions;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        protected BlueJayContext Db { get; private set; }
        protected UserManager<UserData> UserManager { get; private set; }

        /// <summary>
        /// Gets the currently logged-in user for this request.
        /// When used in a controller action which does not require authorization,
        /// a null check must be used before using this variable.
        /// </summary>
        protected UserData CurrentUser
        {
            get
            {
                return UserManager.GetCurrentUser();
            }
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Db = new BlueJayContext("DefaultConnection");
            UserManager = Startup.UserManagerFactory();

            base.Initialize(controllerContext);
        }

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }
    }
}