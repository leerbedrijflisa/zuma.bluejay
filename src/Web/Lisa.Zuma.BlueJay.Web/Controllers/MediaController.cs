using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Data.Extensions;
using Lisa.Zuma.BlueJay.Web.Helpers;
using Lisa.Zuma.BlueJay.Web.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.Web.Controllers
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

            var storageHelper = new StorageHelper("ZumaBlueJayStorageConnectionString", "bluejay");
            var filename = storageHelper.GetFileNameFromSasUri(media.MediaLocation);
            //media.MediaLocation = storageHelper.GetReadableSasUri(filename, new TimeSpan(0, 2, 0)).AbsoluteUri;
            media.MediaLocation = storageHelper.GetReadableSasUri(filename, new TimeSpan(1, 0, 0)).AbsoluteUri;

            Db.SaveChanges();

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
            Db.NoteMedia.Remove(media);
            Db.SaveChanges();

            var storageHelper = new StorageHelper("ZumaBlueJayStorageConnectionString", "bluejay");
            storageHelper.RemoveFromStorageByUri(location);

            return Ok();
        }
    }
}
