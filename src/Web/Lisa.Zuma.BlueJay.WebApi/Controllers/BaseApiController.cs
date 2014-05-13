using Lisa.Zuma.BlueJay.Web.Data;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Data.Managers;
using Lisa.Zuma.BlueJay.WebApi.Extensions;
using Lisa.Zuma.BlueJay.WebApi.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Lisa.Zuma.BlueJay.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// Gives access to all entities in the database.
        /// </summary>
        protected IUnitOfWork UoW
        {
            get
            {
                if (uow == null)
                {
                    // By default, the UserManager will call Db.SaveChanges when any data belonging to the user is changed.
                    // This may or may not conflict with pending changes and can cause data corruption or crashes.
                    // Set the parameter "autoSaveStoreChanges" to true, to enable automatic saving after issueing calls to
                    // the UserManager. If not, you must call UoW.Save() when performing basic CRUD operations.
                    uow = new UnitOfWork(true);
                }

                return uow;
            }
        }

        /// <summary>
        /// Provides access to the built-in UserManager.
        /// </summary>
        protected ApplicationUserManager<UserData> UserManager
        {
            get
            {
                return UoW.UserManager;
            }
        }

        /// <summary>
        /// Provides access to the built-in RoleManager.
        /// </summary>
        protected RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return UoW.RoleManager;
            }
        }

        /// <summary>
        /// Provides access to the StorageHelper.
        /// </summary>
        protected StorageHelper StorageHelper
        {
            get
            {
                if (storageHelper == null)
                {
                    storageHelper = new StorageHelper("ZumaBlueJayStorageConnectionString", "bluejay");
                }

                return storageHelper;
            }
        }

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
            base.Initialize(controllerContext);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private IUnitOfWork uow;
        private StorageHelper storageHelper;
    }
}