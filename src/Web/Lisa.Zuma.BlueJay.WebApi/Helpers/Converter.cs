using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Lisa.Zuma.BlueJay.WebApi.Helpers
{
    public static class Converter
    {
        public static Dossier ToDossier(DossierData dossier, IEnumerable<IdentityRole> roles = null)
        {
            var model = new Dossier
            {
                Id = dossier.Id,
                Name = dossier.Name,
                Notes = new List<Note>(),
                Details = new List<DossierDetail>(),
                Watchers = new List<User>()
            };

            if (dossier.Details != null)
            {
                model.Details = Converter.ToDossierDetail(dossier.Details)
                                    .ToList();
            }

            if (dossier.Notes != null)
            {
                model.Notes = Converter.ToNote(dossier.Notes)
                                .ToList();
            }

            if (roles != null)
            {
                model.Watchers = Converter.ToUser(dossier.Watchers, roles)
                                .ToList();
            }

            return model;
        }

        public static IEnumerable<Dossier> ToDossier(IEnumerable<DossierData> dossiers, IEnumerable<IdentityRole> roles = null)
        {
            foreach (var dossier in dossiers)
            {
                yield return Converter.ToDossier(dossier, roles);
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

            if (note.Media != null)
            {
                model.Media = Converter.ToNoteMedia(note.Media)
                                .ToList();
            }

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

        public static User ToUser(UserData userData, IEnumerable<IdentityRole> roles) 
        {
            return new User
            {
                Id = userData.Id,
                UserName = userData.UserName,
                Type = userData.Type,
                Roles = Converter.ToUserRole(userData.Roles, roles).ToList()
            };
        }

        public static IEnumerable<User> ToUser(IEnumerable<UserData> users, IEnumerable<IdentityRole> roles)
        {
            foreach (var user in users)
            {
                yield return Converter.ToUser(user, roles);
            }
        }

        public static UserRole ToUserRole(IdentityUserRole identityRole, IEnumerable<IdentityRole> roles)
        {
            var role = roles.Single(r => r.Id == identityRole.RoleId);

            return new UserRole
            {
                Deleted = false,
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static IEnumerable<UserRole> ToUserRole(IEnumerable<IdentityUserRole> identityRoles, IEnumerable<IdentityRole> roles)
        {
            foreach (var role in identityRoles)
            {
                yield return Converter.ToUserRole(role, roles);
            }
        }

        public static UserRole ToUserRole(IdentityRole role)
        {
            return new UserRole
            {
                Deleted = false,
                Id = role.Id,
                Name = role.Name
            };
        }

        public static IEnumerable<UserRole> ToUserRole(IEnumerable<IdentityRole> roles)
        {
            foreach (var role in roles)
            {
                yield return Converter.ToUserRole(role);
            }
        }
    }
}
