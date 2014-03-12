using System;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<NoteMedia> Media { get; set; }
    }
}
