using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Automatisches_Kochbuch.Model
{
    public partial class LnkTabUserRezepte
    {

        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("IDUser")]
        public int IdUser { get; set; }

        [Required]
        [DisplayName("IDRezepte")]
        public int IdRezepte { get; set; }

        [Required]
        [DisplayName("AnzahlPortionen")]
        public int AnzahlPortionen { get; set; }

        public virtual TabRezepte IdRezepteNavigation { get; set; }
        public virtual TabUser IdUserNavigation { get; set; }
    }
}
