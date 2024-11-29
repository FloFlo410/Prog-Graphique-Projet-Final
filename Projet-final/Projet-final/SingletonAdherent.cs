using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
        public static MainWindow mainWindow_Window;

        public ObservableCollection<Adherent> Liste_Adherent
        {
            get { return liste_Adherent; }
        }

        public void setMainwindow(Frame _main)
        {
            mainWindow = _main;
        }

        public void setMainwindowWindow(MainWindow _main)
        {
            mainWindow_Window = _main;
        }


        public Frame getMainwindow()
        {
            return mainWindow;
        }

        public MainWindow getMAin_mainWindows()
        {
            return mainWindow_Window;
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
                con.Close();
                Console.WriteLine(ex.Message);
            }
            
        }

        public bool checkIfPasswordHasChanged(string noIdentification, string mdp)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from adherent WHERE noIdentification='"+ noIdentification+"' AND mdp='"+mdp+"';";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

        public void modifierAdherent(Adherent adherent)
        {
            string no_identification = adherent.NoIdentification;

            string prenom = adherent.Prenom;
            string nom = adherent.Nom;
            string adresse = adherent.Adresse;
            DateTime dateNaissance = adherent.DateNaissance;
            string email = adherent.Email;
            string pseudo = adherent.Pseudo;
            string mdp = adherent.Mdp;
            string role = adherent.Role;

            bool passwordHasChanged = checkIfPasswordHasChanged(no_identification, mdp);
            if(passwordHasChanged)
            {
                var inputBytes = Encoding.UTF8.GetBytes(mdp);
                var inputHash = SHA256.HashData(inputBytes);
                mdp = Convert.ToHexString(inputHash);
            }
            
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                if (passwordHasChanged)
                {
                    commande.CommandText = "UPDATE adherent SET nom = @nom, prenom = @prenom, dateNaissance = @dateNaissance, adresse = @adresse, email = @email, pseudo = @pseudo, mdp = @mdp, role = @role WHERE noIdentification = @noIdentification";
                    commande.Parameters.AddWithValue("@mdp", mdp);

                }
                else
                {
                    commande.CommandText = "UPDATE adherent SET nom = @nom, prenom = @prenom, dateNaissance = @dateNaissance, adresse = @adresse, email = @email, pseudo = @pseudo, role = @role WHERE noIdentification = @noIdentification";
                }
                commande.Parameters.AddWithValue("@noIdentification", no_identification);

                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@dateNaissance", dateNaissance.ToString());
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@pseudo", pseudo);
                commande.Parameters.AddWithValue("@role", role);

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


        public void supprimerAdherent(string noIdentification)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "delete from adherent where noIdentification=@noIdentification";
                commande.Parameters.AddWithValue("@noIdentification", noIdentification);
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

        public async void exporterAdherentCsv()
        {
            try
            {
                var picker = new Windows.Storage.Pickers.FileSavePicker();

                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(mainWindow_Window);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

                picker.SuggestedFileName = "adherents";
                picker.FileTypeChoices.Add("Csv", new List<string>() { ".csv" });

                //crée le fichier
                Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

                if(monFichier != null)
                    await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste_Adherent.ToList<Adherent>().ConvertAll(x => x.ToString()), Windows.Storage.Streams.UnicodeEncoding.Utf8);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

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
