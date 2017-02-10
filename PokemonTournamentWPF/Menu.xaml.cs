using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Xml.Serialization;
using System.IO;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public BusinessManager BManager;
        public Menu()
        {
            InitializeComponent();
            BManager = new BusinessManager();
        }

        private void DDeconnection_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Pokemons.IsSelected)
            {
                ListViewPokemon.ItemsSource = BManager.getPokemon();
                AjoutPokemon.Visibility = Visibility.Visible;
                SupprimerPokemon.Visibility = Visibility.Visible;
                ExporterPokemon.Visibility = Visibility.Visible;
                ImporterPokemon.Visibility = Visibility.Visible;
                ImprimerPokemon.Visibility = Visibility.Visible;
            }
            else
            {
                AjoutPokemon.Visibility = Visibility.Collapsed;
                SupprimerPokemon.Visibility = Visibility.Collapsed;
                ExporterPokemon.Visibility = Visibility.Collapsed;
                ImporterPokemon.Visibility = Visibility.Collapsed;
                ImprimerPokemon.Visibility = Visibility.Collapsed;
            }

            if (Stades.IsSelected)
            {
                ListViewStades.ItemsSource = BManager.getStade();
                AjoutStade.Visibility = Visibility.Visible;
                SupprimerStade.Visibility = Visibility.Visible;
                ExporterStade.Visibility = Visibility.Visible;
                ImporterStade.Visibility = Visibility.Visible;
            }
            else
            {
                AjoutStade.Visibility = Visibility.Collapsed;
                SupprimerStade.Visibility = Visibility.Collapsed;
                ExporterStade.Visibility = Visibility.Collapsed;
                ImporterStade.Visibility = Visibility.Collapsed;
            }

            if (Matchs.IsSelected)
                ListViewMatchs.ItemsSource = BManager.getMatch();

            if (Tournois.IsSelected)
                ListViewTournois.ItemsSource = BManager.getTournoi();
        }

        private void AjoutPokemon_Click(object sender, RoutedEventArgs e)
        {
            AddPokemon win = new AddPokemon();
            win.DataContext = ListViewPokemon;
            win.Show();
        }

        private void SupprimerPokemon_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewPokemon.SelectedItem != null)
            {
                if (MessageBox.Show("Confirmer la suppression ?", "Confirmer", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    foreach (Pokemon p in ListViewPokemon.SelectedItems)
                    {
                        BManager.getPokemon().Remove(p);
                    }

                ListViewPokemon.ItemsSource = null;
                ListViewPokemon.ItemsSource = BManager.getPokemon();
            }
        }

        private void ExporterPokemon_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialogue = new Microsoft.Win32.SaveFileDialog();
            dialogue.FileName = "Pokemons";
            dialogue.DefaultExt = ".xml";
            dialogue.Filter = "XML documents |*.xml";

            Nullable<bool> resultat = dialogue.ShowDialog();

            if (resultat == true)
            {
                string filename = dialogue.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof(List<Pokemon>));
                TextWriter textwriter = new StreamWriter(filename);
                serializer.Serialize(textwriter, BManager.getPokemon());
                textwriter.Close();
            }
        }

        private void ImporterPokemon_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialogue = new Microsoft.Win32.OpenFileDialog();
            dialogue.FileName = "Pokemons";
            dialogue.DefaultExt = ".xml";
            dialogue.Filter = "XML documents |*xml";

            Nullable<bool> resultat = dialogue.ShowDialog();

            if (resultat == true)
            {
                string filename = dialogue.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof(List<Pokemon>));
                TextReader textreader = new StreamReader(filename);
                try
                {
                    List<Pokemon> ls = (List<Pokemon>)serializer.Deserialize(textreader);
                    textreader.Close();

                    BManager.getPokemon().Clear();
                    BManager.getPokemon().AddRange(ls);
                }
                catch (InvalidOperationException err)
                {
                    MessageBox.Show("Fichier invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                ListViewPokemon.ItemsSource = null;
                ListViewPokemon.ItemsSource = BManager.getPokemon();
            }
        }

        private void ImprimerPokemon_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialogue = new PrintDialog();
            dialogue.PageRangeSelection = PageRangeSelection.AllPages;
            dialogue.UserPageRangeEnabled = true;

            Nullable<Boolean> print = dialogue.ShowDialog();
            if (print == true)
            {
                FlowDocument doc = new FlowDocument();
                doc.ColumnWidth = 99999;
                doc.FontFamily = new FontFamily("Arial");
                Paragraph para = new Paragraph(new Bold(new Underline(new Run("Liste des Pokémons"))));
                para.TextAlignment = TextAlignment.Center;
                para.FontSize = 24;
                doc.Blocks.Add(para);

                foreach (Pokemon p in BManager.getPokemon())
                {
                    Paragraph tmp = new Paragraph();
                    tmp.TextAlignment = TextAlignment.Left;
                    tmp = new Paragraph(new Run(p.ID + "  -  " + p.Nom));
                    doc.Blocks.Add(tmp);
                    tmp = new Paragraph(new Run(p.TypeElement + "  -  " + p.carac.ToString()));
                    doc.Blocks.Add(tmp);
                    doc.Blocks.Add(new Paragraph(new Run("")));
                }
                IDocumentPaginatorSource idpSource = doc;
                dialogue.PrintDocument(idpSource.DocumentPaginator, "Rapport Pokemon");
            }
        }

        //fonction ajoutée , reference dans la balise Liste view ligne 42 dans Menu.xaml
        private void Detail_Pokemon_Click(object sender, EventArgs e)
        {
            try
            {
                EntitiesLayer.Pokemon item = (Pokemon)ListViewPokemon.SelectedItems[0];
                PokeWindow win = new PokeWindow(item);
                win.Show();
                // this.Close();
            }
            catch { }
        }
        private void Detail_Tournoi_Click(object sender, EventArgs e)
        {
            try
            {
                EntitiesLayer.Tournoi item = (Tournoi)ListViewTournois.SelectedItems[0];
                TournoiWindow win = new TournoiWindow(item);
                win.Show();
                this.Close();
            }
            catch { }
        }

        private void AjoutStade_Click(object sender, RoutedEventArgs e)
        {
            AddStade win = new AddStade();
            win.DataContext = ListViewStades;
            win.Show();
        }

        private void SupprimerStade_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewStades.SelectedItem != null)
            {
                if (MessageBox.Show("Confirmer la suppression ?", "Confirmer", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    foreach (Stade s in ListViewStades.SelectedItems)
                    {
                        BManager.getStade().Remove(s);
                    }

                ListViewStades.ItemsSource = null;
                ListViewStades.ItemsSource = BManager.getStade();
            }
        }

        private void ExporterStade_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialogue = new Microsoft.Win32.SaveFileDialog();
            dialogue.FileName = "Stades";
            dialogue.DefaultExt = ".xml";
            dialogue.Filter = "XML documents |*.xml";

            Nullable<bool> resultat = dialogue.ShowDialog();

            if (resultat == true)
            {
                string filename = dialogue.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof(List<Stade>));
                TextWriter textwriter = new StreamWriter(filename);
                serializer.Serialize(textwriter, BManager.getStade());
                textwriter.Close();
            }
        }

        private void ImporterStade_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialogue = new Microsoft.Win32.OpenFileDialog();
            dialogue.FileName = "Stades";
            dialogue.DefaultExt = ".xml";
            dialogue.Filter = "XML documents |*xml";

            Nullable<bool> resultat = dialogue.ShowDialog();

            if (resultat == true)
            {
                string filename = dialogue.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof(List<Stade>));
                TextReader textreader = new StreamReader(filename);
                try
                {
                    List<Stade> ls = (List<Stade>)serializer.Deserialize(textreader);
                    textreader.Close();

                    BManager.getStade().Clear();
                    BManager.getStade().AddRange(ls);
                }
                catch (InvalidOperationException err)
                {
                    MessageBox.Show("Fichier invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                ListViewStades.ItemsSource = null;
                ListViewStades.ItemsSource = BManager.getStade();
            }
        }
    }
}