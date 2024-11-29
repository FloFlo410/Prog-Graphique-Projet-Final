using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class SingletonCategories
    {

        static SingletonCategories instance = null;
        ObservableCollection<Categorie> liste_categories;

        MySqlConnection con = new MySqlConnection(
             "Server=cours.cegep3r.info;Database=a2024_420335ri_eq4;Uid=2356591;Pwd=2356591;");

        public ObservableCollection<Categorie> ListeCategories
        {
            get { return liste_categories; }
        }

        public SingletonCategories()
        {
            liste_categories = new ObservableCollection<Categorie>();
            putDataInCollection();
        }

        public static SingletonCategories getInstance()
        {
            if (instance == null)
                instance = new SingletonCategories();

            return instance;
        }



        public void putDataInCollection()
        {
            liste_categories.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from categorie";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {

                liste_categories.Add(new Categorie(r[0].ToString()));
            }
            r.Close();
            con.Close();

        }

        public string ajouter(Categorie categorie)
        {
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into categorie values(@nom) ";
                commande.Parameters.AddWithValue("@nom", categorie.Nom);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
                putDataInCollection();
                return "réussi";


            }
            catch (Exception ex)
            {
                con.Close();
                if (ex.Message.Contains("Duplicate"))
                {
                    return "Cette activité existe déjà avec la même catégorie";
                }
            }

            return "erreur";
        }

    }
}
