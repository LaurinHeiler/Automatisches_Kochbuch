using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Model
{
    public static class Role
    {
        public const string ADMIN = "admin";
        public const string USER = "user";

        /// <summary>
        /// Dieses Set muss alle Rollen beinhalten.
        /// Sonst, wird die IsValid-Methode nicht richtig funktionieren.
        /// </summary>
        private static ISet<string> _validRoles = new HashSet<string>()
        {
            ADMIN, USER
        };

        /// <summary>
        /// Bestimmt ob die Rolle gültig ist oder nicht
        /// </summary>
        /// <param name="role">die Rolle zum überprüfen</param>
        /// <returns>
        /// Es wird true zurück gegeben, falls richtig, sonst false
        /// </returns>

        public static bool IsValid(string role)
        {
            return _validRoles.Contains(role.Trim().ToLower());
        }
    }
}
