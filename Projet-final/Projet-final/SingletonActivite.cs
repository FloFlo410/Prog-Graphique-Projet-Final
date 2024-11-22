﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class SingletonActivite
    {
        static SingletonActivite instance = null;
        ObservableCollection<Activite> liste_activites;

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
        }

    }
}
