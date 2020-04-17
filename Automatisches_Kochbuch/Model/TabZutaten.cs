using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Automatisches_Kochbuch.Model
{
    public partial class TabZutaten
    {
        public TabZutaten()
        {
            LnkTabRezeptZutaten = new HashSet<LnkTabRezeptZutaten>();
            LnkTabUserZutaten = new HashSet<LnkTabUserZutaten>();
        }

        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Zutat")]
        public string Zutat { get; set; }

        [Required]
        [DisplayName("IdZutatEinheit")]
        public int IdZutatEinheit { get; set; }

        [Required]
        [DisplayName("IdZutatKategorie")]
        public int IdZutatKategorie { get; set; }

        [DisplayName("Vegetarisch")]
        public bool Vegetarisch { get; set; }

        [DisplayName("Vegan")]
        public bool Vegan { get; set; }

        [DisplayName("glutenfrei")]
        public bool Glutenfrei { get; set; }

        public virtual ICollection<LnkTabRezeptZutaten> LnkTabRezeptZutaten { get; set; }
        public virtual ICollection<LnkTabUserZutaten> LnkTabUserZutaten { get; set; }
    }
}
