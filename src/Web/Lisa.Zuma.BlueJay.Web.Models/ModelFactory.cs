using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Zuma.BlueJay.Web.Models
{
    public static class ModelFactory
    {
        public static DossierModel Create(Dossier dossier)
        {
            var model = new DossierModel
            {
                Id = dossier.Id,
                Notes = new List<NoteModel>()
            };

            model.Profile = new ProfileModel
            {
                Id = dossier.Profile.Id
            };

            foreach (var note in dossier.Notes)
            {
                var noteModel = new NoteModel
                {
                    Id = note.Id,
                    Media = new List<NoteMediaModel>(),
                    Text = note.Text
                };

                foreach (var media in note.Media)
                {
                    var noteMediaModel = new NoteMediaModel
                    {
                        Id = media.Id,
                        Name = media.Name
                    };

                    noteModel.Media.Add(noteMediaModel);
                }

                model.Notes.Add(noteModel);
            }

            return model;
        }

        public static NoteModel Create(Note note)
        {
            var model = new NoteModel
            {
                Id = note.Id,
                Text = note.Text,
                Media = new List<NoteMediaModel>()
            };

            foreach (var media in note.Media)
            {
                var noteMediaModel = new NoteMediaModel
                {
                    Id = media.Id,
                    Name = media.Name,
                    EncodedData = LoadMediaAndEncode(media)
                };

                model.Media.Add(noteMediaModel);
            }

            return model;
        }

        public static NoteMediaModel Create(NoteMedia noteMedia)
        {
            var noteMediaModel = new NoteMediaModel
            {
                Id = noteMedia.Id,
                Name = noteMedia.Name,
                Location = noteMedia.MediaLocation
            };

            return noteMediaModel;
        }

        public static ProfileModel Create(Profile profile)
        {
            var profileModel = new ProfileModel
            {
                Id = profile.Id
            };

            return profileModel;
        }

        private static string LoadMediaAndEncode(NoteMedia media) 
        {
#if DEBUG
            var bytes = System.IO.File.ReadAllBytes(media.MediaLocation);
#else
            // TODO: Add support for the cloud
#endif

            return Convert.ToBase64String(bytes);
        }
    }
}
