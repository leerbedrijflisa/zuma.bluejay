using System;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.Web.Models.DbModels
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<NoteMediaModel> Media { get; set; }
    }
}
