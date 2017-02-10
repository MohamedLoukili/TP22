using BusinessLayer;
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
    /// <summary>
    /// Logique d'interaction pour AddStade.xaml
    /// </summary>
    public partial class AddStade : Window
    {
        public BusinessManager BManager;
        public AddStade()
        {
            InitializeComponent();
            BManager = new BusinessManager();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            String pNom;
            String pPlaces;
            String pBonus;
            int pNbrPlace;
            int pForce;
            Stade nouveau_stade;
            Caracteristiques nouvelle_carac;

            pNom = Nom.Text;
            pPlaces = Nbr_places.Text;
            pBonus = bonus_f.Text;

            if (pNom == "")
                avertissement.Text = "Le champ nom est vide.";
            else
            {
                try
                {
                    pNbrPlace = Convert.ToInt32(pPlaces);
                    pForce = Convert.ToInt32(pBonus);

                    nouvelle_carac = new Caracteristiques(0, pForce, 0, 0, 0);
                    nouveau_stade = new Stade(pNbrPlace, pNom, nouvelle_carac);
                    BManager.getStade().Add(nouveau_stade);
                    ((ListView)DataContext).ItemsSource = null;
                    ((ListView)DataContext).ItemsSource = BManager.getStade();
                    this.Close();
                }
                catch (FormatException er)
                {
                    avertissement.Text = "Les 2e et 3e champ sont des entiers.";
                }
            }
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}