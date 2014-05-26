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
        /// <summary>
        /// Checks if this dossier contains the detail with the id specified by <paramref name="id"/>.
        /// </summary>
        /// <param name="dossierData"></param>
        /// <param name="id">The id of the detail to check for existance.</param>
        /// <returns></returns>
        public static bool HasDetail(this DossierData dossierData, int id)
        {
            return dossierData.Details.Count(d => d.Id == id) > 0;
        }

        /// <summary>
        /// Checks if this dossier contains the detail as specified in <paramref name="dossierDetailData"/>.
        /// </summary>
        /// <param name="dossierData"></param>
        /// <param name="dossierDetailData">The detail to check for existance.</param>
        /// <returns></returns>
        public static bool HasDetail(this DossierData dossierData, DossierDetailData dossierDetailData)
        {
            return dossierData.HasDetail(dossierDetailData.Id);
        }

        /// <summary>
        /// Checks if this dossier contains the note identified by <paramref name="id"/>.
        /// </summary>
        /// <param name="dossierData"></param>
        /// <param name="id">The id of the note to check for existance.</param>
        /// <returns></returns>
        public static bool HasNote(this DossierData dossierData, int id)
        {
            return dossierData.Notes.Count(n => n.Id == id) > 0;
        }

        /// <summary>
        /// Checks if the dossier contains the note specified by <paramref name="noteData"/>.
        /// </summary>
        /// <param name="dossierData"></param>
        /// <param name="noteData">The note to check for existance.</param>
        /// <returns></returns>
        public static bool HasNote(this DossierData dossierData, NoteData noteData)
        {
            return dossierData.HasNote(noteData.Id);
        }

        /// <summary>
        /// Gets the detail identified by <paramref name="id"/>.
        /// </summary>
        /// <param name="dossierData"></param>
        /// <param name="id">The id of the detail to retrieve.</param>
        /// <returns></returns>
        public static DossierDetailData GetDetail(this DossierData dossierData, int id)
        {
            return dossierData.Details.FirstOrDefault(d => d.Id == id);
        }

        /// <summary>
        /// Tries to get the detail identified by <paramref name="id"/> and fills <paramref name="dossierDetailData"/>
        /// with the retrieved detail.
        /// </summary>
        /// <param name="dossierData"></param>
        /// <param name="id">The id of the detail to retrieve.</param>
        /// <param name="dossierDetailData">The detail to fill with the retrieval result.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the note identified by <paramref name="id"/>.
        /// </summary>
        /// <param name="dossierData"></param>
        /// <param name="id">The id of the note to retrieve.</param>
        /// <returns></returns>
        public static NoteData GetNote(this DossierData dossierData, int id)
        {
            return dossierData.Notes.FirstOrDefault(n => n.Id == id);
        }

        /// <summary>
        /// Tries to get the note identified by <paramref name="id"/> and fills <paramref name="noteData"/>
        /// with the retrieved note.
        /// </summary>
        /// <param name="dossierData"></param>
        /// <param name="id">The id of the note to retrieve.</param>
        /// <param name="dossierDetailData">The note to fill with the retrieval result.</param>
        /// <returns></returns>
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
