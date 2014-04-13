using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Lisa.Zuma.BlueJay.WebApi.Helpers
{
    public static class Converter
    {
        public static Dossier ToDossier(DossierData dossier)
        {
            var model = new Dossier
            {
                Id = dossier.Id,
                Notes = new List<Note>(),
                Details = new List<DossierDetail>()
            };

            model.Details = Converter.ToDossierDetail(dossier.Details)
                                        .ToList();

            model.Notes = Converter.ToNote(dossier.Notes)
                                        .ToList();

            return model;
        }

        public static IEnumerable<Dossier> ToDossier(IEnumerable<DossierData> dossiers)
        {
            foreach (var dossier in dossiers)
            {
                yield return Converter.ToDossier(dossier);
            }
        }

        public static Note ToNote(NoteData note)
        {
            var model = new Note
            {
                Id = note.Id,
                Text = note.Text,
                DateCreated = note.DateCreated,
                Media = new List<NoteMedia>()
            };

            model.Media = Converter.ToNoteMedia(note.Media)
                                        .ToList();

            return model;
        }

        public static IEnumerable<Note> ToNote(IEnumerable<NoteData> notes)
        {
            foreach (var note in notes)
            {
                yield return Converter.ToNote(note);
            }
        }

        public static NoteMedia ToNoteMedia(NoteMediaData noteMedia)
        {
            var noteMediaModel = new NoteMedia
            {
                Id = noteMedia.Id,
                Name = noteMedia.Name,
                Location = noteMedia.MediaLocation
            };

            return noteMediaModel;
        }

        public static IEnumerable<NoteMedia> ToNoteMedia(IEnumerable<NoteMediaData> noteMedias)
        {
            foreach (var media in noteMedias)
            {
                yield return Converter.ToNoteMedia(media);
            }
        }

        public static DossierDetail ToDossierDetail(DossierDetailData dossierDetail)
        {
            return new DossierDetail
            {
                Id = dossierDetail.Id,
                Category = dossierDetail.Category,
                Contents = dossierDetail.Contents
            };
        }

        public static IEnumerable<DossierDetail> ToDossierDetail(IEnumerable<DossierDetailData> dossierDetails)
        {
            foreach (var detail in dossierDetails)
            {
                yield return Converter.ToDossierDetail(detail);
            }
        }

        public static UserClaim ToUserClaim(Claim claim)
        {
            return new UserClaim
            {
                Properties = claim.Properties.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                Type = claim.Type,
                Value = claim.Value,
                ValueType = claim.ValueType
            };
        }

        public static IEnumerable<UserClaim> ToUserClaim(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                yield return Converter.ToUserClaim(claim);
            }
        }
    }
}
