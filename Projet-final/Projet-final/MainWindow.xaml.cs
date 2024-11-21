using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {


        public MainWindow()
        {
            this.InitializeComponent();
        }
        private async void nvSample_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item =args.InvokedItem as string;


            switch (item)
            {
                case "Connexion":
                    Connexion dialog = new Connexion();
                    dialog.XamlRoot = mainWindow.XamlRoot;
                    dialog.Title = "Connexion";
                    dialog.PrimaryButtonText = "Se connecter";
                    dialog.CloseButtonText = "Annuler";
                    dialog.DefaultButton = ContentDialogButton.Close;

                    ContentDialogResult resultat = await dialog.ShowAsync();

                    if (resultat == ContentDialogResult.Primary)
                    {
                        connexion.Visibility = Visibility.Collapsed;
                        deconnexion.Visibility = Visibility.Visible;
                    }


                    break;

                case "Déconnexion":

                    connexion.Visibility = Visibility.Visible;
                    deconnexion.Visibility = Visibility.Collapsed;

                    break;
            }
        }
    }
}
