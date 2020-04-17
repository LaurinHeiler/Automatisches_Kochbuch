using System;
using System.Collections.Generic;

namespace Automatisches_Kochbuch.Model
{
    public partial class LnkTabUserZutaten
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdZutaten { get; set; }
        public int Menge { get; set; }

        public TabUser IdUserNavigation { get; set; }
        public TabZutaten IdZutatenNavigation { get; set; }
    }
}
