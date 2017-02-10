using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer {
	public class Match : EntityObject {
		public int IdPokemonVainqueur { get; set; }
        public EPhaseTournoi PhaseTournoi { get; }
        public Pokemon Pokemon1 { get; }
        public Pokemon Pokemon2 { get; }
        public Stade Arene { get; }

        public Match(ref Pokemon ppokemon1, ref Pokemon ppokemon2, EPhaseTournoi pphase = EPhaseTournoi.QuartFinale) {
			PhaseTournoi = pphase;
			Pokemon1 = ppokemon1;
			Pokemon2 = ppokemon2;
            IdPokemonVainqueur = -1;

        }
		public Match(Pokemon ppokemon1, Pokemon ppokemon2, EPhaseTournoi pphase = EPhaseTournoi.QuartFinale) {
			PhaseTournoi = pphase;
			Pokemon1 = ppokemon1;
			Pokemon2 = ppokemon2;
            IdPokemonVainqueur = -1;
        }

        public EPhaseTournoi Phase_next()
        {
            if (PhaseTournoi == EPhaseTournoi.HuitiemeFinale) return EPhaseTournoi.QuartFinale;
            else if (PhaseTournoi == EPhaseTournoi.QuartFinale) return EPhaseTournoi.DemiFinale;
            else return EPhaseTournoi.Finale;

        }
        public double Typecoeff(ETypeElement elem1, ETypeElement elem2)
        {
            if (elem1 > elem2) { return 2; /*Console.WriteLine(elem1+" super efficace contre "+elem2);*/ }
            else if (elem1 < elem2) { return 0.5; /*Console.WriteLine(elem1 + " pas tres efficace contre " + elem2);*/ }
            else return 1;
        }

        public void MatchStart()
        {
            //            0 VIE, 1 FORCE, 2 AGILITE, 3 ENDURANCE, 4 VITESSE
            Random rnd = new Random();
            int val1 = Pokemon1.carac.Vie;Console.WriteLine("La vie de "+Pokemon1.Nom+" est "+val1);
            int val2 = Pokemon2.carac.Vie; Console.WriteLine("La vie de " + Pokemon2.Nom + " est " +val2);
            Boolean premier = Pokemon1.carac.Vitesse > Pokemon2.carac.Vitesse;
            Console.WriteLine("Debut Combat");
            while (val1>0 && val2>0)
            {
                if (premier)
                {
                    //pokemon1 attaque pokemon2
                    val2 = Pokemon1attaque(rnd, val2); Console.WriteLine("La vie de " + Pokemon2.Nom + " est " + val2);
                    if (val2 > 0) { val1 = Pokemon2attaque(rnd, val1); Console.WriteLine("La vie de " + Pokemon1.Nom + " est " + val1); }
                    }
                else
                {
                    //pokemon2 attaque pokemon1
                    val1 = Pokemon2attaque(rnd, val1); Console.WriteLine("La vie de " + Pokemon1.Nom + " est " + val1);
                    if (val1 > 0) { val2 = Pokemon1attaque(rnd, val2); Console.WriteLine("La vie de " + Pokemon2.Nom + " est " + val2); }
                    }
            }
            Console.WriteLine("Fin du combat car "+Pokemon1.Nom+"="+val1+" et "+Pokemon2.Nom+"="+val2);
        }
        //pokemon1 attaque pokemon2
        public int Pokemon1attaque(Random rnd,int viepokemon2)
        {
            Console.WriteLine(Pokemon1.Nom+" attaque "+ Pokemon2.Nom+"!!");
            int esquive = rnd.Next(0, 100);
            if (esquive > Pokemon2.carac.Esquive)
            {
                viepokemon2 -= (int)(Pokemon1.carac.Force * Typecoeff(Pokemon1.TypeElement, Pokemon2.TypeElement));
                Console.WriteLine("!!!!!!!!!!! " + Pokemon2.Nom + " -" + (int)(Pokemon1.carac.Force * Typecoeff(Pokemon1.TypeElement, Pokemon2.TypeElement)) + "!!!!!!!!!!");
                if (viepokemon2 <= 0) { IdPokemonVainqueur = Pokemon1.ID;  }
            }
            else Console.WriteLine( Pokemon2.Nom+" esquive l'attaque");
            return viepokemon2;
        }
        //pokemon2 attaque pokemon1
        public int Pokemon2attaque(Random rnd, int viepokemon1)
        {
            Console.WriteLine(Pokemon2.Nom + " attaque " + Pokemon1.Nom + "!!");
            int esquive = rnd.Next(0, 100);
            if (esquive > Pokemon1.carac.Esquive)
            {
                viepokemon1 -= (int)(Pokemon2.carac.Force * Typecoeff(Pokemon2.TypeElement, Pokemon1.TypeElement));
                Console.WriteLine("!!!!!!!!!!! " + Pokemon1.Nom + " -" + (int)(Pokemon2.carac.Force * Typecoeff(Pokemon2.TypeElement, Pokemon1.TypeElement)) + "!!!!!!!!!!");
                if (viepokemon1 <= 0) { IdPokemonVainqueur = Pokemon2.ID;  }
            }
            else Console.WriteLine(Pokemon1.Nom + " esquive l'attaque");
            return viepokemon1;
        }
        public Pokemon getPokemonVainqueur()
        {
            if (IdPokemonVainqueur == Pokemon1.ID) return Pokemon1;
            else if (IdPokemonVainqueur == Pokemon2.ID) return Pokemon2;
            else return null;
        }

        public override String ToString() {
			return ("Match " + ID + " : " + Pokemon1.Nom + " VS " + Pokemon2.Nom );
		}
	}
}
