using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class Adherent
    {
        public string NoIdentification { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public DateTime DateNaissance { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Pseudo { get; set; }
        public string Mdp { get; set; }
        public string Role { get; set; }

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

        public Adherent(string nom, string prenom, string adresse, DateTime dateNaissance, string email, string pseudo, string mdp, string role)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            DateNaissance = dateNaissance;
            Email = email;
            Pseudo = pseudo;
            Mdp = mdp;
            Role = role;
        }

        public override string ToString()
        {
            return $"{Prenom} - {Nom} - {Age}";
        }
    }
}
