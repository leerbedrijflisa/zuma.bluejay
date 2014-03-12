using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Zuma.BlueJay.Web.Helpers
{
    public static class ModelFactory
    {
        public static Dossier Create(DossierData dossier)
        {
            var model = new Dossier
            {
                Id = dossier.Id,
                Notes = new List<Note>(),
                Details = new List<DossierDetail>()
            };

            model.Details = ModelFactory.Create(dossier.Details)
                                        .ToList();

            model.Notes = ModelFactory.Create(dossier.Notes)
                                        .ToList();

            return model;
        }

        public static IEnumerable<Dossier> Create(IEnumerable<DossierData> dossiers)
        {
            foreach (var dossier in dossiers)
            {
                yield return ModelFactory.Create(dossier);
            }
        }

        public static Note Create(NoteData note)
        {
            var model = new Note
            {
                Id = note.Id,
                Text = note.Text,
                Media = new List<NoteMedia>()
            };

            model.Media = ModelFactory.Create(note.Media)
                                        .ToList();

            return model;
        }

        public static IEnumerable<Note> Create(IEnumerable<NoteData> notes)
        {
            foreach (var note in notes)
            {
                yield return ModelFactory.Create(note);
            }
        }

        public static NoteMedia Create(NoteMediaData noteMedia)
        {
            var noteMediaModel = new NoteMedia
            {
                Id = noteMedia.Id,
                Name = noteMedia.Name,
                Location = noteMedia.MediaLocation
            };

            return noteMediaModel;
        }

        public static IEnumerable<NoteMedia> Create(IEnumerable<NoteMediaData> noteMedias)
        {
            foreach (var media in noteMedias)
            {
                yield return ModelFactory.Create(media);
            }
        }

        public static DossierDetail Create(DossierDetailData dossierDetail)
        {
            return new DossierDetail
            {
                Id = dossierDetail.Id,
                Category = dossierDetail.Category,
                Contents = dossierDetail.Contents
            };
        }

        public static IEnumerable<DossierDetail> Create(IEnumerable<DossierDetailData> dossierDetails)
        {
            foreach (var detail in dossierDetails)
            {
                yield return ModelFactory.Create(detail);
            }
        }
    }
}
