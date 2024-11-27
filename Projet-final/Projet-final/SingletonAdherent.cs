using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace Projet_final
{
    internal class SingletonAdherent
    {

        ObservableCollection<Adherent> liste_Adherent;
        static SingletonAdherent instance = null;
        bool isConnect = false;
        Adherent adherentConnect;

        public static Frame mainWindow;

        public ObservableCollection<Adherent> Liste_Adherent
        {
            get { return liste_Adherent; }
        }

        public void setMainwindow(Frame _main)
        {
            mainWindow = _main;
        }

        public Frame getMainwindow()
        {
            return mainWindow;
        }
        public bool IsConnect
        {
            get { return isConnect; }
            set { isConnect = value; }
        }

        public Adherent AdhrentConnect
        {
            get { return adherentConnect; }
            set { adherentConnect = value; }
        }



        MySqlConnection con = new MySqlConnection(
              "Server=cours.cegep3r.info;Database=a2024_420335ri_eq4;Uid=2356591;Pwd=2356591;");


        public SingletonAdherent()
        {
            liste_Adherent = new ObservableCollection<Adherent>();
            loadDataInList();

        }


        public static SingletonAdherent getInstance()
        {
            if (instance == null) 
                instance = new SingletonAdherent();

            return instance;
        }

        public void ajouterAdherent(Adherent adherent)
        {
            string prenom = adherent.Prenom;
            string nom = adherent.Nom;
            string adresse = adherent.Adresse;
            DateTime dateNaissance = adherent.DateNaissance;
            string email = adherent.Email;
            string pseudo = adherent.Pseudo;
            string mdp = adherent.Mdp;
            string role = adherent.Role;

            var inputBytes = Encoding.UTF8.GetBytes(mdp);
            var inputHash = SHA256.HashData(inputBytes);
            string mdpHashe = Convert.ToHexString(inputHash);
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "Insert into adherent (nom, prenom, adresse, dateNaissance, email, pseudo, mdp, role) VALUES (@nom, @prenom, @adresse, @dateNaissance, @email, @pseudo, @mdp, @role)";

                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@dateNaissance", dateNaissance.ToString());
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@pseudo", pseudo);
                commande.Parameters.AddWithValue("@mdp", mdpHashe);
                commande.Parameters.AddWithValue("@role", role);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();

                loadDataInList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        public void loadDataInList()
        {  
            liste_Adherent.Clear();
        
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from adherent";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                liste_Adherent.Add(new Adherent(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), (DateTime) r[4], (int) r[5], r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString()));
            }
            r.Close();
            con.Close();
        }






          //Connexion/UserOnline
        public void Connexion(string username, string mot_de_passe)
        {

            var inputBytes = Encoding.UTF8.GetBytes(mot_de_passe);
            var inputHash = SHA256.HashData(inputBytes);
            string mdp = Convert.ToHexString(inputHash);

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"Select * from adherent where pseudo ='{username}' and mdp = '{mdp}'";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if (r.HasRows){
            while(r.Read()){
                    adherentConnect = new Adherent(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), (DateTime)r[4], (int)r[5], r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString());
            }

                isConnect = true;
                r.Close();
                con.Close();
            }
            else{
                r.Close();
                con.Close();
                isConnect = false;

            }

        }

        public void Deconnexion()
        {
            isConnect = false;
            adherentConnect = null;
        }
    }
}
