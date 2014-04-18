using Lisa.Zuma.BlueJay.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Lisa.Zuma.BlueJay.Web.Helpers
{
    public class WebApiDossierHelper : WebApiHelper
    {
        public WebApiDossierHelper(string rootUri)
            : base(rootUri)
        {
        }

        /// <summary>
        /// Gets all the dossier associated with the currently logged in user.
        /// This method executes asynchronous.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Dossier> GetAll()
        {
            return GetAllAsync().Result;
        }

        /// <summary>
        /// Gets all the dossier associated with the currently logged in user.
        /// This method executes asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Dossier>> GetAllAsync()
        {
            var request = new RestRequest(dossierUri);
            request.AddParameter(AuthorizationHeader);

            var response = await Client.ExecuteTaskAsync<List<Dossier>>(request);
            return response.Data;
        }

        public Dossier Get(int id)
        {
            return GetAsync(id).Result;
        }

        public async Task<Dossier> GetAsync(int id)
        {
            var request = new RestRequest(dossierUri + "{id}");
            request.AddParameter(AuthorizationHeader)
                .AddUrlSegment("id", id.ToString());

            var response = await Client.ExecuteTaskAsync<Dossier>(request);
            return response.Data;
        }

        public Dossier Create(string userId, Dossier dossier)
        {
            return CreateAsync(userId, dossier).Result;
        }

        public async Task<Dossier> CreateAsync(string userId, Dossier dossier)
        {
            var request = new RestRequest(dossierUri + "{userId}", Method.POST);
            request.AddParameter(AuthorizationHeader).RequestFormat = DataFormat.Json;
            request.AddUrlSegment("userId", userId)
                .AddBody(dossier);

            var response = await Client.ExecuteTaskAsync<Dossier>(request);
            return response.Data;
        }

        private const string dossierUri = "/api/dossier/";
    }
}