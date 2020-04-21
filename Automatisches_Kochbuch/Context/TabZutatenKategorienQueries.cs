using Automatisches_Kochbuch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Context
{
    //Pro Model eine "Query-Klasse"
    public static class TabZutatenKategorienQueries
    {
       //hier würden nun bestimmte, immer wieder gebrauchte Queries stehen, aber nur für das 
       //Model ZutatenKategorien. Für die anderen Models wird eine neue Query-Klasse erstellt.


        public static IEnumerable<TabZutatenKategorien> AlleZutatenKategorien (this IDataContext context)
        {
            return context.TabZutatenKategorien.ToList();
        }

    }
}
