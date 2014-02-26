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
    public class DossierController : ApiController
    {
        private BlueJayContext db = new BlueJayContext();

        public IHttpActionResult Get(int userId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var dossier = db.Dossiers.FirstOrDefault(d => d.Watchers.Count(u => u.Id == user.Id) > 0);
            if (dossier == null)
            {
                return NotFound();
            }

            var dossierModel = ModelFactory.Create(dossier);

            return Ok(dossierModel);
        }
    }
}
