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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Projet_final
{
    public sealed partial class Connexion : ContentDialog
    {
        public Connexion()
        {
            this.InitializeComponent();
            tbox_nomUtilisateur_error.Text = string.Empty;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(tbox_nomUtilisateur.Text) && string.IsNullOrWhiteSpace(tbox_motDePasse.Password))
            {
                tbox_nomUtilisateur_error.Text = "Veuillez saisir un pseudo et un mot de passe.";

            }
            else if (string.IsNullOrWhiteSpace(tbox_nomUtilisateur.Text))
            {
                tbox_nomUtilisateur_error.Text = "Veuillez saisir un pseudo.";

            }
            else if (string.IsNullOrWhiteSpace(tbox_motDePasse.Password))
            {
                tbox_nomUtilisateur_error.Text = "Veuillez saisir un mot de passe.";
            }
            else
            {
                SingletonAdherent.getInstance().Connexion(tbox_nomUtilisateur.Text, tbox_motDePasse.Password);
                if (!SingletonAdherent.getInstance().IsConnect)
                {
                    tbox_nomUtilisateur_error.Text = "Le pseudo ou le mot de passe n'est pas correct";
                }

            }
            
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (!SingletonAdherent.getInstance().IsConnect && args.Result == ContentDialogResult.Primary)
            {
                args.Cancel = true;
            }
        }
    }
}
