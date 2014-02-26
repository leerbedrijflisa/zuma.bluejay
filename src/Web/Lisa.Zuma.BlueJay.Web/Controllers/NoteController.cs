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

            return CreatedAtRoute("NoteApi", new { id = dossierId }, noteModel);
        }

        public NoteMedia StoreMedia(NoteMediaModel media)
        {
            var result = new NoteMedia();

#if DEBUG
            var path = @"D:\zuma_storage\" + media.Name;
            var bytes = Convert.FromBase64String(media.EncodedData);
            System.IO.File.WriteAllBytes(path, bytes);

            result.Name = media.Name;
            result.MediaLocation = path;
#else
            // TODO: Add support for the cloud
#endif

            return result;
        }
    }
}
