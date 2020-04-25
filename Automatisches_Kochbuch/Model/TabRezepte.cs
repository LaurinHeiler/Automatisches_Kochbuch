using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Automatisches_Kochbuch.Model
{
    public partial class TabRezepte
    {
        public TabRezepte()
        {
            LnkTabRezeptZutaten = new HashSet<LnkTabRezeptZutaten>();
            LnkTabUserRezepte = new HashSet<LnkTabUserRezepte>();
        }


        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Rezeptname")]
        public string Rezeptname { get; set; }

        [DisplayName("Vegetarisch")]
        public bool Vegetarisch { get; set; }

        [DisplayName("Vegan")]
        public bool Vegan { get; set; }

        [DisplayName("glutenfrei")]
        public bool Glutenfrei { get; set; }

        [Required]
        [DisplayName("Zubereitung")]
        public string Zubereitung { get; set; }

        [DisplayName("Bild")]
        public byte[] Bild { get; set; }

        public virtual ICollection<LnkTabRezeptZutaten> LnkTabRezeptZutaten { get; set; }
        public virtual ICollection<LnkTabUserRezepte> LnkTabUserRezepte { get; set; }
    }
}
