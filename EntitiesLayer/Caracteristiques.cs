using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntitiesLayer
{
    public class Caracteristiques
    {
        public int Vie { get; set; }
        public int Force { get; set; }
        public int Esquive { get; set; }
        public int Endurance { get; set; }
        public int Vitesse { get; set; }

        public Caracteristiques()
        {
            Vie = 0;
            Force = 0;
            Esquive = 0;
            Endurance = 0;
            Vitesse = 0;
        }

        public Caracteristiques(int pVie, int pForce, int pEsquive, int pEndurance, int pVitesse)
        {
            Vie = pVie;
            Force = pForce;
            Esquive = pEsquive;
            Endurance = pEndurance;
            Vitesse = pVitesse;
        }

        public override string ToString()
        {
            return ("Vie Force Esquive Endurance Vitesse" + Vie + Force + Esquive + Endurance + Vitesse);
        }
    }
}
