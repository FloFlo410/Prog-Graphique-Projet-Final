using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    class SingletonParticipation
    {

        static SingletonParticipation instance = null;
        ObservableCollection<Participation> liste_Participation;

        MySqlConnection con = new MySqlConnection(
             "Server=cours.cegep3r.info;Database=a2024_420335ri_eq4;Uid=2356591;Pwd=2356591;");

        public SingletonParticipation()
        {
            liste_Participation = new ObservableCollection<Participation>();
            loadDataInList();
        }

        public static SingletonParticipation getInstance()
        {
            if (instance == null)
                instance = new SingletonParticipation();

            return instance;
        }

        public ObservableCollection<Participation> Liste_Participation
        {
            get { return liste_Participation; }
        }


        public void loadDataInList()
        {
            liste_Participation.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from participation";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                if (r[3] == null)
                {

                }
                else
                {

                }
                Participation participation = new Participation((int)r[0], r[1].ToString(), (int)r[2], (double)r[3]);
                liste_Participation.Add(participation);
            }
            r.Close();
            con.Close();
        }
    }
}
