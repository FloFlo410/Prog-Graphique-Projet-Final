using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Reflection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Participation_adherant_connecter : Page
    {
        public Participation_adherant_connecter()
        {
            this.InitializeComponent();
            lv_participation.ItemsSource = SingletonParticipation.getInstance().getParticipationByAdherant(SingletonAdherent.getInstance().AdhrentConnect);

            if (SingletonParticipation.getInstance().getParticipationByAdherant(SingletonAdherent.getInstance().AdhrentConnect).Count == 0)
            {
                tblock_no_participation.Visibility = Visibility.Visible;
            }
            else
            {
                tblock_no_participation.Visibility = Visibility.Collapsed;
            }
            

            stp_participation.Visibility = Visibility.Collapsed;
        }

        private void lv_participation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int index = lv_participation.SelectedIndex;
            stp_participation.Visibility = Visibility.Collapsed;

            if (index != -1)
            {
               
                stp_participation.Visibility = Visibility.Visible;
                Participation participation = SingletonParticipation.getInstance().getParticipationByAdherant(SingletonAdherent.getInstance().AdhrentConnect)[index];

                tblock_nom_activite.Text = participation.Seance.ActiviteNom;

                tblock_type_activite.Text = participation.Seance.ActiteType;

                tblock_date_activite.Text = participation.Seance.DateHeure.Date.ToString("D");

                tblock_heure_activite.Text = participation.Seance.DateHeure.ToString("HH:mm:ss");

            }
        }

        private void btn_desinscrire_Click(object sender, RoutedEventArgs e)
        {
            int index = lv_participation.SelectedIndex;
            Participation participation = SingletonParticipation.getInstance().getParticipationByAdherant(SingletonAdherent.getInstance().AdhrentConnect)[index];

            SingletonParticipation.getInstance().supprimer(participation);
            lv_participation.ItemsSource = SingletonParticipation.getInstance().getParticipationByAdherant(SingletonAdherent.getInstance().AdhrentConnect);


        }

        private void mbtn_modifier_note_Click(object sender, RoutedEventArgs e)
        {
            int index = lv_participation.SelectedIndex;
            if (index != -1)
            {
                double value = Math.Round(slider_note.Value,1);

                SingletonParticipation.getInstance().changerNote(SingletonParticipation.getInstance().getParticipationByAdherant(SingletonAdherent.getInstance().AdhrentConnect)[index],value);


            }
        }
    }
}
