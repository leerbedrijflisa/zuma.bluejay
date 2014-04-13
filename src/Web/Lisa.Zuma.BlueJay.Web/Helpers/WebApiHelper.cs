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

        public Dictionary<string, string> Login(string username, string password)
        {
            return LoginAsync(username, password).Result;
        }

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

        public List<Claim> GetClaims(string accessToken)
        {
            return GetClaimsAsync(accessToken).Result;
        }

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

        private Uri rootUri;
    }
}