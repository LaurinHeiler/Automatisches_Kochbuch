using Automatisches_Kochbuch.Model;
using Microsoft.AspNetCore.Rewrite.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Context
{
    public static class TabUserQueries
    {
        public static async Task<TabUser> Authenticate(this IDataContext context, string vorname, string passwort)
        {
            //entsprechenden User aus der DB holen
            TabUser user = await Task.Run(() =>
            context.TabUser.SingleOrDefault(x => x.Vorname == vorname &&
                                                 x.Passwort == passwort));
            //falls ein User gefunden wurde, Passwort schwärzen
            if (user != null)
            {
                user.Passwort = null;
            }
            //User zurückgeben
            return user;

        }

        public async static Task<IEnumerable<TabUser>> GetAll(this IDataContext context)
        {
            //User mit (ohne (wenn es funktionieren würde)) PW zurückgeben
            return await Task.Run(() =>
                //context.TabUser.Select(x => { x.Passwort = null; return x }));
                context.TabUser.ToList());
        }
    }
}
