using System;
using System.Collections.Generic;

namespace Automatisches_Kochbuch.Model
{
    public partial class LnkTabUserRezepte
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdRezepte { get; set; }
        public int AnzahlPortionen { get; set; }

        public TabRezepte IdRezepteNavigation { get; set; }
        public TabUser IdUserNavigation { get; set; }
    }
}
