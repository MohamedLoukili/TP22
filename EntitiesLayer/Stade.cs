using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer {
	public class Stade : EntityObject {
        public Caracteristiques carac { get; set; }
        public int NbPlaces { get; set; }
        public String Nom { get; }

		public Stade(int pNbPlaces, String pNom) {
            carac = new Caracteristiques(0, 0, 0, 0, 0);
			NbPlaces = pNbPlaces;
			Nom = pNom;
		}

        //instancier pCarac dans lors de la construction
		public Stade(int pNbPlaces, String pNom, Caracteristiques pCarac) {
			NbPlaces = pNbPlaces;
			Nom = pNom;
            carac = pCarac;
		}
		public override string ToString() {
			return ("Stade " + Nom + " " + NbPlaces + " places");
		}
	}
}
