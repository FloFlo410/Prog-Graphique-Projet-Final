using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    internal class Seance
    {

        int idSeance;
        string activiteNom;
        string typeActivite;

        public Seance(int idSeance, string activiteNom, string typeActivite)
        {
            this.idSeance = idSeance;
            this.activiteNom = activiteNom;
            this.typeActivite = typeActivite;
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
    
        public string TypeActivite
        {
            get { return typeActivite; }
            set { typeActivite = value; }
        }
    
    }
}
