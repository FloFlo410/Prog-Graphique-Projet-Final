using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class Adherent
    {
        string NoIdentification { get; set; }
        string Nom { get; set; }
        string Prenom { get; set; }
        string Adresse { get; set; }
        DateTime DateNaissance { get; set; }
        int Age { get; set; }
        string Email { get; set; }
        string Pseudo { get; set; }
        string Mdp { get; set; }
        string Role { get; set; }

        public Adherent(string noIdentification, string nom, string prenom, string adresse, DateTime dateNaissance, int age, string email, string pseudo, string mdp, string role)
        {
            NoIdentification = noIdentification;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            DateNaissance = dateNaissance;
            Age = age;
            Email = email;
            Pseudo = pseudo;
            Mdp = mdp;
            Role = role;
        }
    }
}
