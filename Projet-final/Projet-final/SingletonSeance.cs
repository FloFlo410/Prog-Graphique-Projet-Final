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


    }
}
