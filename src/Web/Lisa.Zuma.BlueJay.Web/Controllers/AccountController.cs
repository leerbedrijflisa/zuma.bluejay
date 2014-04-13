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
    public class AccountController : Controller
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

            var apiHelper = new WebApiHelper("http://localhost:14689");
            var identity = await apiHelper.LoginAndGetIdentityAsync(loginModel.Username, loginModel.Password, DefaultAuthenticationTypes.ApplicationCookie);
            if (identity == null)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "The user name or password is incorrect!");
                return View();
            }

            // Find the expire claim that specifies the timestamp on which the access token will
            // be invalid. Extract the date for use with the AuthenticationManager.SignIn() method,
            // then delete the expireClaim.
            var expireClaim = identity.FindFirst("http://leerbedrijflisa.nl/zuma/bluejay/expire");
            var expireDate = DateTime.Parse(expireClaim.Value).ToUniversalTime();
            identity.RemoveClaim(expireClaim);

            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = expireDate
            }, identity);

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