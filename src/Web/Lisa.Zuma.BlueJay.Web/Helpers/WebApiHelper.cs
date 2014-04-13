using Lisa.Zuma.BlueJay.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Lisa.Zuma.BlueJay.Web.Helpers
{
    public sealed class WebApiHelper
    {
        public WebApiHelper(Uri rootUri)
        {
            this.rootUri = rootUri;
        }

        public WebApiHelper(string root)
            : this(new Uri(root))
        {
        }

        /// <summary>
        /// Tries to log the user in on the WebApi.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public Dictionary<string, string> Login(string username, string password)
        {
            return LoginAsync(username, password).Result;
        }

        /// <summary>
        /// Tries to log the user in on the WebApi in an asynchronous manner.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> LoginAsync(string username, string password)
        {
            var client = new RestClient(rootUri.AbsoluteUri);
            var request = new RestRequest("/token", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddParameter("grant_type", "password")
                .AddParameter("username", username)
                .AddParameter("password", password);

            var response = await client.ExecuteTaskAsync<Dictionary<string,string>>(request);
            return response.Data;            
        }

        /// <summary>
        /// Gets the available claims from the WebApi.
        /// </summary>
        /// <param name="accessToken">The access token received after a successful login.</param>
        /// <returns></returns>
        public List<Claim> GetClaims(string accessToken)
        {
            return GetClaimsAsync(accessToken).Result;
        }

        /// <summary>
        /// Gets the available claims from the WebApi in an asynchronous manner.
        /// </summary>
        /// <param name="accessToken">The access token received after a successful login.</param>
        /// <returns></returns>
        public async Task<List<Claim>> GetClaimsAsync(string accessToken)
        {
            var client = new RestClient(rootUri.AbsoluteUri);
            var request = new RestRequest("/api/account/userclaims", Method.GET);
            request.AddHeader("Authorization", string.Format("bearer {0}", accessToken));

            var response = await client.ExecuteTaskAsync<List<UserClaim>>(request);
            var claims = response.Data
                .Select(uc =>
                {
                    var claim = new Claim(uc.Type, uc.Value, uc.ValueType);
                    claim.Properties.Concat(uc.Properties);

                    return claim;
                })
                .ToList();

            return claims;
        }

        /// <summary>
        /// Tries to log the user in on the WebApi, retrieve a set of claims and transform them into a ClaimsIdentity.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <param name="authenticationType">The authentication type used in the local application</param>
        /// <returns></returns>
        public ClaimsIdentity LoginAndGetIdentity(string username, string password, string authenticationType)
        {
            return LoginAndGetIdentityAsync(username, password, authenticationType).Result;
        }

        /// <summary>
        /// Tries to log the user in on the WebApi, retrieve a set of claims and transform them into a ClaimsIdentity
        /// in an asynchronous manner.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <param name="authenticationType">The authentication type used in the local application</param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> LoginAndGetIdentityAsync(string username, string password, string authenticationType)
        {
            var loginResult = await LoginAsync(username, password);
            if (loginResult.ContainsKey("error"))
            {
                return null;
            }

            var accessToken = loginResult["access_token"];
            var accessTokenClaim = new Claim("http://leerbedrijflisa.nl/zuma/bluejay/token", accessToken, ClaimValueTypes.String);
            var claims = await GetClaimsAsync(accessToken);

            var identity = new ClaimsIdentity(claims, authenticationType);
            identity.AddClaim(accessTokenClaim);

            var expireClaim = new Claim("http://leerbedrijflisa.nl/zuma/bluejay/expire", loginResult[".expires"], ClaimValueTypes.DateTime);
            identity.AddClaim(expireClaim);

            return identity;
        }

        private Uri rootUri;
    }
}