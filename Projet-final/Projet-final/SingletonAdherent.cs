using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class SingletonAdherent
    {

        ObservableCollection<Adherent> liste_Adherent;
        static SingletonAdherent instance = null;
        bool isConnect = false;
        Adherent AdherentConnect;

        public



        MySqlConnection con = new MySqlConnection(
              "Server=cours.cegep3r.info;Database=a2024_420335ri_eq4;Uid=2356591;Pwd=2356591;");


        public SingletonAdherent()
        {
            liste_Adherent = new ObservableCollection<Adherent>();

        }


        public static SingletonAdherent getInstance()
        {
            if (instance == null)
                instance = new SingletonAdherent();

            return instance;
        }




        public void get_table()
        {
            
            liste_Adherent.Clear(); 


        }
    }
}
