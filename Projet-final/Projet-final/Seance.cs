using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class Seance
    {

       public int idSeance;
       public string activiteNom;
        public string activiteType;
        public int nbPlacesDispos;

        public Seance(int idSeance, string activiteNom, string activiteType, int nbPlacesDispos)
        {
            this.idSeance = idSeance;
            this.activiteNom = activiteNom;
            this.activiteType = activiteType;
            this.nbPlacesDispos = nbPlacesDispos;
        }

        public int IdSeance
        {
            get { return idSeance; }
            set { idSeance = value; }
        }

        public string ActiviteNom
        {
            get { return activiteNom; }
            set { activiteNom = value; }
        }

        public string ActiteType
        {
            get { return activiteType; }
            set { activiteType = value; }
        }

        public int NbPlacesDispos
        {
            get { return nbPlacesDispos; }
            set { nbPlacesDispos = value; }
        }
    }
}
