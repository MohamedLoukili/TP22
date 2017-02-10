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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BManager = new BusinessManager();
        }

        private BusinessManager BManager;

        private void BConnecter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BManager.CheckConnexionUser((string)log.Text.ToString(), (string)pass.Password))
                {
                    Menu win = new Menu();
                    win.Show();
                    this.Close();
                }
                else avertissement.Text = "Login ou mot de passe incorrect !!";
            }
            catch { avertissement.Text = "Login ou mot de passe incorrect !!"; }
        }

        private void adduser_Click(object sender, RoutedEventArgs e)
        {
            adduser win = new adduser();
            win.Show();
            this.Close();
        }
    }
}
