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
            var loginResult = await apiHelper.LoginAsync(loginModel.Username, loginModel.Password);

            if (loginResult.ContainsKey("error"))
            {
                ModelState.Clear();
                ModelState.AddModelError("", "The user name or password is incorrect!");
                return View();
            }

            var accessToken = loginResult["access_token"];
            var accessTokenClaim = new Claim("http://leerbedrijflisa.nl/zuma/bluejay/token", accessToken, ClaimValueTypes.String);
            var claims = await apiHelper.GetClaimsAsync(accessToken);
            claims.Add(accessTokenClaim);

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTime.Parse(loginResult[".expires"]).ToUniversalTime()
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