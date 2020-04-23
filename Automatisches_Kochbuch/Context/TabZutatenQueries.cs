using Automatisches_Kochbuch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Context
{
    public static class TabZutatenQueries
    {
        public static IEnumerable<TabZutaten> AlleZutaten(this IDataContext context)
        {
            return context.TabZutaten.ToList();
        }
    }
}
