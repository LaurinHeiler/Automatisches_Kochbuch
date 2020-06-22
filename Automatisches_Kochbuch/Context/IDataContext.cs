using Automatisches_Kochbuch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Context
{
    public interface IDataContext
    {
        DbSet<LnkTabRezeptZutaten> LnkTabRezeptZutaten { get; set; }
        DbSet<LnkTabUserRezepte> LnkTabUserRezepte { get; set; }
        DbSet<LnkTabUserZutaten> LnkTabUserZutaten { get; set; }
        DbSet<TabRezepte> TabRezepte { get; set; }
        DbSet<TabUser> TabUser { get; set; }
        DbSet<TabZutaten> TabZutaten { get; set; }
        DbSet<TabZutatenEinheit> TabZutatenEinheit { get; set; }
        DbSet<TabZutatenKategorien> TabZutatenKategorien { get; set; }

        void Save();
        Task SaveChangesAsynchron();
    }
}
