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
    public sealed partial class GestionAdherent : Page
    {
        public GestionAdherent()
        {
            this.InitializeComponent();
            lv_Adherent.ItemsSource = SingletonAdherent.getInstance().Liste_Adherent;
            stck_infoAdherent.Visibility = Visibility.Collapsed;

        }

        private void btn_ajouter_adherent_Click(object sender, RoutedEventArgs e)
        {
            var mainwindow = SingletonAdherent.getInstance().getMainwindow();

            mainwindow.Navigate(typeof(AjouterAdherent));


        }

        private void btn_supprimer_adherent_Click(object sender, RoutedEventArgs e)
        {
            Adherent adherent = (Adherent) lv_Adherent.SelectedItem;
            if (adherent != null)
                SingletonAdherent.getInstance().supprimerAdherent(adherent.NoIdentification);
        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            if(lv_Adherent.SelectedItem != null)
            {
                string noIdentification = tbl_noIdentification.Text;
                string prenom = tbx_prenom.Text;
                string nom = tbx_nom.Text;
                string adresse = tbx_adresse.Text;
                DateTime dateNaissance = (DateTime)cldr_dateNaissance.Date.Value.DateTime;
                string email = tbx_email.Text;
                string pseudo = tbx_pseudo.Text;
                string mdp = tbx_mdp.Password;
                string role = tbx_role.Text;

                Adherent adherent = new Adherent(noIdentification, nom, prenom, adresse, dateNaissance, email, pseudo, mdp, role);

                SingletonAdherent.getInstance().modifierAdherent(adherent);
            }
            
        }

        private void lv_Adherent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Adherent adherent = (Adherent)lv_Adherent.SelectedItem;
            if (adherent != null)
            {
                stck_infoAdherent.Visibility = Visibility.Visible;
                tbl_noIdentification.Text = adherent.NoIdentification;
                tbx_prenom.Text = adherent.Prenom;
                tbx_nom.Text = adherent.Nom;
                cldr_dateNaissance.Date = adherent.DateNaissance;
                tbl_age.Text = adherent.Age.ToString() + "ans";
                tbx_adresse.Text = adherent.Adresse;
                tbx_email.Text = adherent.Email;
                tbx_pseudo.Text = adherent.Pseudo;
                tbx_mdp.Password = adherent.Mdp;
                tbx_role.Text = adherent.Role;

            }
            else
            {
                stck_infoAdherent.Visibility= Visibility.Collapsed;
            }
        }

        private void btn_exporter_adherent_Click(object sender, RoutedEventArgs e)
        {
            SingletonAdherent.getInstance().exporterAdherentCsv();
        }
    }
}
