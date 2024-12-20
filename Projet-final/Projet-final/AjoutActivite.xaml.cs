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
    public sealed partial class AjoutActivite : Page
    {
        public AjoutActivite()
        {
            this.InitializeComponent();
            cbox_categories.ItemsSource = SingletonCategories.getInstance().ListeCategories;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SingletonAdherent.getInstance().getMainwindow().Navigate(typeof(GestionActivite));

        }


        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {

            if (switch_choix.IsOn)
            {
                tbox_categories.Visibility = Visibility.Visible;
                cbox_categories.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbox_categories.Visibility = Visibility.Collapsed;
                cbox_categories.Visibility = Visibility.Visible;
            }
        }

        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                string categorie;
                if (switch_choix.IsOn)
                {
                    categorie = tbox_categories.Text;
                    tblock_error.Text = SingletonCategories.getInstance().ajouter(new Categorie(categorie));
                }
                else
                {
                    categorie = cbox_categories.SelectedValue.ToString();
                }

                string result = SingletonActivite.getInstance().ajouter(new Activite(tbox_nom.Text, categorie, Convert.ToInt32(num_prix_org.Text), Convert.ToInt32(num_prix_client.Text),tbox_url.Text));
                if (result == "r�ussi")
                {
                    SingletonAdherent.getInstance().getMainwindow().Navigate(typeof(GestionActivite));
                }
                else
                {
                    tblock_error.Text = result;
                }

            }



        }



        private bool Validation()
        {

            restart();
            bool valide = true;

            if (string.IsNullOrWhiteSpace(tbox_nom.Text))
            {
                valide = false;
                tbox_nom_error.Text = "Le nom ne peut pas �tre vide";

            }

            if (string.IsNullOrWhiteSpace(num_prix_org.Text) || num_prix_org.Value<0)
            {
                valide = false;
                num_prix_org_error.Text = "Le nombre ne peut pas �tre plus petit que 0";
            }
            else if (num_prix_org.Value >= num_prix_client.Value)
            {

                valide = false;
                num_prix_org_error.Visibility = Visibility.Visible;
                num_prix_client_error.Visibility = Visibility.Visible;
                num_prix_org_error.Text = "Le prix d'organisation doit �tre plus petit que le prix pour le client.";
                num_prix_client_error.Text = "Le prix d'organisation doit �tre plus petit que le prix pour le client.";

            }

            if (string.IsNullOrWhiteSpace(num_prix_client.Text) || num_prix_client.Value<0)
            {
                valide = false;
                num_prix_client_error.Text = "Le nombre ne peut pas �tre plus petit que 0";

            }

            if (string.IsNullOrWhiteSpace(tbox_url.Text))
            {
                valide = false;
                tbox_url_error.Text = "L'url ne peut pas �tre vide";
            }


            if (!switch_choix.IsOn)
            {
                if (cbox_categories.SelectedItem == null)
                {
                    valide = false;
                    cbox_categories_error.Text = "La cat�gorie ne peut pas �tre vide";

                }

            }
            else
            {
                if (string.IsNullOrWhiteSpace(tbox_categories.Text))
                {
                    valide = false;
                    cbox_categories_error.Text = "La cat�gorie ne peut pas �tre vide";
                }

            }

            return valide;

        }

        private void restart()
        {
            num_prix_client_error.Text = String.Empty;
            num_prix_org_error.Text = String.Empty;
            tbox_nom_error.Text = String.Empty;
            cbox_categories_error.Text = String.Empty;

        }
    }
}
