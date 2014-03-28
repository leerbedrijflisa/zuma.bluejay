using Lisa.Zuma.BlueJay.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Zuma.BlueJay.Web.Data.Extensions
{
    public static class UserDataExtensions
    {
        public static bool HasDossier(this UserData userData, int id)
        {
            return userData.Dossiers.Count(d => d.Id == id) > 0;
        }

        public static bool HasDossier(this UserData userData, DossierData dossier)
        {
            return userData.HasDossier(dossier.Id);
        }

        public static DossierData GetDossier(this UserData userData, int id)
        {
            return userData.Dossiers.FirstOrDefault(d => d.Id == id);
        }

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
