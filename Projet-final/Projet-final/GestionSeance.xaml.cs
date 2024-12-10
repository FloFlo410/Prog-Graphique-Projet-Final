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
    public sealed partial class GestionSeance : Page
    {
        public GestionSeance()
        {
            this.InitializeComponent();
            lv_seance.ItemsSource = SingletonSeance.getInstance().Liste_seances;
            cbox_activite.ItemsSource = SingletonActivite.getInstance().getListeActivites();
        }

        private void lv_seance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lv_seance.SelectedIndex;
            if (index != -1)
            {
                stp_detail.Visibility = Visibility.Visible;
                Seance seance = SingletonSeance.getInstance().Liste_seances[index];

                tblock_idSeance.Text = seance.idSeance.ToString();
                tblock_nom.Text = seance.activiteNom + " : " + seance.activiteType;
                num_place_dispo.Value = seance.nbPlacesDispos;
                date.SelectedDate = seance.DateHeure.Date;
                timePicker_heure.SelectedTime = seance.DateHeure.TimeOfDay;
                for(int i = 0; i < cbox_activite.Items.Count; i++)
                {
                    if(cbox_activite.Items[i].ToString().Contains(seance.activiteNom )  && cbox_activite.Items[i].ToString().Contains(seance.activiteType))
                    {
                        cbox_activite.SelectedIndex = i;
                    }
                }

            }
            else
            {
                stp_detail.Visibility = Visibility.Collapsed;
            }
        }

        private async void export_csv_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(SingletonAdherent.getInstance().getMAin_mainWindows());
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "Liste s�ances";
            picker.FileTypeChoices.Add("Csv", new List<string>() { ".csv" });

            //cr�e le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            if (monFichier != null)
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, SingletonSeance.getInstance().Liste_seances.ToList<Seance>().ConvertAll(x=>x.ToString()), Windows.Storage.Streams.UnicodeEncoding.Utf8);

        }

        private void btn_ajoute_Click(object sender, RoutedEventArgs e)
        {
            SingletonAdherent.getInstance().getMainwindow().Navigate(typeof(AjoutSeance));
        }

        private void btn_modif_Click(object sender, RoutedEventArgs e)
        {
            if (valide())
            {
                int idSeance = Int32.Parse(tblock_idSeance.Text);
                string activiteNom = cbox_activite.SelectedItem.ToString().Split('-')[0].Trim();
                string activiteType = cbox_activite.SelectedItem.ToString().Split('-')[1].Trim();
                DateTime dateHeure = new DateTime(date.Date.Year, date.Date.Month, date.Date.Day, timePicker_heure.Time.Hours, timePicker_heure.Time.Minutes, timePicker_heure.Time.Seconds);
                int nbPlacesDispo = (int) num_place_dispo.Value;

                SingletonSeance.getInstance().modifierSeance(new Seance(idSeance, activiteNom, activiteType, dateHeure, nbPlacesDispo));
            }
        }

        private void btn_sup_Click(object sender, RoutedEventArgs e)
        {
            if(lv_seance.SelectedIndex >= 0)
            {
                Seance seance = lv_seance.SelectedItem as Seance;
                SingletonSeance.getInstance().supprimerSeance(seance.idSeance);
            }
        }

        private bool valide()
        {
            resetError();
            bool valide = true;
            if(cbox_activite.SelectedIndex < 0)
            {
                valide = false;
                tbl_err_cbActivite.Visibility = Visibility.Visible;
                tbl_err_cbActivite.Text = "L\'activit� est obligatoire";
            }
            if(num_place_dispo.Value < 0 || string.IsNullOrWhiteSpace(num_place_dispo.Text))
            {
                valide = false;
                tbl_err_placeDispo.Visibility = Visibility.Visible;
                tbl_err_placeDispo.Text = "Le nombre de place disponible doit �tre un nombre positif.";
            }
            if(date.SelectedDate == null)
            {
                valide= false;
                tbl_err_date.Visibility = Visibility.Visible;
                tbl_err_date.Text = "La date doit �tre une date valide.";
            }
            if(timePicker_heure.SelectedTime == null)
            {
                valide= false;
                tbl_err_time.Visibility = Visibility.Visible;
                tbl_err_time.Text = "L'heure doit �tre une heure valide.";
            }
            return valide;
        }

        private void resetError()
        {
            tbl_err_cbActivite.Visibility=Visibility.Collapsed;
            tbl_err_date.Visibility=Visibility.Collapsed;
            tbl_err_placeDispo.Visibility=Visibility.Collapsed;
            tbl_err_time.Visibility=Visibility.Collapsed;
        }
    }
}
    
    

