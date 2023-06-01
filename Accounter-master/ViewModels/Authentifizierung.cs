using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounter.ViewModels
{
    public static class Authentifizierung
    {
        private static List<Benutzer> erlaubteBenutzer = new List<Benutzer>
    {
        new Benutzer { Benutzername = "admin", Passwort = "geheim" },
        new Benutzer { Benutzername = "user1", Passwort = "password1" },
        new Benutzer { Benutzername = "user2", Passwort = "password2" },
    };

        public static bool PruefeZugriff(string benutzername, string passwort)
        {
            return erlaubteBenutzer.Any(b => b.Benutzername == benutzername && b.Passwort == passwort);
        }
    }
}
