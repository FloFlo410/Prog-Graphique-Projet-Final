using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class Activite
    {
        public string Nom { get; set; }
        public string Type { get; set; }
        public double CoutOrganisation { get; set; }
        public double PrixVente { get; set; }
        public string Url_img { get; set; }

        public double MoyenneNote { get; set; }

        public Activite(string nom, string type, double coutOrganisation, double prixVente, string url_img)
        {
            Nom = nom;
            Type = type;
            CoutOrganisation = coutOrganisation;
            PrixVente = prixVente;
            Url_img = url_img;
        }

        public override string ToString()
        {
            return $"{Nom} - {Type}";
        }
    }
}
