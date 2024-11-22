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
            if (!SingletonAdherent.getInstance().Connexion(tbox_nomUtilisateur.Text, tbox_motDePasse.Text))
            {
                tbox_nomUtilisateur_error.Text = "Le Pseudo ou le mot de passe n'est pas correct";
            }
        }
    }
}
