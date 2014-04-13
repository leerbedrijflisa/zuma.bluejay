using Lisa.Zuma.BlueJay.Models;
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
        public async Task<ActionResult> Login(LoginViewModel loginModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "User name or password is not set!");
                return View();
            }

            var rest = new RestClient("http://localhost:14689");
            var request = new RestRequest("/token", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded")
                    .AddParameter("grant_type", "password")
                    .AddParameter("username", loginModel.Username)
                    .AddParameter("password", loginModel.Password);

            var response = await rest.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.Clear();
                ModelState.AddModelError("", "The user name or password is incorrect!");
                return View();
            }

            var tokenResult = await JsonConvert.DeserializeObjectAsync<Dictionary<string, string>>(response.Content);
            var accessToken = "";
            if (tokenResult.TryGetValue("access_token", out accessToken))
            {
                request = new RestRequest("/api/account/userclaims", Method.GET);
                request.AddHeader("Authorization", string.Format("bearer {0}", accessToken));

                var claimResponse = await rest.ExecuteTaskAsync<List<UserClaim>>(request);
                var claims = claimResponse.Data
                    .Select(uc =>
                    {
                        var claim = new Claim(uc.Type, uc.Value, uc.ValueType);
                        claim.Properties.Concat(uc.Properties);

                        return claim;
                    })
                    .ToList();

                var accessTokenClaim = new Claim("http://leerbedrijflisa.nl/zuma/bluejay/token", accessToken);
                claims.Add(accessTokenClaim);

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.Parse(tokenResult[".expires"]).ToUniversalTime()
                }, identity);
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Failed to login. Please, try again.");
                return View();
            }

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