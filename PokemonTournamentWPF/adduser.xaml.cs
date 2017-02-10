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
    /// Logique d'interaction pour adduser.xaml
    /// </summary>
    public partial class adduser : Window
    {
        
        public BusinessLayer.BusinessManager BManager;
        public adduser()
        {
            InitializeComponent();
            BManager = new BusinessLayer.BusinessManager();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string newnom = (string)nom.Text.ToString();
            string newprenom = (string)prenom.Text.ToString();
            string newlogin= (string)Login.Text.ToString();
            string newmdp=(string)mdp.Password;
            string newmdp2 = (string)mdpconfirmed.Password;
            if (newnom=="" || newprenom=="" || newlogin == "" || newmdp == "" || newmdp2 == "")
            {
                avertissement.Text = "Veillez remplir tous les champs !";
            }
            else if (newmdp != newmdp2)
            {
                avertissement.Text = "Les mots de passe doivent etre identiques !";
            }
            else 
            {
                try
                {
                    if (BManager.CheckUserExist(newlogin))
                    {
                        avertissement.Text = "Ce login existe déjà !";
                    }
                }
                catch {
                    BManager.getUsers().Add(new EntitiesLayer.Utilisateur(newnom, newprenom, newlogin, newmdp));
                    Retour_Click(sender, e);
                }


            }

        }
    }
}
