﻿using Microsoft.UI.Xaml;
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
            stp_no_selection.Visibility = Visibility.Visible;

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
            if(lv_Adherent.SelectedItem != null && valide())
            {
                string noIdentification = tbl_noIdentification.Text;
                string prenom = tbx_prenom.Text;
                string nom = tbx_nom.Text;
                string adresse = tbx_adresse.Text;
                DateTime dateNaissance = (DateTime)cldr_dateNaissance.Date.Value.DateTime;
                string email = tbx_email.Text;
                string pseudo = tbx_pseudo.Text;
                string mdp = tbx_mdp.Password;
                string role = tbx_role.SelectedValue.ToString().ToLower();

                Adherent adherent = new Adherent(noIdentification, nom, prenom, adresse, dateNaissance, email, pseudo, mdp, role);

                SingletonAdherent.getInstance().modifierAdherent(adherent);
            }
            
        }

        private void lv_Adherent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Adherent adherent = (Adherent)lv_Adherent.SelectedItem;
            if (adherent != null)
            {
                resetErr();
                stck_infoAdherent.Visibility = Visibility.Visible;
                stp_no_selection.Visibility = Visibility.Collapsed;
                tbl_noIdentification.Text = adherent.NoIdentification;
                tbx_prenom.Text = adherent.Prenom;
                tbx_nom.Text = adherent.Nom;
                cldr_dateNaissance.Date = adherent.DateNaissance;
                tbl_age.Text = adherent.Age.ToString() + "ans";
                tbx_adresse.Text = adherent.Adresse;
                tbx_email.Text = adherent.Email;
                tbx_pseudo.Text = adherent.Pseudo;
                tbx_mdp.Password = adherent.Mdp;


                if (adherent.Role == "administrateur")
                {
                    tbx_role.SelectedIndex = 1;
                }else
                    tbx_role.SelectedIndex = 0;


            }
            else
            {
                stck_infoAdherent.Visibility= Visibility.Collapsed;
                stp_no_selection.Visibility = Visibility.Visible;

            }
        }

        private void btn_exporter_adherent_Click(object sender, RoutedEventArgs e)
        {
            SingletonAdherent.getInstance().exporterAdherentCsv();
        }


        private bool valide()
        {
            bool valide = true;
            resetErr();

            if (string.IsNullOrWhiteSpace(tbx_prenom.Text))
            {
                valide = false;
                tbl_prenom_err.Text = "Le prénom ne peut pas être vide.";
            }
            if (string.IsNullOrWhiteSpace(tbx_nom.Text))
            {
                valide = false;
                tbl_nom_err.Text = "Le nom ne peut pas être vide.";
            }
            if (string.IsNullOrWhiteSpace(tbx_adresse.Text))
            {
                valide = false;
                tbl_adresse_err.Text = "L'adresse ne peut pas être vide.";
            }
            if (cldr_dateNaissance.Date == null)
            {
                valide = false;
                tbl_dateNaissance_err.Text = "La date de naissance doit être une date valide.";
            }
            else if (DateTime.Now.Year - cldr_dateNaissance.Date.Value.Year < 18)
            {
                valide = false;
                tbl_dateNaissance_err.Text = "Vous devez avoir 18 ans.";
            }

            if (string.IsNullOrWhiteSpace(tbx_email.Text))
            {
                valide = false;
                tbl_email_err.Text = "L'email ne peut pas être vide.";
            }

            if (string.IsNullOrWhiteSpace(tbx_pseudo.Text))
            {
                valide = false;
                tbl_pseudo_err.Text = "Le pseudo ne peut pas être vide.";
            }
            if (string.IsNullOrWhiteSpace(tbx_mdp.Password))
            {
                valide = false;
                tbl_mdp_err.Text = "Le mot de passe ne peut pas être vide.";
            }
            if (string.IsNullOrWhiteSpace(tbx_role.SelectedValue.ToString()))
            {
                valide = false;
                tbl_role_err.Text = "Le rôle ne peut pas être vide.";
            }
            return valide;
        }

        private void resetErr()
        {
            tbl_prenom_err.Text = "";
            tbl_nom_err.Text = "";
            tbl_adresse_err.Text = "";
            tbl_dateNaissance_err.Text = "";
            tbl_email_err.Text = "";
            tbl_pseudo_err.Text = "";
            tbl_mdp_err.Text = "";
            tbl_role_err.Text = "";
        }
    }
}
