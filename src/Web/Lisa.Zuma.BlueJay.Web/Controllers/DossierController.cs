using Lisa.Zuma.BlueJay.Web.Data;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
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
            string userId = User.Identity.GetUserId();
            var user = Db.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            var dossier = user.Dossiers.FirstOrDefault(d => d.Id == id);
            if (dossier == null)
            {
                return NotFound();
            }

            var dossierModel = Converter.ToDossier(dossier);

            return Ok(dossierModel);
        }

        public IHttpActionResult Get()
        {
            string userId = User.Identity.GetUserId();
            var user = Db.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            var dossiers = Db
                .Dossiers
                .Where(d => d.Watchers.Count(u => u.Id == user.Id) > 0)
                .ToList();

            var dossierModels = Converter.ToDossier(dossiers);

            return Ok(dossierModels);
        }
    }
}
