using System;
using System.Collections.Generic;

namespace Automatisches_Kochbuch.Model
{
    public partial class TabUser
    {
        public TabUser()
        {
            LnkTabUserRezepte = new HashSet<LnkTabUserRezepte>();
            LnkTabUserZutaten = new HashSet<LnkTabUserZutaten>();
        }

        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Passwort { get; set; }
        public int AnzahlPortionen { get; set; }
        public sbyte? Vegetarier { get; set; }
        public sbyte? Veganer { get; set; }
        public sbyte? Glutenfrei { get; set; }

        public ICollection<LnkTabUserRezepte> LnkTabUserRezepte { get; set; }
        public ICollection<LnkTabUserZutaten> LnkTabUserZutaten { get; set; }
    }
}
