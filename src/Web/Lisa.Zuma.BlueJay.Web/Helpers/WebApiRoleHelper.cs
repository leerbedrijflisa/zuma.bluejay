using Lisa.Zuma.BlueJay.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Lisa.Zuma.BlueJay.Web.Helpers
{
    public class WebApiRoleHelper : WebApiHelper
    {
        public WebApiRoleHelper(string rootUri)
            : base(rootUri)
        {
        }

        public List<UserRole> GetRoles()
        {
            return GetRolesAsync().Result;
        }

        public async Task<List<UserRole>> GetRolesAsync()
        {
            var request = new RestRequest(roleUri, Method.GET);
            request.AddParameter(AuthorizationHeader);

            var result = await Client.ExecuteTaskAsync<List<UserRole>>(request);
            return result.Data;
        }

        public UserRole AddRole(UserRole role)
        {
            return AddRoleAsync(role).Result;
        }

        public async Task<UserRole> AddRoleAsync(UserRole role)
        {
            var request = new RestRequest(roleUri, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(AuthorizationHeader)
                .AddBody(role);

            var result = await Client.ExecuteTaskAsync<UserRole>(request);
            return result.Data;
        }

        public List<UserRole> AddRoles(IEnumerable<UserRole> roles)
        {
            return AddRolesAsync(roles).Result;
        }

        public async Task<List<UserRole>> AddRolesAsync(IEnumerable<UserRole> roles)
        {
            var result = new List<UserRole>();

            foreach (var role in roles)
            {
                result.Add(await AddRoleAsync(role));
            }

            return result;
        }

        private const string roleUri = "/api/role";
    }
}