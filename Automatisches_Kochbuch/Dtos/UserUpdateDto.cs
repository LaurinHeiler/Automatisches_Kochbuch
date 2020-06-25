using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Vorname")]
        public string Vorname { get; set; }

        [Required]
        [DisplayName("Nachname")]
        public string Nachname { get; set; }

        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Passwort")]
        public string Passwort { get; set; }

        [Required]
        [DisplayName("Rolle")]
        public string Role { get; set; }

        [Required]
        [DisplayName("AnzahlPortionen")]
        public int AnzahlPortionen { get; set; }

        [DisplayName("Vegetarier")]
        public bool Vegetarier { get; set; }

        [DisplayName("Veganer")]
        public bool Veganer { get; set; }

        [DisplayName("glutenfrei")]
        public bool Glutenfrei { get; set; }
    }
}
