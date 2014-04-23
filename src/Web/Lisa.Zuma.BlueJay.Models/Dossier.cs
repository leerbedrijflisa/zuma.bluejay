using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lisa.Zuma.BlueJay.Models
{
    public class Dossier
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        public List<DossierDetail> Details { get; set; }
        public List<Note> Notes { get; set; }
        public List<User> Watchers { get; set; }
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Display(Name = "Eigenaar Id")]
        public string OwnerId { get; set; }
    }
}
