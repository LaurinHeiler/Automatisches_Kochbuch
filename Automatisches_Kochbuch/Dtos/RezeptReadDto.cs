using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Dtos
{
    public class RezeptReadDto
    {
        [Required]
        [DataMember(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [DataMember(Name = "Rezeptname")]
        public string Rezeptname { get; set; }

        [DataMember(Name = "Vegetarisch")]
        public bool Vegetarisch { get; set; }

        [DataMember(Name = "Vegan")]
        public bool Vegan { get; set; }

        [DataMember(Name = "Glutenfrei")]
        public bool Glutenfrei { get; set; }

        [Required]
        [DataMember(Name = "Zubereitung")]
        public string Zubereitung { get; set; }

        [DataMember(Name = "Bild")]
        public byte[] Bild { get; set; }

    }
}
