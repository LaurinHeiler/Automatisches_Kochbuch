using System;
using System.Collections.Generic;

namespace Automatisches_Kochbuch.Model
{
    public partial class TabZutaten
    {
        public TabZutaten()
        {
            LnkTabRezeptZutaten = new HashSet<LnkTabRezeptZutaten>();
            LnkTabUserZutaten = new HashSet<LnkTabUserZutaten>();
        }

        public int Id { get; set; }
        public string Zutat { get; set; }
        public int IdZutatEinheit { get; set; }
        public int IdZutatKategorie { get; set; }
        public sbyte? Vegetarisch { get; set; }
        public sbyte? Vegan { get; set; }
        public sbyte? Glutenfrei { get; set; }

        public ICollection<LnkTabRezeptZutaten> LnkTabRezeptZutaten { get; set; }
        public ICollection<LnkTabUserZutaten> LnkTabUserZutaten { get; set; }
    }
}
