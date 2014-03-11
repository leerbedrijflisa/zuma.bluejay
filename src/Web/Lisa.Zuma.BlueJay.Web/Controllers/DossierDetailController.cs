using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Models;
using Lisa.Zuma.BlueJay.Web.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    public class DossierDetailController : BaseApiController
    {
        public IHttpActionResult Get(int dossierId)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var details = ModelFactory.Create(dossier.Details);
            return Ok(details);
        }

        public IHttpActionResult Get(int dossierId, int id)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var detail = dossier.Details.FirstOrDefault(d => d.Id == id);
            if (detail == null)
            {
                return NotFound();
            }

            var result = ModelFactory.Create(detail);
            return Ok(result);
        }

        public IHttpActionResult Post(int dossierId, [FromBody] DossierDetailModel dossierDetailModel)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var detail = new DossierDetailData
            {
                Category = dossierDetailModel.Category,
                Contents = dossierDetailModel.Contents
            };

            dossier.Details.Add(detail);
            Db.SaveChanges();

            var model = ModelFactory.Create(detail);
            return CreatedAtRoute("DossierDetailApi", new { dossierId = dossierId, id = detail.Id }, model);
        }

        public IHttpActionResult Put(int dossierId, int id, [FromBody] DossierDetailModel dossierDetailModel)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null) 
            {
                return NotFound();
            }

            var detail = dossier.Details.FirstOrDefault(d => d.Id == id);
            if (detail == null)
            {
                return NotFound();
            }

            if (detail.Category != dossierDetailModel.Category)
            {
                detail.Category = dossierDetailModel.Category;
            }

            if (detail.Contents != dossierDetailModel.Contents)
            {
                detail.Contents = dossierDetailModel.Contents;
            }

            Db.SaveChanges();

            var model = ModelFactory.Create(detail);
            return Ok(model);
        }

        public IHttpActionResult Delete(int dossierId, int id)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var detail = dossier.Details.FirstOrDefault(d => d.Id == id);
            if (detail == null)
            {
                return NotFound();
            }

            Db.DossierDetails.Remove(detail);
            Db.SaveChanges();

            return Ok();
        }
    }
}
