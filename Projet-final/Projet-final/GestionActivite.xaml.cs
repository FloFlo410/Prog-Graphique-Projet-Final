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
                Activite activite = SingletonActivite.getInstance().getListeActivites()[index];
                
                for(int i =0; i<cbox_categories.Items.Count; i++)
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

                if (indexComboBox != -1)
                {
                    cbox_categories.SelectedItem = cbox_categories.Items[indexComboBox];
                }



            }
            else
            {
                stp_detail.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_modif_Click(object sender, RoutedEventArgs e)
        {

            int index = lv_activites.SelectedIndex;
            Activite newActivite = new Activite(tbox_nom.Text, cbox_categories.SelectedValue.ToString(), num_prix_org.Value, num_prix_client.Value);
            Activite oldActivite = SingletonActivite.getInstance().getListeActivites()[index];
            string result = SingletonActivite.getInstance().modifier(newActivite,oldActivite);

            if(result == "réussi")
                tblock_error.Text = string.Empty;
            else
                tblock_error.Text = result;
            
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
    }
}
