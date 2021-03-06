﻿using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Data.Extensions;
using Lisa.Zuma.BlueJay.WebApi.Helpers;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.WebApi.Controllers
{
    [Authorize]
    public class MediaController : BaseApiController
    {
        public IHttpActionResult Get(int dossierId, int noteId)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var note = default(NoteData);
            if (!dossier.TryGetNote(noteId, out note))
            {
                return NotFound();
            }

            var result = new List<NoteMedia>();
            foreach (var media in note.Media)
            {
                var noteMediaModel = Converter.ToNoteMedia(media);
                result.Add(noteMediaModel);
            }

            return Ok(result);
        }

        public IHttpActionResult Get(int dossierId, int noteId, int id)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var note = default(NoteData);
            if (!dossier.TryGetNote(noteId, out note))
            {
                return NotFound();
            }

            var media = default(NoteMediaData);
            if (!note.TryGetMedia(id, out media))
            {
                return NotFound();
            }

            var model = Converter.ToNoteMedia(media);
            
            var fileName = StorageHelper.GetFileNameFromSasUri(media.MediaLocation);
            model.Location = StorageHelper.GetReadableSasUri(fileName, new TimeSpan(0, 2, 0)).AbsoluteUri;

            return Ok(model);
        }

        public IHttpActionResult Put(int dossierId, int noteId, int id, [FromBody] NoteMedia noteMediaModel)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var note = default(NoteData);
            if (!dossier.TryGetNote(noteId, out note))
            {
                return NotFound();
            }

            var media = default(NoteMediaData);
            if (!note.TryGetMedia(id, out media))
            {
                return NotFound();
            }
                        
            var filename = StorageHelper.GetFileNameFromSasUri(media.MediaLocation);
            media.MediaLocation = StorageHelper.GetReadableSasUri(filename, new TimeSpan(0, 2, 0)).AbsoluteUri;
            //media.MediaLocation = StorageHelper.GetReadableSasUri(filename, new TimeSpan(1, 0, 0)).AbsoluteUri;

            UoW.Save();

            var result = Converter.ToNoteMedia(media);
            return Ok(result);
        }

        public IHttpActionResult Delete(int dossierId, int noteId, int id)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var note = default(NoteData);
            if (!dossier.TryGetNote(noteId, out note))
            {
                return NotFound();
            }

            var media = default(NoteMediaData);
            if (!note.TryGetMedia(id, out media))
            {
                return NotFound();
            }

            string location = media.MediaLocation;

            UoW.NoteMediaRepository.Delete(media);
            UoW.Save();

            StorageHelper.RemoveFromStorageByUri(location);

            return Ok();
        }
    }
}
