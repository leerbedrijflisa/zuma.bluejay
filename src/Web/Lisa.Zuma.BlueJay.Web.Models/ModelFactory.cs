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
                        Name = media.Name
                    };

                    noteModel.Media.Add(noteMediaModel);
                }

                model.Notes.Add(noteModel);
            }

            return model;
        }
    }
}
