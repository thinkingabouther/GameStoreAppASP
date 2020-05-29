using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GameStoreAppCF.Models
{
    public class LoginVerifier
    {        
        private static string saltValue = "abcd";

        private static Dictionary<Role, string> users_hash = new Dictionary<Role, string>
        {
            { Role.Admin, "73N4Hv/Fd0EA+H/i9DekNQ==" }, // 1234
            { Role.Owner, "oEc0O/S6Zf1MTvlZbJKWDA==" } // 12345
        };

        private static Dictionary<Role, List<string>> users_actions = new Dictionary<Role, List<string>>
        {
            { Role.Admin, new List<string>()
            {
                "Authors", "Clients", "Games", "Genres", "Types", "Orders", "Reviews", "Manufacturers"
            }
            },
            { Role.Owner, new List<string>()
            {
                "Reports"
            }
            }
        };
        public static bool VerifyAccess(Role role, string password)
        {
            var currentHash = GetHash(password, saltValue);
            return currentHash == users_hash[role];
        }

        public static Role GetRoleByAction(string action)
        {
            foreach(var user_action in users_actions)
            {
                foreach(var action_name in user_action.Value)
                {
                    if (action_name == action) return user_action.Key;
                }
            }
            return Role.Admin;
        }
        private static string GetHash(string password, string salt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] digest = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            string base64digest = Convert.ToBase64String(digest, 0, digest.Length);
            return base64digest;
        }

        public enum Role
        {
            Admin,
            Owner
        }
    }
}