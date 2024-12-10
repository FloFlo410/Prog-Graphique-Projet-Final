using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

            Participation participation;

            liste_Participation.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from participation";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                if (r[3].Equals(System.DBNull.Value))
                {
                    participation = new Participation((int)r[0], r[1].ToString(), (int)r[2], -1);
                }
                else
                {
                    Debug.WriteLine(r[3].GetType());
                    participation = new Participation((int)r[0], r[1].ToString(), (int)r[2], (double)r[3]);
                }

                liste_Participation.Add(participation);
            }
            r.Close();
            con.Close();
        }


        public ObservableCollection<Participation> getParticipationByAdherant(Adherent adherent)
        {

            ObservableCollection<Participation> liste_Participants = new ObservableCollection<Participation>();
            Participation participation;

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"Select * from participation where idAdherent = '{adherent.NoIdentification}'";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                if (r[3].Equals(System.DBNull.Value))
                {
                    participation = new Participation((int)r[0], r[1].ToString(), (int)r[2], -1);
                }
                else
                {
                    Debug.WriteLine(r[3].GetType());
                    participation = new Participation((int)r[0], r[1].ToString(), (int)r[2], (double)r[3]);
                }

                liste_Participants.Add(participation);
            }
            r.Close();
            con.Close();


            return liste_Participants;
        }

        public void supprimer(Participation participation)
        {
            try
            {


                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"DELETE FROM participation WHERE idParticipation=@idParticipation";
                commande.Parameters.AddWithValue("@idParticipation", (int)participation.IdParticipant);
                con.Open();
                commande.Prepare();

                commande.ExecuteNonQuery();

                con.Close();
                getParticipationByAdherant(SingletonAdherent.getInstance().AdhrentConnect);

            }
            catch (Exception ex)
            {
                con.Close();
                Debug.WriteLine(ex.Message);
            }
        }


        public int changerNote(Participation participation, double note)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"UPDATE participation SET note = @note WHERE idParticipation = @idParticipation";
                commande.Parameters.AddWithValue("@note", note);
                commande.Parameters.AddWithValue("@idParticipation", (int)participation.IdParticipant);

                con.Open();
                commande.Prepare();

                commande.ExecuteNonQuery();

                con.Close();

                return 0;

            }
            catch (Exception ex)
            {
                con.Close();
                Debug.WriteLine(ex.Message);
                return -1;
            }





        }
    }
}
