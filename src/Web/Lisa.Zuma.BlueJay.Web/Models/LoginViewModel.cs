using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Lisa.Zuma.BlueJay.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }
    }
}