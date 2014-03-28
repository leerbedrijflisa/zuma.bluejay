using Lisa.Zuma.BlueJay.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.Web.Data.Extensions
{
    public static class DossierDataExtensions
    {
        public static bool HasDetail(this DossierData dossierData, int id)
        {
            return dossierData.Details.Count(d => d.Id == id) > 0;
        }

        public static bool HasDetail(this DossierData dossierData, DossierDetailData dossierDetailData)
        {
            return dossierData.HasDetail(dossierDetailData.Id);
        }

        public static bool HasNote(this DossierData dossierData, int id)
        {
            return dossierData.Notes.Count(n => n.Id == id) > 0;
        }

        public static bool HasNote(this DossierData dossierData, NoteData noteData)
        {
            return dossierData.HasNote(noteData.Id);
        }

        public static DossierDetailData GetDetail(this DossierData dossierData, int id)
        {
            return dossierData.Details.FirstOrDefault(d => d.Id == id);
        }

        public static bool TryGetDetail(this DossierData dossierData, int id, out DossierDetailData dossierDetailData)
        {
            if (!dossierData.HasDetail(id))
            {
                dossierDetailData = null;
                return false;
            }

            dossierDetailData = dossierData.GetDetail(id);
            return true;
        }

        public static NoteData GetNote(this DossierData dossierData, int id)
        {
            return dossierData.Notes.FirstOrDefault(n => n.Id == id);
        }

        public static bool TryGetNote(this DossierData dossierData, int id, out NoteData noteData)
        {
            if (!dossierData.HasNote(id))
            {
                noteData = null;
                return false;
            }

            noteData = dossierData.GetNote(id);
            return true;
        }
    }
}
