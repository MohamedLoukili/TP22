using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;

using EntitiesLayer;
using System.IO;

namespace DataAccessLayer
{
    public class DataManager
    {
        private static DataManager instance;
        private static readonly object padlock = new object();
        private static string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Benjamin\\Documents\\bdd.mdf;Integrated Security=True;Connect Timeout=30";

        public DataManager() { }
        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DataManager();
                        }
                    }
                }
                return instance;
            }
        }

        private DataTable SelectByDataAdapter(string request)
        {
            DataTable res = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(res);
            }
            return res;
        }
        private List<string> SelectByDataReader(string request)
        {
            List<string> res = new List<string>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    res.Add(String.Format("{0} ({1})", sqlDataReader.GetString(1), sqlDataReader.GetGuid(0).ToString()));
                }
                sqlConnection.Close();
            }
            return res;
        }
        private List<string> SelectByStoredProcedure(string entryParam)
        {
            List<string> res = new List<string>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "GetBooksByType";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("typeId", entryParam);

                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    res.Add(sqlDataReader.GetString(1));
                }
            }
            return res;
        }
        private int UpdateByCommandBuilder(string request, DataTable authors)
        {
            int res = 0;

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                //SqlTransaction myTrans = sqlConnection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection/*, myTrans*/);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlDataAdapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
                sqlDataAdapter.InsertCommand = sqlCommandBuilder.GetInsertCommand();
                sqlDataAdapter.DeleteCommand = sqlCommandBuilder.GetDeleteCommand();

                sqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                try
                {
                    res = sqlDataAdapter.Update(authors);
                    //myTrans.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Update impossible (attention aux clefs primaires et secondaires) :\n{0}", e.Source);
                    //myTrans.Rollback();
                }
            }
            return res;
        }

        private List<Pokemon> _lsPokemon = null;
        public List<Pokemon> LsPokemon
        {
            get
            {
                if (_lsPokemon == null)
                {
                    DataTable getData = SelectByDataAdapter("SELECT * FROM POKEMON;");
                    _lsPokemon = new List<Pokemon>();
                    foreach (DataRow row in getData.Rows)
                    {
                        _lsPokemon.Add(new Pokemon((int)row.ItemArray[0], (string)row.ItemArray[1], (ETypeElement)row.ItemArray[2], (int)row.ItemArray[3], (int)row.ItemArray[4], (int)row.ItemArray[5], (int)row.ItemArray[6], (int)row.ItemArray[7]));
                    }
                }
                return _lsPokemon;
            }
            set
            {
                int i = 0;
                DataTable setData = SelectByDataAdapter("SELECT * FROM POKEMON;");
                foreach (DataRow dr in setData.Rows)
                    dr.Delete();

                foreach (Pokemon tmp in value)
                {
                    DataRow dr = setData.NewRow();

                    dr[0] = tmp.ID;
                    dr[1] = tmp.Nom;
                    dr[2] = tmp.TypeElement;
                    dr[3] = tmp.carac.Vie;
                    dr[4] = tmp.carac.Force;
                    dr[5] = tmp.carac.Esquive;
                    dr[6] = tmp.carac.Endurance;
                    dr[7] = tmp.carac.Vitesse;
                    ++i;

                    setData.Rows.Add(dr);
                }
                UpdateByCommandBuilder("SELECT * FROM POKEMON", setData);
            }
        }
        public List<Pokemon> selectTable(List<Pokemon> value)
        {
            _lsPokemon = null;
            return LsPokemon;
        }
        public List<Pokemon> updateTable(List<Pokemon> value)
        {
            LsPokemon = value;
            return selectTable(value);
        }
        public int idMax(List<Pokemon> value)
        {
            int IDMax = 0;

            foreach (Pokemon tmp in value)
            {
                if (tmp.ID > IDMax)
                    IDMax = tmp.ID;
            }
            return IDMax;
        }

        private List<Dresseur> _lsDresseur = null;
        public List<Dresseur> LsDresseur
        {
            get
            {
                if (_lsDresseur == null)
                {
                    DataTable getData = SelectByDataAdapter("SELECT * FROM DRESSEUR;");
                    _lsDresseur = new List<Dresseur>();

                    foreach (DataRow row in getData.Rows)
                    {
                        _lsDresseur.Add(new Dresseur((int)row.ItemArray[0], (string)row.ItemArray[1], (int)row.ItemArray[2]));
                    }
                }
                return _lsDresseur;
            }
            set
            {
                int i = 0;
                DataTable setData = SelectByDataAdapter("SELECT * FROM DRESSEUR;");
                foreach (DataRow dr in setData.Rows)
                    dr.Delete();

                foreach (Dresseur tmp in value)
                {
                    DataRow dr = setData.NewRow();

                    dr[0] = tmp.ID;
                    dr[1] = tmp.Nom;
                    dr[2] = tmp.Score;
                    ++i;
                }
                UpdateByCommandBuilder("SELECT * FROM DRESSEUR", setData);
            }
        }
        public List<Dresseur> selectTable(List<Dresseur> value)
        {
            _lsDresseur = null;
            return LsDresseur;
        }
        public List<Dresseur> updateTable(List<Dresseur> value)
        {
            LsDresseur = value;
            return selectTable(value);
        }
        public int idMax(List<Dresseur> value)
        {
            int IDMax = 0;

            foreach (Dresseur tmp in value)
            {
                if (tmp.ID > IDMax)
                    IDMax = tmp.ID;
            }
            return IDMax;
        }

        private List<Match> _lsMatch = null;
        public List<Match> LsMatch
        {
            get
            {
                if (_lsMatch == null)
                {
                    DataTable getData = SelectByDataAdapter("SELECT * FROM MATCH;");
                    _lsMatch = new List<Match>();

                    foreach (DataRow row in getData.Rows)
                    {
                        _lsMatch.Add(new Match((int)row.ItemArray[0], (int)row.ItemArray[1], getPokemonByID((int)row.ItemArray[3]), getPokemonByID((int)row.ItemArray[4]), (Stade)row.ItemArray[5], (int)row.ItemArray[6], (EPhaseTournoi)row.ItemArray[2]));
                    }
                }
                return _lsMatch;
            }
            set
            {
                int i = 0;
                DataTable setData = SelectByDataAdapter("SELECT * FROM MATCH;");
                foreach (DataRow dr in setData.Rows)
                    dr.Delete();

                foreach (Match tmp in value)
                {
                    DataRow dr = setData.NewRow();
                    dr[0] = tmp.ID;
                    dr[1] = tmp.IdPokemonVainqueur;
                    dr[2] = tmp.PhaseTournoi;
                    dr[3] = tmp.Pokemon1.ID;
                    dr[4] = tmp.Pokemon2.ID;
                    dr[5] = tmp.Arene.ID;
                    dr[6] = tmp.IdTournoi;
                    ++i;
                }
                UpdateByCommandBuilder("SELECT * FROM MATCH", setData);
            }
        }
        public List<Match> selectTable(List<Match> value)
        {
            _lsMatch = null;
            return LsMatch;
        }
        public List<Match> updateTable(List<Match> value)
        {
            LsMatch = value;
            return selectTable(value);
        }
        public int idMax(List<Match> value)
        {
            int IDMax = 0;

            foreach (Match tmp in value)
            {
                if (tmp.ID > IDMax)
                    IDMax = tmp.ID;
            }
            return IDMax;
        }

        private List<Stade> _lsStade = null;
        public List<Stade> LsStade
        {
            get
            {
                if (_lsStade == null)
                {
                    DataTable getData = SelectByDataAdapter("SELECT * FROM STADE;");
                    _lsStade = new List<Stade>();

                    foreach (DataRow row in getData.Rows)
                    {
                        Caracteristiques carac = new Caracteristiques((int)row.ItemArray[3], (int)row.ItemArray[4], (int)row.ItemArray[5], (int)row.ItemArray[6], (int)row.ItemArray[7]);
                        _lsStade.Add(new Stade((int)row.ItemArray[0], (string)row.ItemArray[1], (int)row.ItemArray[2], (Caracteristiques)carac));
                    }
                }
                return _lsStade;
            }
            set
            {
                int i = 0;
                DataTable setData = SelectByDataAdapter("SELECT * FROM STADE;");
                foreach (DataRow dr in setData.Rows)
                    dr.Delete();

                foreach (Stade tmp in value)
                {
                    DataRow dr = setData.NewRow();
                    dr[0] = tmp.ID;
                    dr[1] = tmp.Nom;
                    dr[2] = tmp.NbPlaces;
                    dr[3] = tmp.carac.Vie;
                    dr[4] = tmp.carac.Force;
                    dr[5] = tmp.carac.Esquive;
                    dr[6] = tmp.carac.Vitesse;
                    ++i;
                }
                UpdateByCommandBuilder("SELECT * FROM STADE", setData);
            }
        }
        public List<Stade> selectTable(List<Stade> value)
        {
            _lsStade = null;
            return LsStade;
        }
        public List<Stade> updateTable(List<Stade> value)
        {
            LsStade = value;
            return selectTable(value);
        }
        public int idMax(List<Stade> value)
        {
            int IDMax = 0;

            foreach (Stade tmp in value)
            {
                if (tmp.ID > IDMax)
                    IDMax = tmp.ID;
            }
            return IDMax;
        }

        private List<Tournoi> _lsTournoi = null;
        public List<Tournoi> LsTournoi
        {
            get
            {
                if (_lsTournoi == null)
                {
                    DataTable getData = SelectByDataAdapter("SELECT * FROM TOURNOI;");
                    _lsTournoi = new List<Tournoi>();

                    foreach (DataRow row in getData.Rows)
                    {
                        Tournoi tmp = new Tournoi((string)row.ItemArray[2]);
                        foreach (Match match in LsMatch)
                            if (match.IdTournoi == (int)row.ItemArray[0])
                                tmp.AjouterMatch(match);
                        _lsTournoi.Add(tmp);
                    }
                }
                return _lsTournoi;
            }
            set
            {
                int i = 0;
                DataTable setData = SelectByDataAdapter("SELECT * FROM TOURNOI;");
                foreach (DataRow dr in setData.Rows)
                    dr.Delete();

                foreach (Tournoi tmp in value)
                {
                    DataRow dr = setData.NewRow();
                    dr[0] = tmp.ID;
                    dr[1] = tmp.Nom;
                    dr[2] = tmp.nbrMatch;
                    ++i;
                }
                UpdateByCommandBuilder("SELECT * FROM TOURNOI", setData);
            }
        }
        public List<Tournoi> selectTable(List<Tournoi> value)
        {
            _lsTournoi = null;
            return LsTournoi;
        }
        public List<Tournoi> updateTable(List<Tournoi> value)
        {
            LsTournoi = value;
            return selectTable(value);
        }
        public int idMax(List<Tournoi> value)
        {
            int IDMax = 0;

            foreach (Tournoi tmp in value)
            {
                if (tmp.ID > IDMax)
                    IDMax = tmp.ID;
            }
            return IDMax;
        }

        private List<Utilisateur> _lsUtilisateur = null;
        public List<Utilisateur> LsUtilisateur
        {
            get
            {
                if (_lsUtilisateur == null)
                {
                    DataTable getData = SelectByDataAdapter("SELECT * FROM UTILISATEUR;");
                    _lsUtilisateur = new List<Utilisateur>();

                    foreach (DataRow row in getData.Rows)
                    {
                        _lsUtilisateur.Add(new Utilisateur((string)row.ItemArray[1], (string)row.ItemArray[2], (string)row.ItemArray[3], (string)row.ItemArray[4]));
                    }
                }
                return _lsUtilisateur;
            }
            set
            {
                int i = 0;
                DataTable setData = SelectByDataAdapter("SELECT * FROM UTILISATEUR;");
                foreach (DataRow dr in setData.Rows)
                    dr.Delete();

                foreach (Utilisateur tmp in value)
                {
                    Console.WriteLine("Coucou");
                    DataRow dr = setData.NewRow();
                    dr[0] = tmp.ID;
                    dr[1] = tmp.Nom;
                    dr[2] = tmp.Prenom;
                    dr[3] = tmp.Login;
                    dr[4] = tmp.Password;
                    ++i;
                }
                UpdateByCommandBuilder("SELECT * FROM UTILISATEUR", setData);
            }
        }
        public List<Utilisateur> selectTable(List<Utilisateur> value)
        {
            _lsUtilisateur = null;
            return LsUtilisateur;
        }
        public List<Utilisateur> updateTable(List<Utilisateur> value)
        {
            LsUtilisateur = value;
            return selectTable(value);
        }
        public int idMax(List<Utilisateur> value)
        {
            int IDMax = 0;

            foreach (Utilisateur tmp in value)
            {
                if (tmp.ID > IDMax)
                    IDMax = tmp.ID;
            }
            return IDMax;
        }


        public IList<Pokemon> getPokemonByElement(ETypeElement elem)
        {
            var query = from pokemon in LsPokemon
                        where pokemon.TypeElement == elem
                        select pokemon;
            return query.ToList();
        }
        public Pokemon getPokemonByName(string login)
        {
            var query = (from pokemon in LsPokemon
                         where pokemon.Nom == login
                         select pokemon).First();

            return (Pokemon)query;
        }
        public Pokemon getPokemonByID(int id)
        {
            var query = (from pokemon in LsPokemon
                         where pokemon.ID == id
                         select pokemon).First();

            return (Pokemon)query;
        }

        public Utilisateur getUtilisateurByLogin(String plogin)
        {
            var query = (from user in LsUtilisateur
                        where user.getLogin() == plogin
                        select user).First();
            return (Utilisateur)query;
        }

        public void updateAllTable()
        {

            updateTable(LsUtilisateur);
            updateTable(LsDresseur);
            updateTable(LsPokemon);
            updateTable(LsStade);
            updateTable(LsMatch);
            updateTable(LsTournoi);

        }
        public void ToXML(string xmlFile)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add((DataTable)SelectByDataAdapter("SELECT * FROM UTILISATEUR;"));
            ds.Tables.Add((DataTable)SelectByDataAdapter("SELECT * FROM TYPE;"));
            ds.Tables.Add((DataTable)SelectByDataAdapter("SELECT * FROM POKEMON;"));
            ds.Tables.Add((DataTable)SelectByDataAdapter("SELECT * FROM DRESSEUR;"));
            ds.Tables.Add((DataTable)SelectByDataAdapter("SELECT * FROM STADE;"));
            ds.Tables.Add((DataTable)SelectByDataAdapter("SELECT * FROM MATCH;"));
            ds.Tables.Add((DataTable)SelectByDataAdapter("SELECT * FROM TOURNOI;"));

            string dsXml = ds.GetXml();

            using (StreamWriter fs = new StreamWriter(xmlFile))
            {
                ds.WriteXml(fs);
            }
        }

        public override string ToString()
        {
            string str = "";

            str = "\nListe des Utilisateurs :\n";
            foreach (Utilisateur tmp in LsUtilisateur)
            {
                str += tmp.ToString();
            }
            str += "\nListe des dresseurs :\n";
            foreach (Dresseur tmp in LsDresseur)
            {
                str += tmp.ToString();
            }
            str += "\nListe des pokemons :\n";
            foreach (Pokemon tmp in this.LsPokemon)
            {
                str += tmp.ToString();
            }
            str += "\nListe des stades :\n";
            foreach (Stade tmp in LsStade)
            {
                str += tmp.ToString();
            }
            str += "\nListe des matchs :\n";
            foreach (Match tmp in LsMatch)
            {
                str += tmp.ToString();
            }
            str += "\nListe des tournois :\n";
            foreach (Tournoi tmp in LsTournoi)
            {
                str += tmp.ToString();
            }

            return str;
        }
    }
}