using Microsoft.UI;
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
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Reservation : Page
    {
        
        public Reservation()
        {
            this.InitializeComponent();
            afficherActivite();
        }

        public void afficherActivite()
        {
            Activite activite = SingletonActivite.getInstance().getActiviteSelectione();
            tblock_nom.Text = activite.Nom;
            //tbl_nom.Text = activite.Nom;
            tbl_prix_client.Text = activite.PrixVente.ToString() + "$/session";

            afficherSeances();
        }

        public void afficherSeances()
        {
            Activite activite = SingletonActivite.getInstance().getActiviteSelectione();
            ObservableCollection<Seance> seances = SingletonActivite.getInstance().getSeancesPourActivite(activite.Nom, activite.Type);
            gv_seance.ItemsSource = seances;
        }

        private void btn_reserver_seance_Click(object sender, RoutedEventArgs e)
        {
            if (SingletonAdherent.getInstance().IsConnect)
            {
                Adherent adherent = SingletonAdherent.getInstance().AdhrentConnect;
                if (gv_seance.SelectedItem != null)
                {
                    Seance seance = gv_seance.SelectedItem as Seance;
                    int statut = SingletonActivite.getInstance().reserverSeanceActivite(adherent.NoIdentification, seance.IdSeance);
                    if(statut == 0) 
                        tbl_err_reservation.Text = "Vous avez réserver cette séance.";
                    else if (statut == -2)
                        tbl_err_reservation.Text = "Vous êtes déjà inscrit(e).";
                    else if (statut == -3)
                        tbl_err_reservation.Text = "Il n'y a plus de place disponibles.";
                    else
                        tbl_err_reservation.Text = "Une erreur s'est produite.";

                    afficherSeances();
                }
                else
                {

                    tbl_err_reservation.Text = "Veuillez choisir une séance.";
                }
            }
            else
            {
                tbl_err_reservation.Text = "Veuillez vous connecter avant de réserver une séance.";
            }

        }
    }
 }
