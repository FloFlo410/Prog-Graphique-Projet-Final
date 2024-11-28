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

        public Activite(string nom, string type, double coutOrganisation, double prixVente)
        {
            Nom = nom;
            Type = type;
            CoutOrganisation = coutOrganisation;
            PrixVente = prixVente;
        }

        public override string ToString()
        {
            return $"{Nom} - {Type}";
        }
    }
}
