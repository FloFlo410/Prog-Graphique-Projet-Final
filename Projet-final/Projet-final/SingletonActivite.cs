using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class SingletonActivite
    {
        static SingletonActivite instance = null;
        ObservableCollection<Activite> liste_activites;
        static Activite activiteSelectionne;

        MySqlConnection con = new MySqlConnection(
             "Server=cours.cegep3r.info;Database=a2024_420335ri_eq4;Uid=2356591;Pwd=2356591;");

        public SingletonActivite()
        {
            liste_activites = new ObservableCollection<Activite>();
            loadDataInList();
        }
        public static SingletonActivite getInstance()
        {
            if (instance == null)
                instance = new SingletonActivite();

            return instance;
        }

        public void setActiviteSelectione(Activite activite)
        {
            activiteSelectionne = activite;
        }
        public Activite getActiviteSelectione()
        {
            return activiteSelectionne;
        }

        public ObservableCollection<Activite> getListeActivites()
        {
            return liste_activites;
        }

        public void ClearList()
        {
            liste_activites.Clear();
        }

        public void loadDataInList()
        {
            ClearList();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from activite";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Activite activite = new Activite(r[0].ToString(), r[1].ToString(), Double.Parse(r[2].ToString()), Int32.Parse(r[3].ToString()));
                liste_activites.Add(activite);
            }
            r.Close();
            con.Close();
            setMoyennesActivites();
        }



        public string modifier(Activite newActivite, Activite oldActivite)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"update activite set nom ='{newActivite.Nom}', type = '{newActivite.Type}', coutOrganisation={newActivite.CoutOrganisation}, prixVente ={newActivite.PrixVente} where nom ='{oldActivite.Nom}' AND type = '{oldActivite.Type}' ";

                con.Open();
                int i = commande.ExecuteNonQuery();

                con.Close();
                loadDataInList();
                return "réussi";
            }
            catch (Exception ex)
            {
                con.Close();
                if(ex.HResult == -2147467259)
                {
                    return "Cette activité existe déjà avec la même catégorie";
                }
                
            }

            return "erreur";


        }

        public ObservableCollection<Seance> getSeancesPourActivite(string nomActivite, string typeActivite)
        {
            ObservableCollection<Seance> seances = new ObservableCollection<Seance>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from seance Where activiteNom = @nomActivite AND activiteType = @typeActivite";
            commande.Parameters.AddWithValue("@nomActivite", nomActivite);
            commande.Parameters.AddWithValue("@typeActivite", typeActivite);
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Seance seance = new Seance(Int32.Parse(r[0].ToString()), r[1].ToString(), r[2].ToString(), (DateTime) r[3] ,Int32.Parse(r[4].ToString()));
                seances.Add(seance);
            }
            r.Close();
            con.Close();

            return seances;
        }

        public int reserverSeanceActivite(string noIdentification, int idSeance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO participation (idAdherent, idSeance) VALUES (@idAdherent, @idSeance)";

                commande.Parameters.AddWithValue("@idAdherent", noIdentification);
                commande.Parameters.AddWithValue("@idSeance", idSeance);
               

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();

                loadDataInList();

                return 0;
            }
            catch (Exception ex)
            {

                con.Close();
                Console.WriteLine(ex.Message);
                if (ex.Message == "Cet adhérent est déjà incrit à cette séance.")
                    return -2;
                else if (ex.Message == "Il n'y a plus de places pour cette séance.")
                    return -3;
                return -1;
            }
        }

        // Statistiques
        public int nbTotalActivite()
        {

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select nombre_total_activite()";
            con.Open();
            int i = Int32.Parse(commande.ExecuteScalar().ToString());
            con.Close();
            return i;
        }

        public double moyenneNoteParActivite(string nomActivite, string typeActivite)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select moyenne_evaluation_selon_activite(@nomActivite, @typeActivite)";
            commande.Parameters.AddWithValue("@nomActivite", nomActivite);
            commande.Parameters.AddWithValue("@typeActivite", typeActivite);
            con.Open();
            double i = 0;
            Double.TryParse(commande.ExecuteScalar().ToString(), out i);
            con.Close();
            return i;
        }

        public void setMoyennesActivites()
        {
            foreach(Activite activite in liste_activites)
            {
                activite.MoyenneNote = moyenneNoteParActivite(activite.Nom, activite.Type);
            }
        }

        public void supprimer(Activite activite)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"DELETE FROM activite WHERE nom ='{activite.Nom}' AND type = '{activite.Type}'";

                con.Open();
                int i = commande.ExecuteNonQuery();

                con.Close();
                loadDataInList();
            }
            catch (Exception ex)
            {
                con.Close();

            }
        }


        public string ajouter(Activite activite)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("ajouter_activite");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("_nom", activite.Nom);
                commande.Parameters.AddWithValue("_type", activite.Type);
                commande.Parameters.AddWithValue("_coutOrganisation", activite.CoutOrganisation);
                commande.Parameters.AddWithValue("_prixVente", activite.PrixVente);


                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                loadDataInList();
                return "réussi";

            }
            catch (Exception ex)
            {
                con.Close();
                return ex.Message;
            }
        }

    }
}
