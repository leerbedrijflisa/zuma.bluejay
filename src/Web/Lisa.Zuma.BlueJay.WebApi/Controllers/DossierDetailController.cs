﻿using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.WebApi.Helpers;
using Lisa.Zuma.BlueJay.Web.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.WebApi.Controllers
{
    [Authorize]
    public class DossierDetailController : BaseApiController
    {
        public IHttpActionResult Get(int dossierId)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var details = Converter.ToDossierDetail(dossier.Details);
            return Ok(details);
        }

        public IHttpActionResult Get(int dossierId, int id)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var detail = default(DossierDetailData);
            if (!dossier.TryGetDetail(id, out detail))
            {
                return NotFound();
            }

            var result = Converter.ToDossierDetail(detail);
            return Ok(result);
        }

        public IHttpActionResult Post(int dossierId, [FromBody] DossierDetail dossierDetailModel)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var detail = new DossierDetailData
            {
                Category = dossierDetailModel.Category,
                Contents = dossierDetailModel.Contents
            };

            dossier.Details.Add(detail);
            UoW.Save();

            var model = Converter.ToDossierDetail(detail);
            return CreatedAtRoute("DossierDetailApi", new { dossierId = dossierId, id = detail.Id }, model);
        }

        public IHttpActionResult Put(int dossierId, int id, [FromBody] DossierDetail dossierDetailModel)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var detail = default(DossierDetailData);
            if (!dossier.TryGetDetail(id, out detail))
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

            UoW.Save();

            var model = Converter.ToDossierDetail(detail);
            return Ok(model);
        }

        public IHttpActionResult Delete(int dossierId, int id)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var detail = default(DossierDetailData);
            if (!dossier.TryGetDetail(id, out detail))
            {
                return NotFound();
            }
            
            UoW.DossierDetailRepository.Delete(detail);
            UoW.Save();

            return Ok();
        }
    }
}
