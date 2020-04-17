using System;
using System.Collections.Generic;

namespace Automatisches_Kochbuch.Model
{
    public partial class TabRezepte
    {
        public TabRezepte()
        {
            LnkTabRezeptZutaten = new HashSet<LnkTabRezeptZutaten>();
            LnkTabUserRezepte = new HashSet<LnkTabUserRezepte>();
        }

        public int Id { get; set; }
        public string Rezeptname { get; set; }
        public sbyte? Vegetarisch { get; set; }
        public sbyte? Vegan { get; set; }
        public sbyte? Glutenfrei { get; set; }
        public string Zubereitung { get; set; }
        public byte[] Bild { get; set; }

        public ICollection<LnkTabRezeptZutaten> LnkTabRezeptZutaten { get; set; }
        public ICollection<LnkTabUserRezepte> LnkTabUserRezepte { get; set; }
    }
}
