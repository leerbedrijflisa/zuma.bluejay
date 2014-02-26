using System;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.Web.Models.DbModels
{
    public class DossierModel
    {
        public int Id { get; set; }
        public List<NoteModel> Notes { get; set; }
    }
}
