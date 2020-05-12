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

        private static ISet<string> _validRoles = new HashSet<string>()
        {
            ADMIN, USER
        };

        public static bool IsValid(string role)
        {
            return _validRoles.Contains(role.Trim().ToLower());
        }
    }
}
