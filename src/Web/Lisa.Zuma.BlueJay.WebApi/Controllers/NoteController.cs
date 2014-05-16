using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.WebApi.Helpers;
using Lisa.Zuma.BlueJay.Web.Data.Extensions;
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

namespace Lisa.Zuma.BlueJay.WebApi.Controllers
{
    [Authorize]
    public class NoteController : BaseApiController
    {
        /// <summary>
        /// Gets a list of notes belonging to the requested dossier.
        /// </summary>
        /// <param name="dossierId">The id of the dossier from which to retrieve the notes.</param>
        public IHttpActionResult Get(int dossierId)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var result = new List<Note>();

            foreach (var note in dossier.Notes)
            {
                var model = Converter.ToNote(note);
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
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var note = default(NoteData);
            if (!dossier.TryGetNote(id, out note))
            {
                return NotFound();
            }

            var noteModel = Converter.ToNote(note);
            return Ok(noteModel);
        }

        /// <summary>
        /// Creates a new note and adds it to the database.
        /// </summary>
        /// <param name="dossierId">The id of the dossier to which this note belongs.</param>
        /// <param name="noteModel">The model used to create the note.</param>
        /// <returns></returns>
        public IHttpActionResult Post(int dossierId, [FromBody] Note noteModel)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var noteData = new NoteData
            {
                Text = noteModel.Text,
                DateCreated = noteModel.DateCreated,
                PosterId = CurrentUser.Id
            };

            foreach (var media in noteModel.Media)
            {
                var noteMediaData = new NoteMediaData
                {
                    Name = media.Name,
                    MediaLocation = GetStorageUri(media.Name)
                };

                noteData.Media.Add(noteMediaData);
            }

            dossier.Notes.Add(noteData);
            UoW.Save();

            var note = Converter.ToNote(noteData);
            
            return CreatedAtRoute("NoteApi", new { dossierId = dossierId }, note);
        }

        /// <summary>
        /// Updates the note with the newly received value(s).
        /// </summary>
        /// <param name="dossierId">The id of the dossier to which this note belongs.</param>
        /// <param name="noteModel">The model describing the contents of the note.</param>
        /// <returns></returns>
        public IHttpActionResult Put(int dossierId, [FromBody] Note noteModel)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var note = default(NoteData);
            if (!dossier.TryGetNote(noteModel.Id, out note))
            {
                return NotFound();
            }

            note.Text = noteModel.Text;
            UoW.Save();

            var result = Converter.ToNote(note);
            return Ok(result);
        }

        public IHttpActionResult Delete(int dossierId, int id)
        {
            var dossier = default(DossierData);
            if (!CurrentUser.TryGetDossier(dossierId, out dossier))
            {
                return NotFound();
            }

            var note = default(NoteData);
            if (!dossier.TryGetNote(id, out note))
            {
                return NotFound();
            }

            UoW.NoteRepository.Delete(note);
            UoW.Save();

            return Ok();
        }

        public string GetStorageUri(string file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file);

            return StorageHelper.GetWriteableSasUri(fileName, new TimeSpan(0, 2, 0)).AbsoluteUri;
        }
    }
}
