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
    /// Logique d'interaction pour PokeWindow.xaml
    /// </summary>
    public partial class PokeWindow : Window
    {

        //BusinessLayer.BusinessManager _manager;
        public PokeWindow(EntitiesLayer.Pokemon nompokemon)
        {
            InitializeComponent();
            poke.DataContext = nompokemon;
            image.DataContext= System.IO.Directory.GetCurrentDirectory() + "/PokemonImage/" + nompokemon.Nom +".png";
            type.DataContext = System.IO.Directory.GetCurrentDirectory() + "/typeLogo/" + nompokemon.TypeElement + ".png";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }
    }
}
