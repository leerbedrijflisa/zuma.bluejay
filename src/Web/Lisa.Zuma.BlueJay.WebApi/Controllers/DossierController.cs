using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Data;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Data.Extensions;
using Lisa.Zuma.BlueJay.WebApi.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/dossier")]
    public class DossierController : BaseApiController
    {
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(id, out dossier))
            {
                return NotFound();
            }
                        
            var dossierModel = Converter.ToDossier(dossier, RoleManager.Roles);

            return Ok(dossierModel);
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            // TODO: Fix problem with context issue when using CurrentUser property in BaseApiController.
            var u = UoW.UserManager.FindById(User.Identity.GetUserId());
            var dossierModels = Converter.ToDossier(u.Dossiers, RoleManager.Roles);

            return Ok(dossierModels);
        }

        [Route("{userId}")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(string userId, [FromBody] Dossier dossier)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var dossierData = new DossierData()
            {
                Name = dossier.Name,
                Watchers = new List<UserData>()
            };
            dossierData.Watchers.Add(user);

            UoW.DossierRepository.Insert(dossierData);
            UoW.Save();

            var result = Converter.ToDossier(dossierData);

            return Created(Request.RequestUri, result);
        }
    }
}
