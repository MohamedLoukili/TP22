using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntitiesLayer;

namespace PokemonTournamentConsole {
	class Program {
		static void Main(string[] args) {
			Pokemon Elector = new Pokemon("Elector", ETypeElement.Electrique);
			Console.WriteLine(Elector);
			Pokemon Laggron = new Pokemon("Laggron", ETypeElement.Eau);
			Console.WriteLine(Laggron);
			Pokemon Salameche = new Pokemon("Salameche", ETypeElement.Feu);
			Console.WriteLine(Salameche);

			Match match1 = new Match(ref Elector, ref Laggron);
			Match match2 = new Match(ref Salameche, ref Laggron, EPhaseTournoi.DemiFinale);
			Match match3 = new Match(new Pokemon("Bulbizarre", ETypeElement.Plante), new Pokemon("Kadabra", ETypeElement.Psy));
			Console.WriteLine(match1);
			Console.WriteLine(match2);
			Console.WriteLine(match3);

			Stade stade1 = new Stade(500, "Stade or", new Caracteristiques(0,2,0,0,0));
			Stade stade2 = new Stade(1000, "Stade platine", new Caracteristiques(0,0,-2,0,0));
			Console.WriteLine(stade1);
			Console.WriteLine(stade2);

			Dresseur dresseur1 = new Dresseur("Sacha");
			Dresseur dresseur2 = new Dresseur("Ondine");
			Console.WriteLine(dresseur1);
			Console.WriteLine(dresseur2);


            /*BusinessLayer.BusinessManager BManager = new BusinessLayer.BusinessManager();

            Console.WriteLine(BusinessLayer.BusinessManager.Manager.ToString());
            BManager.getUsers().Add(new EntitiesLayer.Utilisateur("1", "", "", ""));
            foreach(Utilisateur tmp in BManager.getUsers())
                Console.WriteLine(tmp.ToString());
            List<Utilisateur> cp = BManager.getUsers();
            BusinessLayer.BusinessManager.Manager.updateTable(cp);
            Console.WriteLine(BusinessLayer.BusinessManager.Manager.ToString());*/
            Console.ReadKey();
		}
	}
}
