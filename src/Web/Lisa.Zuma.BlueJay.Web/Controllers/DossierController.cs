using Lisa.Zuma.BlueJay.Web.Data;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Data.Extensions;
using Lisa.Zuma.BlueJay.Web.Helpers;
using Lisa.Zuma.BlueJay.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    [Authorize]
    public class DossierController : BaseApiController
    {
        public IHttpActionResult Get(int id)
        {
            var user = base.GetCurrentUser();
            if (user == null)
            {
                return NotFound();
            }

            var dossier = default(DossierData);
            if (!user.TryGetDossier(id, out dossier))
            {
                return NotFound();
            }
                        
            var dossierModel = Converter.ToDossier(dossier);

            return Ok(dossierModel);
        }

        public IHttpActionResult Get()
        {
            var user = base.GetCurrentUser();
            if (user == null)
            {
                return NotFound();
            }

            var dossierModels = Converter.ToDossier(user.Dossiers);

            return Ok(dossierModels);
        }
    }
}
