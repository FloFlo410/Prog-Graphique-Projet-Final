using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public sealed partial class AjouterAdherent : Page
    {
        public AjouterAdherent()
        {
            this.InitializeComponent();
        }

        private void btn_ajouter_Click(object sender, RoutedEventArgs e)
        {
            resetErr();
            if (valide())
            {
                string prenom = tbx_prenom.Text;
                string nom = tbx_nom.Text;
                string adresse = tbx_adresse.Text;
                DateTime dateNaissance = (DateTime) cldr_dateNaissance.Date.Value.DateTime;
                string email = tbx_email.Text;
                string pseudo = tbx_pseudo.Text;
                string mdp = tbx_mdp.Text;
                string role = tbx_role.Text;

                Adherent adherent = new Adherent(nom, prenom, adresse, dateNaissance, email, pseudo, mdp, role);
                SingletonAdherent.getInstance().ajouterAdherent(adherent);
            }
        }

        private bool valide()
        {
            bool valide = true;

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
            if (string.IsNullOrWhiteSpace(tbx_mdp.Text))
            {
                valide = false;
                tbl_mdp_err.Text = "Le mot de passe ne peut pas être vide.";
            }
            if (string.IsNullOrWhiteSpace(tbx_role.Text))
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
