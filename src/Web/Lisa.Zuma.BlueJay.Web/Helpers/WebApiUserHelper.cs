using Lisa.Zuma.BlueJay.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Lisa.Zuma.BlueJay.Web.Helpers
{
    public class WebApiUserHelper : WebApiHelper
    {
        public WebApiUserHelper(string rootUri)
            : base(rootUri)
        {
        }

        /// <summary>
        /// Gets all users available on the WebApi.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            return GetAllAsync().Result;
        }

        /// <summary>
        /// Gets all users available on the WebApi in an asynchronous manner.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var request = new RestRequest("/api/user", Method.GET);
            request.AddParameter(AuthorizationHeader);

            var response = await Client.ExecuteTaskAsync<List<User>>(request);
            return response.Data;
        }

        /// <summary>
        /// Gets a user by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Get(string id)
        {
            return GetAsync(id).Result;
        }

        /// <summary>
        /// Gets a user by its id in an asynchronous manner.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns></returns>
        public async Task<User> GetAsync(string id)
        {
            var request = new RestRequest("/api/user/{id}", Method.GET);
            request.AddParameter(AuthorizationHeader)
                .AddUrlSegment("id", id);

            var response = await Client.ExecuteTaskAsync<User>(request);
            return response.Data;
        }


        /// <summary>
        /// Adds the user to the given role and adds the role when it does not exist.
        /// </summary>
        /// <param name="id">The user id</param>
        /// <param name="role">The role to add the user to</param>
        /// <returns></returns>
        public List<string> AddToRole(string id, string role)
        {
            return AddToRoleAsync(id, role).Result;
        }

        /// <summary>
        /// Adds the user to the given role and adds the role when it does not exist in an asynchronous manner.
        /// </summary>
        /// <param name="id">The user id</param>
        /// <param name="role">The role to add the user to</param>
        /// <returns></returns>
        public async Task<List<string>> AddToRoleAsync(string id, string role)
        {
            var request = new RestRequest("/api/user/addrole/{id}/{role}", Method.POST);
            request.AddParameter(AuthorizationHeader)
                .AddUrlSegment("id", id)
                .AddUrlSegment("role", role);

            var response = await Client.ExecuteTaskAsync<List<string>>(request);
            return response.Data;
        }
    }
}