using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Models;
using Newtonsoft.Json.Linq;
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
    public class WebApiHelper
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
        /// Tries to log the user in on the WebApi in an asynchronous manner.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public async Task<WebApiLoginResult> LoginAsync(string username, string password)
        {
            var request = new RestRequest("/token", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddParameter("grant_type", "password")
                .AddParameter("username", username)
                .AddParameter("password", password);

            var response = await Client.ExecuteTaskAsync<Dictionary<string, string>>(request);
            var loginResult = new WebApiLoginResult(response.Data);

            return loginResult;
        }

        /// <summary>
        /// Gets the available claims from the WebApi in an asynchronous manner.
        /// </summary>
        /// <param name="accessToken">The access token received after a successful login.</param>
        /// <returns></returns>
        public async Task<List<Claim>> GetClaimsAsync(string accessToken)
        {
            var request = new RestRequest("/api/account/userclaims", Method.GET);
            request.AddHeader("Authorization", string.Format("bearer {0}", accessToken));

            var response = await Client.ExecuteTaskAsync<List<UserClaim>>(request);
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

        public async Task<RegisterUserResult> RegisterUserAsync(RegisterUserViewModel userModel)
        {
            var request = new RestRequest("/api/account/register", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddObject(userModel);

            var result = new RegisterUserResult();

            var response = await Client.ExecuteTaskAsync(request);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                // Deserialize to JObject for custom processing.
                var obj = await Newtonsoft.Json.JsonConvert.DeserializeObjectAsync<JObject>(response.Content);

                // Get the message property of the error result
                result.Message = obj.Property("message").Value.Value<string>();

                // Convert the modelState property to a JObject and convert it to a 
                // Dictionary with one key with multiple values
                result.Errors = ((JObject)obj.Property("modelState").Value)
                                .ToObject<Dictionary<string, IList<string>>>();

                result.Success = false;
            }
            else
            {
                result.User = await Newtonsoft.Json.JsonConvert.DeserializeObjectAsync<User>(response.Content);
                result.Success = true;
            }

            return result;
        }

        protected Uri RootUri
        {
            get
            {
                return this.rootUri;
            }
        }

        protected RestClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new RestClient(this.rootUri.AbsoluteUri);
                }

                return client;
            }
        }

        protected Parameter AuthorizationHeader
        {
            get
            {
                var principal = HttpContext.Current.User as ClaimsPrincipal;
                if (principal == null && principal.HasClaim(c => c.Type == "http://leerbedrijflisa.nl/zuma/bluejay/token"))
                {
                    return null;
                }

                var parameter = new Parameter();
                parameter.Type = ParameterType.HttpHeader;
                parameter.Name = "Authorization";
                parameter.Value = string.Format("bearer {0}", principal.FindFirst("http://leerbedrijflisa.nl/zuma/bluejay/token").Value);

                return parameter;
            }
        }

        private Uri rootUri;
        private RestClient client;
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
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorUri { get; set; }
        public AccessTokenModel TokenResult { get; set; }

        private void Initialize(Dictionary<string, string> tokenResult)
        {
            if (tokenResult.ContainsKey("error"))
            {
                Success = false;
                Error = tokenResult["error"];
                ErrorDescription = tokenResult["error_description"];

                if (tokenResult.ContainsKey("error_uri"))
                {
                    ErrorUri = tokenResult["error_uri"];
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
            Error = loginResult.Error;
            ErrorDescription = loginResult.ErrorDescription;
            ErrorUri = loginResult.ErrorUri;
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

    public class RegisterUserResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, IList<string>> Errors { get; set; }
        public User User { get; set; }
    }
}