using Automatisches_Kochbuch.Dtos;
using Automatisches_Kochbuch.Model;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.AspNetCore.Rewrite.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Context
{
    public static class TabUserQueries
    {
        public static async Task<TabUser> AuthenticateAsync(this IDataContext context, string username, string passwort)
        {
            //entsprechenden User aus der DB holen
            TabUser user = await
            context.TabUser.SingleOrDefaultAsync(x => x.Username == username &&
                                                 x.Passwort == passwort);
            //falls ein User gefunden wurde, Passwort schwärzen bzw. unkenntlich machen
            if (user != null)
            {
                user.Passwort = null;
            }
            //User zurückgeben
            return user;

        }

        public async static Task<IEnumerable<TabUser>> GetAllUsersAsync(this IDataContext context)
        {
            List<TabUser> users = await context.TabUser.ToListAsync();

            //User ohne PW zurückgeben
            return users.Select(x => { x.Passwort = null; return x; });
        }

        public static async Task<bool> DeleteUserAsync(this IDataContext context, int id)
        {
            //entsprechenden User aus der DB holen
            TabUser userDB = await context.TabUser.SingleOrDefaultAsync(x =>
            x.Id == id);

            //falls ein User gefunden wurde, diesen Löschen
            if (userDB != null)
            {
                context.TabUser.Remove(userDB);
                //await context.SaveChangesAsynchron();

                return true;
            }

            //es gibt keinen entsprechenden User
            return false;
        }

        public static async Task<TabUser> GetUserAsync(this IDataContext context, int id)
        {
            //entsprechenden User aus der DB holen
            TabUser user = await context.TabUser.SingleOrDefaultAsync(x =>
            x.Id == id);

            //falls ein User gefunden wurde, Passwort schwärzen bzw. unkenntlich machen
            if (user != null)
            {
                user.Passwort = null;
            }

            //User zurückgeben
            return user;
        }




    }
}
