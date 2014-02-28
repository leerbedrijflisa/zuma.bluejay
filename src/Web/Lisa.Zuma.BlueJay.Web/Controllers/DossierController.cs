using Lisa.Zuma.BlueJay.Web.Data;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    public class DossierController : BaseApiController
    {
        public IHttpActionResult Get(int id)
        {
            var user = Db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var dossier = Db.Dossiers.FirstOrDefault(d => d.Watchers.Count(u => u.Id == user.Id) > 0);
            if (dossier == null)
            {
                return NotFound();
            }

            var dossierModel = ModelFactory.Create(dossier);

            return Ok(dossierModel);
        }
    }
}
