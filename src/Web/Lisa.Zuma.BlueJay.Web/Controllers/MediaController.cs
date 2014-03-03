using Lisa.Zuma.BlueJay.Web.Models;
using Lisa.Zuma.BlueJay.Web.Models.DbModels;
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
    public class MediaController : BaseApiController
    {
        public IHttpActionResult Get(int dossierId, int noteId)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var note = dossier.Notes.FirstOrDefault(n => n.Id == noteId);
            if (note == null)
            {
                return NotFound();
            }

            var result = new List<NoteMediaModel>();
            foreach (var media in note.Media)
            {
                var noteMediaModel = ModelFactory.Create(media);
                result.Add(noteMediaModel);
            }

            return Ok(result);
        }

        public IHttpActionResult Get(int dossierId, int noteId, int id)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var note = dossier.Notes.FirstOrDefault(n => n.Id == noteId);
            if (note == null)
            {
                return NotFound();
            }

            var media = note.Media.FirstOrDefault(m => m.Id == id);
            if (media == null) 
            {
                return NotFound();
            }
            
            var model = ModelFactory.Create(media);
            return Ok(model);
        }

        public IHttpActionResult Put(int dossierId, int noteId, int id, [FromBody] NoteMediaModel noteMediaModel)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var note = dossier.Notes.FirstOrDefault(n => n.Id == noteId);
            if (note == null)
            {
                return NotFound();
            }

            var media = note.Media.FirstOrDefault(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            var model = ChangeSharedAccessSignature(noteMediaModel);
            media.MediaLocation = model.Location;
            Db.SaveChanges();

            var result = ModelFactory.Create(media);
            return Ok(result);
        }

        public IHttpActionResult Delete(int dossierId, int noteId, int id)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var note = dossier.Notes.FirstOrDefault(n => n.Id == noteId);
            if (note == null)
            {
                return NotFound();
            }

            var media = note.Media.FirstOrDefault(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            Db.NoteMedia.Remove(media);
            Db.SaveChanges();

            return Ok();
        }

        private NoteMediaModel ChangeSharedAccessSignature(NoteMediaModel noteMediaModel)
        {
            var setting = CloudConfigurationManager.GetSetting("ZumaBlueJayStorageConnectionString");
            var account = CloudStorageAccount.Parse(setting);

            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("bluejay");

            var uri = new Uri(noteMediaModel.Location);
            var blockName = uri.Segments.Last();
            var blockBlob = container.GetBlockBlobReference(blockName);
            var policy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read
            };

            var location = blockBlob.Uri.AbsoluteUri + blockBlob.GetSharedAccessSignature(policy);
            noteMediaModel.Location = location;

            return noteMediaModel;
        }
    }
}
