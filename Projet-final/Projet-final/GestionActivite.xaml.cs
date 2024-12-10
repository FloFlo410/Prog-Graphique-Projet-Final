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
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_final
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GestionActivite : Page
    {
        public GestionActivite()
        {
            this.InitializeComponent();
            lv_activites.ItemsSource = SingletonActivite.getInstance().getListeActivites();
            cbox_categories.ItemsSource = SingletonCategories.getInstance().ListeCategories;
        }

        private void lv_activites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lv_activites.SelectedIndex;
            int indexComboBox = -1;
            if (index != -1)
            {
                stp_detail.Visibility = Visibility.Visible;
                stp_no_selection.Visibility = Visibility.Collapsed;
                Activite activite = SingletonActivite.getInstance().getListeActivites()[index];

                for (int i = 0; i < cbox_categories.Items.Count; i++)
                {
                    if (cbox_categories.Items[i].ToString() == activite.Type)
                    {
                        indexComboBox = i;
                    }

                }

                tblock_nom.Text = activite.Nom;
                tbox_nom.Text = activite.Nom;
                num_prix_org.Value = activite.CoutOrganisation;
                num_prix_client.Value = activite.PrixVente;
                tbox_url_img.Text = activite.Url_img;

                if (indexComboBox != -1)
                {
                    cbox_categories.SelectedItem = cbox_categories.Items[indexComboBox];
                }
            }
            else
            {
                stp_detail.Visibility = Visibility.Collapsed;
                stp_no_selection.Visibility = Visibility.Visible;
            }
        }

        private void btn_modif_Click(object sender, RoutedEventArgs e)
        {
            if (valide())
            {
                int index = lv_activites.SelectedIndex;
                Activite newActivite = new Activite(tbox_nom.Text, cbox_categories.SelectedValue.ToString(), num_prix_org.Value, num_prix_client.Value, tbox_url_img.Text);
                Activite oldActivite = SingletonActivite.getInstance().getListeActivites()[index];
                string result = SingletonActivite.getInstance().modifier(newActivite, oldActivite);

                if (result == "réussi")
                    tblock_error.Text = string.Empty;
                else
                    tblock_error.Text = result;
            }
            

        }

        private void btn_sup_Click(object sender, RoutedEventArgs e)
        {
            int index = lv_activites.SelectedIndex;
            Activite activite = SingletonActivite.getInstance().getListeActivites()[index];

            SingletonActivite.getInstance().supprimer(activite);



        }

        private void btn_ajoute_Click(object sender, RoutedEventArgs e)
        {
            SingletonAdherent.getInstance().getMainwindow().Navigate(typeof(AjoutActivite));
        }

        private async void export_csv_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(SingletonAdherent.getInstance().getMAin_mainWindows());
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "Liste Activitées";
            picker.FileTypeChoices.Add("Csv", new List<string>() { ".csv" });

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            if (monFichier != null)
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, SingletonActivite.getInstance().getListeActivites().ToList<Activite>().ConvertAll(x => x.ToString()), Windows.Storage.Streams.UnicodeEncoding.Utf8);

        }

        private bool valide()
        {
            resetErr();
            bool valide = true;
            if (string.IsNullOrWhiteSpace(tbox_nom.Text))
            {
                valide = false;
                tbl_err_nom.Visibility = Visibility.Visible;
                tbl_err_nom.Text = "Le nom ne peut pas être vide.";
            }
            if(cbox_categories.SelectedIndex < 0)
            {
                valide = false;
                tbl_err_cb_categories.Visibility = Visibility.Visible;
                tbl_err_cb_categories.Text = "La catégorie ne peut pas être vide.";
            }
            if(num_prix_org.Value < 0 || string.IsNullOrWhiteSpace(num_prix_org.Text))
            {
                valide = false;
                tbl_err_num_prix_org.Visibility = Visibility.Visible;
                tbl_err_num_prix_org.Text = "Le prix d'organisation doit être un nombre positif.";
            }
            if (num_prix_client.Value < 0 || string.IsNullOrWhiteSpace(num_prix_client.Text))
            {
                valide = false;
                tbl_err_num_prix_client.Visibility = Visibility.Visible;
                tbl_err_num_prix_client.Text = "Le prix pour le client doit être un nombre positif.";
            }
            if(string.IsNullOrWhiteSpace(tbox_url_img.Text))
            {
                valide = false;
                tbl_err_url.Visibility = Visibility.Visible;
                tbl_err_url.Text = "L'url pour l'image ne peut pas être vide.";
            }

            return valide;
        }

        private void resetErr()
        {
            tbl_err_nom.Visibility = Visibility.Collapsed;
            tbl_err_cb_categories.Visibility = Visibility.Collapsed;
            tbl_err_num_prix_org.Visibility = Visibility.Collapsed;
            tbl_err_num_prix_client.Visibility = Visibility.Collapsed;
            tbl_err_url.Visibility = Visibility.Collapsed;
        }
    }
}
