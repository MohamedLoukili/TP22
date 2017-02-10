using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EntitiesLayer;

namespace PokemonTournamentWPF
{
        
    public partial class AddMatch : Window
    {
        public BusinessLayer.BusinessManager BManager;
        EntitiesLayer.Tournoi TheTournoi;

        public AddMatch(EntitiesLayer.Tournoi tournoi)
        {
            InitializeComponent();
            BManager = new BusinessLayer.BusinessManager();
            TheTournoi = tournoi;
            List<Pokemon> listPoke = new List<Pokemon>();
            List<Pokemon> listtmp = BManager.getPokemon();
            for(int i=0;i<listtmp.Count;i++)
            {
                listPoke.Add(listtmp.ElementAt(i));
            }
            for(int i=0;i<TheTournoi.nbrMatch;i++)
            {
                listPoke.Remove(TheTournoi.Matchs[i].Pokemon1);
                listPoke.Remove(TheTournoi.Matchs[i].Pokemon2);
            }
            pokemon1.DataContext = listPoke;
            pokemon2.DataContext = listPoke;
        }
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Pokemon poke1=new Pokemon();
            Pokemon poke2 = new Pokemon();
            poke1 = (Pokemon)pokemon1.SelectedItem;
            poke2 = (Pokemon)pokemon2.SelectedItem;
            if (poke1 == null || poke2 == null)
            {
                avertissement.Text = "Veuillez bien entrer 2 pokemons";
            }
            else if (poke1 == poke2)
            {
                avertissement.Text = "Vous ne pouvez pas choisir le meme pokemon";
            }
            else
            {
                Match newmatch = new Match(poke1, poke2);
                TheTournoi.AjouterMatch(newmatch);
                BManager.getMatch().Add(newmatch);
                TournoiWindow win = new TournoiWindow(TheTournoi);
                win.Show();
                this.Close();
            }
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
