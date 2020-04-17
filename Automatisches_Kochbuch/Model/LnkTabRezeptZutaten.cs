using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Automatisches_Kochbuch.Model
{
    public partial class LnkTabRezeptZutaten
    {

        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("IDRezept")]
        public int IdRezept { get; set; }

        [Required]
        [DisplayName("IDZutaten")]
        public int IdZutaten { get; set; }

        [Required]
        [DisplayName("Menge")]
        public decimal Menge { get; set; }

        public virtual TabRezepte IdRezeptNavigation { get; set; }
        public virtual TabZutaten IdZutatenNavigation { get; set; }
    }
}
