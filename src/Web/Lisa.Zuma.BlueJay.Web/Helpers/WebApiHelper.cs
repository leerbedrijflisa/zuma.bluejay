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
        public WebApiLoginResult Login(string username, string password)
        {
            return LoginAsync(username, password).Result;
        }

        /// <summary>
        /// Tries to log the user in on the WebApi in an asynchronous manner.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public async Task<WebApiLoginResult> LoginAsync(string username, string password)
        {
            var client = new RestClient(rootUri.AbsoluteUri);
            var request = new RestRequest("/token", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddParameter("grant_type", "password")
                .AddParameter("username", username)
                .AddParameter("password", password);

            var response = await client.ExecuteTaskAsync<Dictionary<string, string>>(request);
            var loginResult = new WebApiLoginResult(response.Data);

            return loginResult;
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
        public WebApiLoginIdentityResult LoginAndGetIdentity(string username, string password, string authenticationType)
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
        public async Task<WebApiLoginIdentityResult> LoginAndGetIdentityAsync(string username, string password, string authenticationType)
        {
            var loginResult = await LoginAsync(username, password);
            var result = new WebApiLoginIdentityResult(loginResult);

            if (!result.Success)
            {
                return result;
            }

            var accessTokenClaim = new Claim("http://leerbedrijflisa.nl/zuma/bluejay/token", loginResult.TokenResult.AccessToken, ClaimValueTypes.String);
            var claims = await GetClaimsAsync(loginResult.TokenResult.AccessToken);

            var identity = new ClaimsIdentity(claims, authenticationType);
            identity.AddClaim(accessTokenClaim);

            result.Identity = identity;
            return result;
        }

        private Uri rootUri;
    }

    public class WebApiLoginResult
    {
        public WebApiLoginResult(Dictionary<string, string> tokenResult)
        {
            Initialize(tokenResult);
        }

        public WebApiLoginResult(WebApiLoginResult loginResult)
        {
            Initialize(loginResult);
        }

        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public AccessTokenModel TokenResult { get; set; }

        private void Initialize(Dictionary<string, string> tokenResult)
        {
            if (tokenResult.ContainsKey("error"))
            {
                Success = false;
                Errors = new List<string>();

                foreach (var item in tokenResult)
                {
                    Errors.Add(item.Value);
                }
            }
            else
            {
                Success = true;
                TokenResult = new AccessTokenModel(tokenResult);
            }
        }

        private void Initialize(WebApiLoginResult loginResult)
        {
            Success = loginResult.Success;
            Errors = loginResult.Errors;
            TokenResult = loginResult.TokenResult;
        }
    }

    public class WebApiLoginIdentityResult : WebApiLoginResult
    {
        public WebApiLoginIdentityResult(WebApiLoginResult loginResult)
            : base(loginResult)
        {
        }

        public ClaimsIdentity Identity { get; set; }
    }

    public class AccessTokenModel
    {
        public AccessTokenModel(Dictionary<string, string> tokenResult)
        {
            Initialize(tokenResult);
        }

        public DateTime Expires { get; private set; }
        public DateTime Issued { get; private set; }
        public string AccessToken { get; private set; }
        public int ExpiresIn { get; private set; }
        public string TokenType { get; private set; }
        public string UserName { get; private set; }

        private void Initialize(Dictionary<string, string> tokenResult)
        {
            Expires = DateTime.Parse(tokenResult[".expires"]);
            Issued = DateTime.Parse(tokenResult[".issued"]);
            AccessToken = tokenResult["access_token"];
            ExpiresIn = int.Parse(tokenResult["expires_in"]);
            TokenType = tokenResult["token_type"];
            UserName = tokenResult["userName"];
        }
    }
}