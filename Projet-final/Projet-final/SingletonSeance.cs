using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class SingletonSeance
    {

        static SingletonSeance instance = null;
        ObservableCollection<Seance> liste_Seance;

        MySqlConnection con = new MySqlConnection(
             "Server=cours.cegep3r.info;Database=a2024_420335ri_eq4;Uid=2356591;Pwd=2356591;");

        public SingletonSeance()
        {
            liste_Seance = new ObservableCollection<Seance>();
            loadDataInList();
        }

        public ObservableCollection<Seance> Liste_seances
        {
            get { return liste_Seance; }
        }
        public static SingletonSeance getInstance()
        {
            if (instance == null)
                instance = new SingletonSeance();

            return instance;
        }

        public void loadDataInList()
        {
            liste_Seance.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from seance";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                Seance seance = new Seance(Convert.ToInt32(r[0]), r[1].ToString(), r[2].ToString(), (DateTime)r[3], (int)r[4]);
                liste_Seance.Add(seance);
            }
            r.Close();
            con.Close();
        }

        public Seance getSeanceByID(int id)  
        
        {
            Seance seance = null;


                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"Select * from seance where idSeance = '{id}'";
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read()) { 
                    seance = new Seance(Convert.ToInt32(r[0]), r[1].ToString(), r[2].ToString(), (DateTime)r[3], (int)r[4]);
                }

                r.Close();
                con.Close();
                return seance;


        }

        public void modifierSeance(Seance seance)
        {
            int idSeance = seance.idSeance;
            string activiteNom = seance.activiteNom;
            string activiteType = seance.activiteType;
            DateTime dateHeure = seance.dateHeure;
            int nbPlacesDispos = seance.nbPlacesDispos;

           

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
    
                    commande.CommandText = "UPDATE seance SET activiteNom = @activiteNom, activiteType = @activiteType, dateHeure = @dateHeure, nbPlacesDispos = @nbPlacesDispos WHERE idSeance = @idSeance";
                
                commande.Parameters.AddWithValue("@idSeance", idSeance);

                commande.Parameters.AddWithValue("@activiteNom", activiteNom);
                commande.Parameters.AddWithValue("@activiteType", activiteType);
                commande.Parameters.AddWithValue("@dateHeure", dateHeure.ToString());
                commande.Parameters.AddWithValue("@nbPlacesDispos", nbPlacesDispos);
                

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();

                loadDataInList();
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine(ex.Message);
            }
        }

        public void ajouterSeance(Seance seance)
        {
            int idSeance = seance.idSeance;
            string activiteNom = seance.activiteNom;
            string activiteType = seance.activiteType;
            DateTime dateHeure = seance.dateHeure;
            int nbPlacesDispos = seance.nbPlacesDispos;



            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                commande.CommandText = "INSERT INTO seance (activiteNom, activiteType, dateHeure, nbPlacesDispos) VALUES (@activiteNom, @activiteType, @dateHeure, @nbPlacesDispos)\r\n";

                commande.Parameters.AddWithValue("@idSeance", idSeance);

                commande.Parameters.AddWithValue("@activiteNom", activiteNom);
                commande.Parameters.AddWithValue("@activiteType", activiteType);
                commande.Parameters.AddWithValue("@dateHeure", dateHeure.ToString());
                commande.Parameters.AddWithValue("@nbPlacesDispos", nbPlacesDispos);


                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();

                loadDataInList();
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine(ex.Message);
            }
        
        }

        public void supprimerSeance(int idSeance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "delete from seance where idSeance = @idSeance";
                commande.Parameters.AddWithValue("@idSeance", idSeance);
                con.Open();
                commande.Prepare();

                commande.ExecuteNonQuery();

                con.Close();
                loadDataInList();
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine(ex.Message);

            }
        }


    }
}
