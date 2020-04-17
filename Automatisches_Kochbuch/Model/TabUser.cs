using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Automatisches_Kochbuch.Model
{
    public partial class TabUser
    {
        public TabUser()
        {
            LnkTabUserRezepte = new HashSet<LnkTabUserRezepte>();
            LnkTabUserZutaten = new HashSet<LnkTabUserZutaten>();
        }

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
        [DisplayName("Passwort")]
        public string Passwort { get; set; }

        [Required]
        [DisplayName("AnzahlPortionen")]
        public int AnzahlPortionen { get; set; }

        [DisplayName("Vegetarier")]
        public sbyte? Vegetarier { get; set; }

        [DisplayName("Veganer")]
        public sbyte? Veganer { get; set; }

        [DisplayName("glutenfrei")]
        public sbyte? Glutenfrei { get; set; }

        public virtual ICollection<LnkTabUserRezepte> LnkTabUserRezepte { get; set; }
        public virtual ICollection<LnkTabUserZutaten> LnkTabUserZutaten { get; set; }
    }
}
