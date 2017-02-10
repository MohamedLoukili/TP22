using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntitiesLayer
{
    public class Utilisateur
    {
        public String Nom { get; }
        public String Prenom { get; }
        public String Login;
        public String Password { get; }

        public Utilisateur(String pnom, String pprenom, String plogin, String ppassword)
        {
            Nom = pnom;
            Prenom = pprenom;
            Login = plogin;
            Password = ppassword;
        }

        public String getLogin() {
            return Login;
        }

        public String getPassword() {
            return Password;
        }
    }
}
