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
            mainWindow.Navigate(typeof(ListeActivitees));

            SingletonAdherent.getInstance().setMainwindow(this.mainWindow);
            SingletonAdherent.getInstance().setMainwindowWindow(this);

            setNomAccueil();
        }
        private async void nvSample_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item =args.InvokedItem as string;


            switch (item)
            {
                case "Statistiques":
                    mainWindow.Navigate(typeof(Statistiques));
                    break;

                case "Mes participations":

                    if (SingletonAdherent.getInstance().IsConnect)
                    {
                    mainWindow.Navigate(typeof(Participation_adherant_connecter));
                    nv.Header = null;
                    }
                    else
                    {
                        Connexion dialog1 = new Connexion();
                        dialog1.XamlRoot = mainWindow.XamlRoot;
                        dialog1.Title = "Connexion";
                        dialog1.PrimaryButtonText = "Se connecter";
                        dialog1.CloseButtonText = "Annuler";
                        dialog1.DefaultButton = ContentDialogButton.Close;

                        ContentDialogResult resultat1 = await dialog1.ShowAsync();

                        if (resultat1 == ContentDialogResult.Primary)
                        {
                            if (SingletonAdherent.getInstance().IsConnect)
                            {
                                connexion.Visibility = Visibility.Collapsed;
                                deconnexion.Visibility = Visibility.Visible;
                                tblock_acceuil_name.Visibility = Visibility.Visible;
                                if (SingletonAdherent.getInstance().AdhrentConnect.Role == "administrateur")
                                {
                                    menu_admin.Visibility = Visibility.Visible;
                                }
                            }
                        }
                    }

                    break;

                case "Gestion utilisateur":
                    mainWindow.Navigate(typeof(GestionAdherent));
                    nv.Header = null;
               break;

                case "Gestion Activité":
                    mainWindow.Navigate(typeof(GestionActivite));
                    nv.Header = null;
                    break;


                case "Gestion Séance":
                    mainWindow.Navigate(typeof(GestionSeance));
                    nv.Header = null;
                    break;


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
                            if (SingletonAdherent.getInstance().IsConnect){ 
                                connexion.Visibility = Visibility.Collapsed;
                                deconnexion.Visibility = Visibility.Visible;
                                tblock_acceuil_name.Visibility = Visibility.Visible;
                                if (SingletonAdherent.getInstance().AdhrentConnect.Role == "administrateur")
                                {
                                    menu_admin.Visibility = Visibility.Visible;
                                }
                            }
                    }
                    break;

                case "Déconnexion":


                    SingletonAdherent.getInstance().Deconnexion();

                    connexion.Visibility = Visibility.Visible;
                    deconnexion.Visibility = Visibility.Collapsed;
                    tblock_acceuil_name.Visibility = Visibility.Collapsed;
                    menu_admin.Visibility = Visibility.Collapsed;
                    break;

                case "Liste Activités":
                    mainWindow.Navigate(typeof(ListeActivitees));
                    break;
            }
        }

        public void setNomAccueil()
        {
            if (SingletonAdherent.getInstance().IsConnect)
                tblock_acceuil_name.Text = "Bonjour, " + SingletonAdherent.getInstance().AdhrentConnect.Prenom;
            else
                tblock_acceuil_name.Text = "Bonjour";
        }
    }
}
