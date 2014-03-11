using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Zuma.BlueJay.Web.Models
{
    public static class ModelFactory
    {
        public static DossierModel Create(DossierData dossier)
        {
            var model = new DossierModel
            {
                Id = dossier.Id,
                Notes = new List<NoteModel>(),
                Details = new List<DossierDetailModel>()
            };

            model.Details = ModelFactory.Create(dossier.Details)
                                        .ToList();

            model.Notes = ModelFactory.Create(dossier.Notes)
                                        .ToList();

            return model;
        }

        public static IEnumerable<DossierModel> Create(IEnumerable<DossierData> dossiers)
        {
            foreach (var dossier in dossiers)
            {
                yield return ModelFactory.Create(dossier);
            }
        }

        public static NoteModel Create(NoteData note)
        {
            var model = new NoteModel
            {
                Id = note.Id,
                Text = note.Text,
                Media = new List<NoteMediaModel>()
            };

            model.Media = ModelFactory.Create(note.Media)
                                        .ToList();

            return model;
        }

        public static IEnumerable<NoteModel> Create(IEnumerable<NoteData> notes)
        {
            foreach (var note in notes)
            {
                yield return ModelFactory.Create(note);
            }
        }

        public static NoteMediaModel Create(NoteMediaData noteMedia)
        {
            var noteMediaModel = new NoteMediaModel
            {
                Id = noteMedia.Id,
                Name = noteMedia.Name,
                Location = noteMedia.MediaLocation
            };

            return noteMediaModel;
        }

        public static IEnumerable<NoteMediaModel> Create(IEnumerable<NoteMediaData> noteMedias)
        {
            foreach (var media in noteMedias)
            {
                yield return ModelFactory.Create(media);
            }
        }

        public static DossierDetailModel Create(DossierDetailData dossierDetail)
        {
            return new DossierDetailModel
            {
                Id = dossierDetail.Id,
                Category = dossierDetail.Category,
                Contents = dossierDetail.Contents
            };
        }

        public static IEnumerable<DossierDetailModel> Create(IEnumerable<DossierDetailData> dossierDetails)
        {
            foreach (var detail in dossierDetails)
            {
                yield return ModelFactory.Create(detail);
            }
        }
    }
}
