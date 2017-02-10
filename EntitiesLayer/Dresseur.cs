using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer {
	public class Dresseur : EntityObject {
		public String Nom { get; }
		public int Score { get; }

		public Dresseur(String pnom) {
			Nom = pnom;
			Score = 0;
		}
		public override string ToString() {
			return ("Dresseur " + Nom + " ; score : " + Score + " points");
		}
	}
}
