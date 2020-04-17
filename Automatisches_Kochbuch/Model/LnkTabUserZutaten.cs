using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Automatisches_Kochbuch.Model
{
    public partial class LnkTabUserZutaten
    {

        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("IDUser")]
        public int IdUser { get; set; }

        [Required]
        [DisplayName("IDZutaten")]
        public int IdZutaten { get; set; }

        [Required]
        [DisplayName("Menge")]
        public int Menge { get; set; }

        public virtual TabUser IdUserNavigation { get; set; }
        public virtual TabZutaten IdZutatenNavigation { get; set; }
    }
}
