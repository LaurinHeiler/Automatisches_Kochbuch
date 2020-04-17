using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Automatisches_Kochbuch.Model
{
    public partial class TabZutatenKategorien
    {

        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }


        [Required]
        [DisplayName("Kategorie")]
        public string Kategorie { get; set; }
    }
}
