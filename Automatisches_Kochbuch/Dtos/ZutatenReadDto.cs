using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Dtos
{
    public class ZutatenReadDto
    {
        public string Zutat { get; set; }
        public bool Vegetarisch { get; set; }
        public bool Vegan { get; set; }
        public bool Glutenfrei { get; set; }
    }
}
