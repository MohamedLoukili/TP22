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

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class TournoiWindow : Window
    {
        BusinessLayer.BusinessManager _manager;
        EntitiesLayer.Tournoi TheTournoi;
        int first = 4;
        int second = 5;

        public TournoiWindow(EntitiesLayer.Tournoi tournoi)
        {
            TheTournoi = tournoi;
            _manager = new BusinessLayer.BusinessManager();
            InitializeComponent();
            letournoi.DataContext = tournoi;
            if (TheTournoi.nbrMatch > 0)
            {
                match_Q1_1.DataContext = cheminImage(TheTournoi.Matchs[0].Pokemon1.Nom);
                match_Q1_2.DataContext = cheminImage(TheTournoi.Matchs[0].Pokemon2.Nom);
                if (TheTournoi.nbrMatch > 1)
                {
                    match_Q2_1.DataContext = cheminImage(TheTournoi.Matchs[1].Pokemon1.Nom);
                    match_Q2_2.DataContext = cheminImage(TheTournoi.Matchs[1].Pokemon2.Nom);
                    if (TheTournoi.Matchs[0].IdPokemonVainqueur != -1)
                    {
                        match_D1_1.DataContext = cheminImage(TheTournoi.Matchs[0].getPokemonVainqueur().Nom);
                    }
                    if (TheTournoi.Matchs[1].IdPokemonVainqueur != -1)
                    {
                        match_D1_2.DataContext = cheminImage(TheTournoi.Matchs[1].getPokemonVainqueur().Nom);
                        if (TheTournoi.Matchs[0].IdPokemonVainqueur != -1)
                        {
                            try
                            {
                                if (TheTournoi.Matchs[first].Pokemon1.ID != TheTournoi.Matchs[0].getPokemonVainqueur().ID)
                                { first = 5; second = 4; }
                            }
                            catch { first = 5; second = 4; }
                            if (TheTournoi.Matchs[first].IdPokemonVainqueur != -1)
                            {
                                match_F_1.DataContext = cheminImage(TheTournoi.Matchs[first].getPokemonVainqueur().Nom);
                            }
                        }
                    }

                    if (TheTournoi.nbrMatch > 2)
                        {
                            match_Q3_1.DataContext = cheminImage(TheTournoi.Matchs[2].Pokemon1.Nom);
                            match_Q3_2.DataContext = cheminImage(TheTournoi.Matchs[2].Pokemon2.Nom);
                            if (TheTournoi.Matchs[2].IdPokemonVainqueur != -1)
                            {
                                match_D2_1.DataContext = cheminImage(TheTournoi.Matchs[2].getPokemonVainqueur().Nom);
                            }
                        if (TheTournoi.nbrMatch > 3)
                        {
                            match_Q4_1.DataContext = cheminImage(TheTournoi.Matchs[3].Pokemon1.Nom);
                            match_Q4_2.DataContext = cheminImage(TheTournoi.Matchs[3].Pokemon2.Nom);
                            if (TheTournoi.Matchs[3].IdPokemonVainqueur != -1)
                            {
                                match_D2_2.DataContext = cheminImage(TheTournoi.Matchs[3].getPokemonVainqueur().Nom);
                                if (TheTournoi.Matchs[2].IdPokemonVainqueur != -1)
                                {
                                    try
                                    {
                                        if (TheTournoi.Matchs[second].Pokemon1.ID != TheTournoi.Matchs[2].getPokemonVainqueur().ID)
                                        { first = 5; second = 4; }
                                    }
                                    catch { first = 5; second = 4; }

                                    if (TheTournoi.Matchs[second].IdPokemonVainqueur != -1)
                                    {
                                        match_F_2.DataContext = cheminImage(TheTournoi.Matchs[second].getPokemonVainqueur().Nom);
                                        try
                                        {
                                            if (TheTournoi.Matchs[first].IdPokemonVainqueur != -1)
                                            {
                                                if (TheTournoi.Matchs[6].IdPokemonVainqueur != -1)
                                                {
                                                    gagnant.DataContext = cheminImage(TheTournoi.Matchs[6].getPokemonVainqueur().Nom);
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                                                  
                    }
                }
            }
        }

       
        // DEBUT DES VS
        private void Match_Q1_start(object sender, RoutedEventArgs e)
        {
            if (TheTournoi.nbrMatch >= 4)
            {
                try
                {
                    if (TheTournoi.Matchs[0].IdPokemonVainqueur == -1)
                    {
                        TheTournoi.MatchStart(0);
                        match_D1_1.DataContext = cheminImage(TheTournoi.Matchs[0].getPokemonVainqueur().Nom);
                    }
                }
                catch { }
            }
        }
        private void Match_Q2_start(object sender, RoutedEventArgs e)
        {
            if (TheTournoi.nbrMatch >= 4)
            {
                try
                {
                    if (TheTournoi.Matchs[1].IdPokemonVainqueur == -1)
                    {
                        TheTournoi.MatchStart(1);
                        match_D1_2.DataContext = cheminImage(TheTournoi.Matchs[1].getPokemonVainqueur().Nom);
                    }
                }
                catch { }
            }
        }
        private void Match_Q3_start(object sender, RoutedEventArgs e)
        {
            if (TheTournoi.nbrMatch >= 4)
            {
                try
                {
                    if (TheTournoi.Matchs[2].IdPokemonVainqueur == -1)
                    {
                        TheTournoi.MatchStart(2);
                        match_D2_1.DataContext = cheminImage(TheTournoi.Matchs[2].getPokemonVainqueur().Nom);
                        if (TheTournoi.Matchs[3].IdPokemonVainqueur != -1 && (TheTournoi.Matchs[0].IdPokemonVainqueur == -1 || TheTournoi.Matchs[1].IdPokemonVainqueur == -1))
                        {
                            first = 5;
                            second = 4;
                        }
                    }
                }
                catch { }
            }
        }
        private void Match_Q4_start(object sender, RoutedEventArgs e)
        {
            if (TheTournoi.nbrMatch >= 4)
            {
                try
                {
                    if (TheTournoi.Matchs[3].IdPokemonVainqueur == -1)
                    {
                        TheTournoi.MatchStart(3);
                        match_D2_2.DataContext = cheminImage(TheTournoi.Matchs[3].getPokemonVainqueur().Nom);
                        //gestion de l'ordre des demis finales
                        if (TheTournoi.Matchs[2].IdPokemonVainqueur != -1 && (TheTournoi.Matchs[0].IdPokemonVainqueur == -1 || TheTournoi.Matchs[1].IdPokemonVainqueur == -1))
                        {
                            first = 5;
                            second = 4;
                        }
                    }
                }
                catch { }
            }
        }
        private void Match_D1_start(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TheTournoi.Matchs[first].IdPokemonVainqueur == -1)
                {
                    TheTournoi.MatchStart(first);
                    match_F_1.DataContext = cheminImage(TheTournoi.Matchs[first].getPokemonVainqueur().Nom);
                }
            }
            catch { }
        }
        private void Match_D2_start(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TheTournoi.Matchs[second].IdPokemonVainqueur == -1)
                {
                    TheTournoi.MatchStart(second);
                    match_F_2.DataContext = cheminImage(TheTournoi.Matchs[second].getPokemonVainqueur().Nom);
                }
            }
            catch { }
        }
        private void Match_F_start(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TheTournoi.Matchs[6].IdPokemonVainqueur == -1)
                {
                    TheTournoi.MatchStart(6);
                    lumiere.DataContext = cheminImage("lumiere");
                    gagnant.DataContext = cheminImage(TheTournoi.Matchs[6].getPokemonVainqueur().Nom);
                }
            }
            catch { }
        }

        // FIN DES VS

        public string cheminImage(string nomimage)
        {
            return System.IO.Directory.GetCurrentDirectory() + "/PokemonImage/" + nomimage + ".png";
        }
        private void retour_btn(object sender, RoutedEventArgs e)
        {
            Menu win = new Menu();
            win.Show();
            this.Close();
        }
        private void addMatch_Click(object sender, RoutedEventArgs e)
        {
            if (TheTournoi.nbrMatch < 4)
            {
                AddMatch win = new AddMatch(TheTournoi);
                win.DataContext = letournoi;
                win.Show();
                this.Close();
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
