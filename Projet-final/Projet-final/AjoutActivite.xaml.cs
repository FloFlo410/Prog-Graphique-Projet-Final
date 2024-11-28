using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI.Common;
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
                cbox_categories.Visibility=Visibility.Collapsed;
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

                string result = SingletonActivite.getInstance().ajouter(new Activite(tbox_nom.Text, categorie, Convert.ToInt32(num_prix_org.Text), Convert.ToInt32(num_prix_client.Text)));
                if(result == "réussi")
                {
                    SingletonAdherent.getInstance().getMainwindow().Navigate(typeof(GestionActivite));
                }
                else
                {
                    tblock_error.Text = result;
                }

            }



        }



        private bool Validation() {

            restart();
            bool valide = true;


            if (string.IsNullOrWhiteSpace(tbox_nom.Text))
            {
                valide = false;
                tbox_nom_error.Text = "Le nom ne peut pas être vide";

            }
            return valide;
        
        
        }



        private void restart()
        {
            num_prix_client_error.Text =String.Empty;
            num_prix_org_error.Text = String.Empty;
            tbox_nom_error.Text = String.Empty;
            cbox_categories_error.Text = String.Empty;

        }
    }
}
