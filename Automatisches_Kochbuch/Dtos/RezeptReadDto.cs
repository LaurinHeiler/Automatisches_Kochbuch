using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Dtos
{
    public class RezeptReadDto
    {
        public string Rezeptname { get; set; }

        public bool Vegetarisch { get; set; }

        public bool Vegan { get; set; }

        public bool Glutenfrei { get; set; }

        public string Zubereitung { get; set; }

        public byte[] Bild { get; set; }

    }
}
