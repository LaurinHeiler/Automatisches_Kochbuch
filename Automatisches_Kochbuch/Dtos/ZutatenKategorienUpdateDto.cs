using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Dtos
{
    public class ZutatenKategorienUpdateDto
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [DataMember(Name = "Kategorie")]
        public string Kategorie { get; set; }
    }
}
