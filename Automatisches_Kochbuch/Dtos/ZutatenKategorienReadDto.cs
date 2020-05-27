using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Dtos
{
    public class ZutatenKategorienReadDto
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Kategorie")]
        public string Kategorie { get; set; }
    }
}
