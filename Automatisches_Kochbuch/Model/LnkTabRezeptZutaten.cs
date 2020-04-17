using System;
using System.Collections.Generic;

namespace Automatisches_Kochbuch.Model
{
    public partial class LnkTabRezeptZutaten
    {
        public int Id { get; set; }
        public int IdRezept { get; set; }
        public int IdZutaten { get; set; }
        public decimal Menge { get; set; }

        public TabRezepte IdRezeptNavigation { get; set; }
        public TabZutaten IdZutatenNavigation { get; set; }
    }
}
