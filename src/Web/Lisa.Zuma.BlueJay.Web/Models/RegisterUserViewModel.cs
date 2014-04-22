using Lisa.Zuma.BlueJay.Web.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lisa.Zuma.BlueJay.Web.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het wachtwoord moet minstens bestaan uit {2} karakters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Bevestig wachtwoord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Het wachtwoord en de bevestiging komen niet overeen.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Ouder (j/n)")]
        public bool IsParent { get; set; }

        [RequiredIf("IsParent", true, ErrorMessage="De naam van het kind is verplicht.")]
        [Display(Name = "Naam kind")]
        public string DossierName { get; set; }
    }
}