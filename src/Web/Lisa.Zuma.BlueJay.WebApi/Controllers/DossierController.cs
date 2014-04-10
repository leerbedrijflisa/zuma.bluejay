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
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.WebApi.Controllers
{
    [Authorize]
    public class DossierController : BaseApiController
    {
        public IHttpActionResult Get(int id)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(id, out dossier))
            {
                return NotFound();
            }
                        
            var dossierModel = Converter.ToDossier(dossier);

            return Ok(dossierModel);
        }

        public IHttpActionResult Get()
        {
            var dossierModels = Converter.ToDossier(CurrentUser.Dossiers);

            return Ok(dossierModels);
        }
    }
}
