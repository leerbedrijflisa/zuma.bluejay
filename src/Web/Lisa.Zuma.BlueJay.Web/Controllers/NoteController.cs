using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Helpers;
using Lisa.Zuma.BlueJay.Web.Models;
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
            var dossier = Db.Dossiers.Find(dossierId);
            if (dossier == null)
            {
                return NotFound();
            }

            var noteData = new NoteData
            {
                Text = noteModel.Text
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
            Db.SaveChanges();

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

            var result = Converter.ToNote(note);
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

        public string GetStorageUri(string file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file);
            var storageHelper = new StorageHelper("ZumaBlueJayStorageConnectionString", "bluejay");

            return storageHelper.GetWriteableSasUri(fileName, new TimeSpan(0, 2, 0)).AbsoluteUri;
        }
    }
}
