using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer {
    public class Tournoi : EntityObject
    {
        public List<Match> Matchs { get; set; }
        public String Nom { get; set; }
        public int nbrMatch { get; set; }

        public Tournoi(String pnom)
        {
            Matchs = new List<Match>();
            Nom = pnom;
            nbrMatch = 0;
        }

        public void AjouterMatch(Match match)
        {
         //   if (Matchs.Count < 8)
         //   {
                Matchs.Add(match);
                nbrMatch++;
         //   }
        }
        public void MatchStart(int i)
        {
            if(Matchs[i].IdPokemonVainqueur==-1)
            {
                Matchs[i].MatchStart();
                try
                {
                    if (i % 2 == 0 )
                    {
                        if(Matchs[i + 1].IdPokemonVainqueur != -1)
                        {
                            Pokemon p1 = Matchs[i].getPokemonVainqueur();
                            Pokemon p2 = Matchs[i + 1].getPokemonVainqueur();
                            Matchs.Add(new Match(p1, p2, Matchs[i].Phase_next()));
                            nbrMatch++;
                        }
                    }
                    else if (i % 2 == 1)
                    {
                        if (Matchs[i - 1].IdPokemonVainqueur != -1)
                        {
                            Pokemon p1 = Matchs[i].getPokemonVainqueur();
                            Pokemon p2 = Matchs[i - 1].getPokemonVainqueur();
                            Matchs.Add(new Match(p2, p1, Matchs[i].Phase_next()));
                            nbrMatch++;
                        }
                    }
                }
                catch { }
               
            }            
        }
        public override string ToString()
        {
            return ("Tournoi " + Nom + " " + Matchs.Count + " Matchs");
        }
        
    }
}
