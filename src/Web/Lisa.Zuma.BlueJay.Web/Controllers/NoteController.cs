using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Models;
using Lisa.Zuma.BlueJay.Web.Models.DbModels;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    public class NoteController : BaseApiController
    {
        /// <summary>
        /// Gets a list of notes belonging to the requested dossier.
        /// </summary>
        /// <param name="dossierId">The id of the dossier from which to retrieve the notes.</param>
        public IHttpActionResult Get(int dossierId)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var result = new List<NoteModel>();

            foreach (var note in dossier.Notes)
            {
                var model = ModelFactory.Create(note);
                result.Add(model);
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets the note belonging to the requested dossier, identified by the given id.
        /// </summary>
        /// <param name="dossierId">The id of the dossier from which to retrieve the note.</param>
        /// <param name="id">The id of the note that should be retrieved.</param>
        /// <returns></returns>
        public IHttpActionResult Get(int dossierId, int id)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var note = dossier.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            var noteModel = ModelFactory.Create(note);
            return Ok(noteModel);
        }

        /// <summary>
        /// Creates a new note and adds it to the database.
        /// </summary>
        /// <param name="dossierId">The id of the dossier to which this note belongs.</param>
        /// <param name="noteModel">The model used to create the note.</param>
        /// <returns></returns>
        public IHttpActionResult Post(int dossierId, [FromBody] NoteModel noteModel)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var note = new Note
            {
                Text = noteModel.Text,
                Media = new List<NoteMedia>()
            };

            foreach (var media in noteModel.Media)
            {
                var noteMedia = StoreMedia(media);
                note.Media.Add(noteMedia);
            }

            dossier.Notes.Add(note);
            Db.SaveChanges();

            var model = ModelFactory.Create(note);
            
            return CreatedAtRoute("NoteApi", new { dossierId = dossierId }, model);
        }

        /// <summary>
        /// Updates the note with the newly received value(s).
        /// </summary>
        /// <param name="dossierId">The id of the dossier to which this note belongs.</param>
        /// <param name="noteModel">The model describing the contents of the note.</param>
        /// <returns></returns>
        public IHttpActionResult Put(int dossierId, [FromBody] NoteModel noteModel)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var note = dossier.Notes.FirstOrDefault(n => n.Id == noteModel.Id);
            if (note == null)
            {
                return NotFound();
            }

            note.Text = noteModel.Text;
            Db.SaveChanges();

            var result = ModelFactory.Create(note);
            return Ok(result);
        }

        public IHttpActionResult Delete(int dossierId, int id)
        {
            var dossier = Db.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var note = dossier.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            // Remove note by calling Db.Notes.Remove(note), dossier.Notes.Remove(note) will crash after Db.SaveChanges()
            // because it tries to set the relation to NULL to persist the entry in the database.
            Db.Notes.Remove(note);
            Db.SaveChanges();

            return Ok();
        }

        public NoteMedia StoreMedia(NoteMediaModel media)
        {
            var noteMedia = new NoteMedia
            {
                Name = media.Name
            };

            var setting = CloudConfigurationManager.GetSetting("ZumaBlueJayStorageConnectionString");
            var account = CloudStorageAccount.Parse(setting);

            var blobClient = account.CreateCloudBlobClient();


            var container = blobClient.GetContainerReference("bluejay");
            container.CreateIfNotExists();

            var blockBlob = container.GetBlockBlobReference(Guid.NewGuid().ToString() + Path.GetExtension(media.Name));
            var policy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Write,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(20)
            };
            blockBlob.Metadata.Add("Created", DateTime.Now.ToShortDateString());
            var location = blockBlob.Uri.AbsoluteUri + blockBlob.GetSharedAccessSignature(policy);
            noteMedia.MediaLocation = location;

            return noteMedia;
        }
    }
}
