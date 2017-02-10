using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer {
	public class Pokemon : EntityObject {
		public Caracteristiques carac { get; set; }
		public ETypeElement TypeElement { get; set; }
		public String Nom { get; set; }

        public Pokemon()
        {
            Nom = "Inconnu";
            TypeElement = 0;
            carac = new Caracteristiques(0, 0, 0, 0, 0);
        }
		public Pokemon(String pNom, ETypeElement pElem)
        {
			Nom = pNom;
			TypeElement = pElem;
            carac = new Caracteristiques(50, 20, 20, 20, 20);
        }
        public Pokemon(String nom, ETypeElement elem, int valvie, int valforce, int valesq, int valendur, int valvit)
        {
            carac = new Caracteristiques(valvie,valforce,valesq,valendur,valvit);
            Nom = nom;
            TypeElement = elem;
        }
        public Pokemon(int id, String nom, ETypeElement elem, int valvie, int valforce, int valesq, int valendur, int valvit)
        {
            ID = id;
            carac = new Caracteristiques(valvie, valforce, valesq, valendur, valvit);
            Nom = nom;
            TypeElement = elem;
        }

        public override String ToString() {
            return ("Pokemon " + TypeElement + " " + Nom + " ; ID " + ID + " ; Vie " + carac.Vie + " Force " + carac.Force + " Esquive " + carac.Esquive + " Endurance " + carac.Endurance + " Vitesse " + carac.Vitesse);
		}
	}
}
