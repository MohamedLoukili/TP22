using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntitiesLayer;
using StubDataAccessLayer;

namespace BusinessLayer
{
    public class BusinessManager
    {
        static private DalManager Manager = new DalManager();

        public BusinessManager() {
        }

        public List<Pokemon> getPokemon() {
            return Manager.LsPokemon;
        }


        public Pokemon getPokemonByName(string login)
        {
            return Manager.getPokemonByName(login);
        }
        public Pokemon getPokemonByID(int id)
        {
            return Manager.getPokemonByID(id);
        }


        public List<Stade> getStade() {
                return Manager.LsStade;
        }

        public List<Match> getMatch()
        {
            return Manager.LsMatch;
        }
        public List<Tournoi> getTournoi()
        {
            return Manager.LsTournoi;
        }
        public List<Utilisateur> getUsers()
        {
            return Manager.LsUtilisateur;
        }

        public List<String> getPokemonByElem(ETypeElement elem) {
			var query = from pokemonElem in Manager.getPokemonByElement(elem)
						select pokemonElem.Nom;
			return query.ToList();
		}

		public List<String> getPokemonForceVie() {
			var query = from pokemon in Manager.LsPokemon
						where pokemon.carac.Vie > 30 &&
							  pokemon.carac.Force > 3
						select pokemon.Nom;
			return query.ToList();
		}

        public bool CheckUserExist(string login)
        {   
            if (Manager.GetUtilisateurByLogin(login) != null)
            {
                return true;
            }
            else return false;
        }

        public bool CheckConnexionUser(string login, string password)
        {
            bool verif=false;
            try
            {
                Utilisateur user = Manager.GetUtilisateurByLogin(login);
                if (user != null)
                {
                    if (user.getPassword() == password)
                    {
                        verif = true;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }

            return verif;
        }
    }
}
