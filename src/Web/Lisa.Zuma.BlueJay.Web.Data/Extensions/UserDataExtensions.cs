using Lisa.Zuma.BlueJay.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Zuma.BlueJay.Web.Data.Extensions
{
    public static class UserDataExtensions
    {
        /// <summary>
        /// Checks if this user has the dossier with the id specified by <paramref name="id"/>.
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="id">The id of the dossier to check for existance.</param>
        /// <returns></returns>
        public static bool HasDossier(this UserData userData, int id)
        {
            return userData.Dossiers.Count(d => d.Id == id) > 0;
        }

        /// <summary>
        /// Checks if this user has the dossier as specified by <paramref name="dossier"/>.
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="dossier">The dossier to check for existance.</param>
        /// <returns></returns>
        public static bool HasDossier(this UserData userData, DossierData dossier)
        {
            return userData.HasDossier(dossier.Id);
        }

        /// <summary>
        /// Gets the dossier identified by <paramref name="id"/>.
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="id">The id of the dossier to retrieve.</param>
        /// <returns></returns>
        public static DossierData GetDossier(this UserData userData, int id)
        {
            return userData.Dossiers.FirstOrDefault(d => d.Id == id);
        }

        /// <summary>
        /// Tries to get the dossier identified by <paramref name="id"/> and fills <paramref name="dossierData"/>
        /// with the retrieved dossier.
        /// </summary>
        /// <param name="noteData"></param>
        /// <param name="id">The id of the dossier to retrieve.</param>
        /// <param name="dossierData">The dossier to fill with the retrieval result.</param>
        /// <returns></returns>
        public static bool TryGetDossier(this UserData userData, int id, out DossierData dossierData)
        {
            if (!userData.HasDossier(id))
            {
                dossierData = null;
                return false;
            }

            dossierData = userData.GetDossier(id);
            return true;
        }
    }
}
