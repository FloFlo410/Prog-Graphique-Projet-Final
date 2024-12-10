using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
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
    public sealed partial class AjoutSeance : Page
    {
        public AjoutSeance()
        {
            this.InitializeComponent();
            cbox_activite.ItemsSource = SingletonActivite.getInstance().getListeActivites();

        }

        private bool valide()
        {
            resetError();
            bool valide = true;
            if (cbox_activite.SelectedIndex < 0)
            {
                valide = false;
                tbl_err_cbActivite.Visibility = Visibility.Visible;
                tbl_err_cbActivite.Text = "L\'activité est obligatoire";
            }
            if (num_place_dispo.Value < 0 || string.IsNullOrWhiteSpace(num_place_dispo.Text))
            {
                valide = false;
                tbl_err_placeDispo.Visibility = Visibility.Visible;
                tbl_err_placeDispo.Text = "Le nombre de place disponible doit être un nombre positif.";
            }
            if (date.SelectedDate == null)
            {
                valide = false;
                tbl_err_date.Visibility = Visibility.Visible;
                tbl_err_date.Text = "La date doit être une date valide.";
            }
            if (timePicker_heure.SelectedTime == null)
            {
                valide = false;
                tbl_err_time.Visibility = Visibility.Visible;
                tbl_err_time.Text = "L'heure doit être une heure valide.";
            }
            return valide;
        }

        private void resetError()
        {
            tbl_err_cbActivite.Visibility = Visibility.Collapsed;
            tbl_err_date.Visibility = Visibility.Collapsed;
            tbl_err_placeDispo.Visibility = Visibility.Collapsed;
            tbl_err_time.Visibility = Visibility.Collapsed;
        }

        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            if (valide())
            {
                string activiteNom = cbox_activite.SelectedItem.ToString().Split('-')[0].Trim();
                string activiteType = cbox_activite.SelectedItem.ToString().Split('-')[1].Trim();
                DateTime dateHeure = new DateTime(date.Date.Year, date.Date.Month, date.Date.Day, timePicker_heure.Time.Hours, timePicker_heure.Time.Minutes, timePicker_heure.Time.Seconds);
                int nbPlacesDispo = (int)num_place_dispo.Value;

                Seance seance = new Seance(0, activiteNom, activiteType, dateHeure, nbPlacesDispo);
                SingletonSeance.getInstance().ajouterSeance(seance);

                var mainwindow = SingletonAdherent.getInstance().getMainwindow();
                mainwindow.Navigate(typeof(GestionSeance));
            }
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            SingletonAdherent.getInstance().getMainwindow().Navigate(typeof(GestionSeance));

        }
    }
}
