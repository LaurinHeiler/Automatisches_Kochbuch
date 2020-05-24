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
        public static async Task<TabUser> AuthenticateAsync(this IDataContext context, string vorname, string passwort)
        {
            //entsprechenden User aus der DB holen
            TabUser user = await Task.Run(() =>
            context.TabUser.SingleOrDefaultAsync(x => x.Vorname == vorname &&
                                                 x.Passwort == passwort));
            //falls ein User gefunden wurde, Passwort schwärzen
            if (user != null)
            {
                user.Passwort = null;
            }
            //User zurückgeben
            return user;

        }

        public async static Task<IEnumerable<TabUser>> GetAllAsync(this IDataContext context)
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
                await context.SaveChangesAsynchron();

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

            //falls ein User gefunden wurde, Passwort schwärzen
            if (user != null)
            {
                user.Passwort = null;
            }

            //User zurückgeben
            return user;
        }

        public static async Task<TabUser> RegisterUserAsync(this IDataContext context, TabUser userParam)
        {
            // prüfen, ob der Username schon vergeben ist / WENN USERNAME IN DB
            //bool usernameVorhanden = await context.TabUser.AnyAsync(x =>
            //    x.Username == userParam.Username);

            //falls keine gültige Rolle angegeben wurde,
            //bekommt der neue User die Rolle "user"
            //FÜR DIE ROLEN KLASSE/DB!
            //if (!Role.IsValid(userParam.Role))
            //{
            //    userParam.Role = Role.USER;
            //}

            await context.TabUser.AddAsync(userParam);
            await context.SaveChangesAsynchron();

            return userParam;

        }

        public static async Task<TabUser> UpdateUserdataAsync(this IDataContext context, TabUser userParam)
        {
            //entsprechenden User aus der DB holen
            TabUser userDB = await context.TabUser.SingleOrDefaultAsync(u =>
            u.Id == userParam.Id);

            //falls ein User gefunden wurde, dessen Daten aktualisieren
            if (userDB != null)
            {
                ////prüfen, ob der Username geändert wrude / ERST WENN USER IN DER DB IST
                //if (userDB.UserName != userParam.UserName)
                //{
                //    //prüfen, ob der neue Username noch frei ist
                //    bool usernameVorhanden = await _context.Users.AnyAsync(x =>
                //        x.UserName == userParam.UserName);

                //    if (usernameVorhanden)
                //    {
                //        return null;
                //    }
                //    //neuen Usernamen Übernehmen
                //    userDB.UserName = userParam.UserName;
                //}

                //prüfen, ob die Rolle geändert wurde
                //FÜR DIE ROLEN KLASSE / DB
                //if (userDB.Role != userParam.Role)
                //{

                //    //falls keine gültige Rolle angegeben wurde,
                //    //bekommt der neue User die Rolle "user"
                //    string userParamRole = userParam.Role.Trim().ToLower();
                //    if (!Role.IsValid(userParam.Role))
                //    {
                //        userParam.Role = Role.USER;
                //    }
                //}

                //Daten werden aktualisiert
                userDB.Vorname = userParam.Vorname;
                userDB.Nachname = userParam.Nachname;
                userDB.Id = userParam.Id;
                userDB.Passwort = userParam.Passwort;
                //weitere Properties, wenn die DB endlich geändert ist (Vegi, Vegan, Gluten)

                await context.SaveChangesAsynchron();

                return userDB;
            }

            //es gibt keinen entsprechenden User
            return null;
        }
    }
}
