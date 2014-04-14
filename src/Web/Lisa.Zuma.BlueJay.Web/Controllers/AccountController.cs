using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Helpers;
using Lisa.Zuma.BlueJay.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginModel, string returnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "User name or password is not set!");
                return View();
            }
            
            var loginResult = await WebApiHelper.LoginAndGetIdentityAsync(loginModel.Username, loginModel.Password, DefaultAuthenticationTypes.ApplicationCookie);
            if (!loginResult.Success)
            {
                ModelState.Clear();
                loginResult.Errors.ForEach(err =>
                {
                    if (!err.Contains("invalid_grant"))
                    {
                        ModelState.AddModelError("", err);
                    }
                });

                return View();
            }

            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = loginResult.TokenResult.Expires
            }, loginResult.Identity);

            return Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(User.Identity.AuthenticationType);

            return RedirectToAction("About", "Home");
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
	}
}