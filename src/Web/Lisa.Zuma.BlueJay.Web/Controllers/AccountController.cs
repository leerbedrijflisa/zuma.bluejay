using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Helpers;
using Lisa.Zuma.BlueJay.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                ModelState.AddModelError("", loginResult.ErrorDescription);

                return View();
            }

            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = loginResult.TokenResult.Expires
            }, loginResult.Identity);

            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var registerResult = await WebApiHelper.RegisterUserAsync(userModel);
            if (!registerResult.Success)
            {
                ModelState.Clear();

                foreach (var error in registerResult.Errors)
                {
                    foreach (var message in error.Value)
                    {
                        ModelState.AddModelError(error.Key, message);
                    }
                }

                return View();
            }

            var loginModel = new LoginViewModel 
            {
                Username = userModel.UserName,
                Password = userModel.Password
            };
            
            var loginResult = await Login(loginModel, Url.Action("Index", "Home"));
            // Return when login failed
            if (loginResult is ViewResult)
            {
                return loginResult;
            }

            if (userModel.IsParent)
            {
                var dossier = new Dossier
                {
                    Name = userModel.DossierName,
                    OwnerId = registerResult.User.Id
                };

                var dossierResult = await webApiDossierHelper.CreateAsync(registerResult.User.Id, dossier);
            }

            return RedirectToAction("Index", "Home");
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

        private WebApiDossierHelper webApiDossierHelper = new WebApiDossierHelper(ConfigurationManager.AppSettings["WebApiBaseUrl"]);
	}
}