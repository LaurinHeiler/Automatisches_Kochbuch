using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Dtos
{
    public class ZutatenUpdateDto
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [DataMember(Name = "Zutat")]
        public string Zutat { get; set; }

        [DisplayName("IdZutatEinheit")]
        public int IdZutatEinheit { get; set; }

        [DisplayName("IdZutatKategorie")]
        public int IdZutatKategorie { get; set; }

        [DataMember(Name = "Vegetarisch")]
        public bool Vegetarisch { get; set; }

        [DataMember(Name = "Vegan")]
        public bool Vegan { get; set; }

        [DataMember(Name = "Glutenfrei")]
        public bool Glutenfrei { get; set; }
    }
}
