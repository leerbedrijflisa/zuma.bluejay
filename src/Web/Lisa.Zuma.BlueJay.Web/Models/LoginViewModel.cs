using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Lisa.Zuma.BlueJay.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}