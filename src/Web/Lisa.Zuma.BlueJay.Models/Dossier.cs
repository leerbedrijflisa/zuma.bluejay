﻿using System;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.Models
{
    public class Dossier
    {
        public int Id { get; set; }
        public List<DossierDetail> Details { get; set; }
        public List<Note> Notes { get; set; }
        public List<User> Watchers { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
    }
}
