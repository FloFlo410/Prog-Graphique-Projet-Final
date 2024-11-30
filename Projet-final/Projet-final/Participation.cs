using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_final
{
    class Participation
    {
        int idParticipant;
        Adherent adhrent;
        Seance seance;
        double note;

        public Participation(int idParticipant, string idAdhrent,int idSeance, double note)
        {
            this.idParticipant = idParticipant;
            adhrent = SingletonAdherent.getInstance().getAdherantByID(idAdhrent);
           seance = SingletonSeance.getInstance().getSeanceByID(idSeance);
            this.note = note;
        }

        public Adherent Adhrent
        {
            get { return adhrent; }
            set { adhrent = value; }
        }

        public int IdParticipant
        {
            get { return idParticipant; }
            set { idParticipant = value; }
        }

        public Seance Seance
        {
            get { return seance; }
            set { seance = value; }
        }

        public double Note
        {
            get { return note; }
            set { note = value; }
        }

        public override string ToString()
        {
            return $"{Seance.ActiviteNom} - {Seance.DateHeure}";
        }


    }
}
