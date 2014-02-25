using Lisa.Zuma.BlueJay.Web.Data;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
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
                return null;
            }

            var dossier = db.Dossiers.FirstOrDefault(d => d.Watchers.Contains(user));

            return Ok(dossier);
        }

        public IHttpActionResult Post(int userId, [FromBody] Dossier dossier) 
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) 
            {
                return NotFound();
            }

            user.Dossiers.Add(dossier);
            return Ok();
        }

        public IHttpActionResult Put(int userId, [FromBody] Dossier dossier)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var currentDossier = user.Dossiers.FirstOrDefault(d => d.Id == dossier.Id);
            if (currentDossier == null)
            {
                return NotFound();
            }

            currentDossier = dossier;
            db.SaveChanges();

            return Ok();
        }

        public IHttpActionResult Delete(int dossierId)
        {
            var dossier = db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            db.Dossiers.Remove(dossier);

            return Ok();
        }

        // GET api/dossier
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/dossier/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/dossier
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/dossier/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/dossier/5
        //public void Delete(int id)
        //{
        //}
    }
}
