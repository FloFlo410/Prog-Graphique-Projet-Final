using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    class Statistique
    {


        //activité
        string nomActivite;
        string typeActivite;
        int nombreParticipation;
        double noteMoyenne;
        double revenuTotal;

        //adhérent
        string nomAdherent;
        double prixMoyen;
        double depenseTotal;

        public Statistique(string nomActivite,string typeActivite ,int nombreParticipation, double noteMoyenne, double revenuTotal)
        {
            this.nomActivite = nomActivite;
            this.nombreParticipation = nombreParticipation;
            this.typeActivite = typeActivite;
            this.noteMoyenne = noteMoyenne;
            this.revenuTotal = revenuTotal;
        }

        public Statistique(string nomAdherent, double prixMoyen, double depenseTotal)
        {
            this.nomAdherent = nomAdherent;
            this.prixMoyen = prixMoyen;
            this.depenseTotal = depenseTotal;
        }

        public string NomActivite { 
            get { return nomActivite; }
        }

        public string TypeActivite
        {
            get { return typeActivite; }
        }

        public double NoteMoyenne
        {
            get { return noteMoyenne; }
        }

        public string NomAdherent
        {
            get { return nomAdherent; }
        }
        public int NombreParticipation
        {
            get { return nombreParticipation; }
        }

        public double RevenuTotal
        {
            get { return revenuTotal; }
        }
        public double PrixMoyen
        {
            get { return prixMoyen; }
        }

        public double DepenseTotal
        {
            get { return depenseTotal; }
        }

        public override string ToString()
        {
            return $"{NomActivite}";
        }


    }
}
