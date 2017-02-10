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
using BusinessLayer;
using EntitiesLayer;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Logique d'interaction pour AddPokemon.xaml
    /// </summary>
    public partial class AddPokemon : Window
    {
        public BusinessManager BManager;
        public AddPokemon()
        {
            InitializeComponent();
            BManager = new BusinessManager();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            String pNom;
            ETypeElement pType;
            Pokemon nouveau_poke;

            pNom = Nom.Text;
            pType = (ETypeElement)Type.SelectedIndex;

            if (pNom == "")
                avertissement.Text = "Ecrivez un nom.";
            else
            {
                if ((int)pType == -1)
                    avertissement.Text = "Sélectionner un type.";
                else
                {
                    nouveau_poke = new Pokemon(pNom, pType);
                    BManager.getPokemon().Add(nouveau_poke);
                    ((ListView)DataContext).ItemsSource = null;
                    ((ListView)DataContext).ItemsSource = BManager.getPokemon();
                    this.Close();
                }
            }
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
