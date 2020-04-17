using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Automatisches_Kochbuch.Model
{
    public partial class TabZutatenEinheit
    {

        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("EinheitKuerzel")]
        public string EinheitKuerzel { get; set; }

        [Required]
        [DisplayName("Einheit")]
        public string Einheit { get; set; }
    }
}
