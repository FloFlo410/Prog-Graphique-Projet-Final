using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Statistiques : Page
    {
        public Statistiques()
        {
            this.InitializeComponent();

            tbl_nbTotalAdherents.Text = " : ";
            tbl_nbTotalAdherents.Text += SingletonAdherent.getInstance().nbTotalAdherent();

            tbl_nbTotalActivites.Text = " : ";
            tbl_nbTotalActivites.Text += SingletonActivite.getInstance().nbTotalActivite();

            statActivites();
            statAdherent();

        }

        public void statActivites()
        {
            ObservableCollection<Statistique> liste_stat = new ObservableCollection<Statistique>();
            foreach (Activite activite in SingletonActivite.getInstance().getListeActivites())
            {
                Statistique statistique = new Statistique(activite.Nom, activite.Type, SingletonAdherent.getInstance().nbTotalAdherentSelonActivite(activite.Nom, activite.Type), SingletonActivite.getInstance().moyenneNoteParActivite(activite.Nom, activite.Type), SingletonActivite.getInstance().revenuTotalParActivite(activite.Nom, activite.Type));
                liste_stat.Add(statistique);
            }
            lv_activites.ItemsSource = liste_stat;
        }

        public void statAdherent()
        {
            ObservableCollection<Adherent> liste_adherents = SingletonAdherent.getInstance().Liste_Adherent;
            foreach (Adherent adherent in liste_adherents)
            {
                string item = adherent.NoIdentification + "\t" + adherent.Prenom + "\t" + adherent.Nom + "\tMoyenne prix par activité: ";
                item += SingletonAdherent.getInstance().prixMoyenActiviteAdherent(adherent.NoIdentification);
                item += "\t Prix total dépensé: ";
                item += SingletonAdherent.getInstance().prixTotalAdherent(adherent.NoIdentification);

                //lv_activites.Items.Add(item);
            }
        }

       
    }
}
