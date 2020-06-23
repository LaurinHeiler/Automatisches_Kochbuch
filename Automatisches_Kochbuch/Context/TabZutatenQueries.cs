using Automatisches_Kochbuch.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Context
{
    public static class TabZutatenQueries
    {
        public async static Task<IEnumerable<TabZutaten>> GetAllZutatenAsync(this IDataContext context)
        {
            List<TabZutaten> zutaten = await context.TabZutaten.ToListAsync();

            return zutaten;
        }


    }
}
