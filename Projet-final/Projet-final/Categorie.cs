using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class Categorie
    {
        public Categorie(string nom)
        {
            Nom = nom;
        }

        public string Nom { get; set; }

        public override string ToString()
        {
            return Nom;
        }
    }
}
