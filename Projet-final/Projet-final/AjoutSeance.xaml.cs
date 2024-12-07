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
    public sealed partial class AjoutSeance : Page
    {
        public AjoutSeance()
        {
            this.InitializeComponent();
            cbox_activite.ItemsSource = SingletonActivite.getInstance().getListeActivites();

        }

        private bool valide()
        {
            bool valide = true;
            //if(string.IsNullOrWhiteSpace(tb))
            return valide;
        }

        private void btn_ajout_Click(object sender, RoutedEventArgs e)
        {
            if (valide())
            {
                string activiteNom = cbox_activite.SelectedItem.ToString().Split('-')[0].Trim();
                string activiteType = cbox_activite.SelectedItem.ToString().Split('-')[1].Trim();
                DateTime dateHeure = new DateTime(date.Date.Year, date.Date.Month, date.Date.Day, timePicker_heure.Time.Hours, timePicker_heure.Time.Minutes, timePicker_heure.Time.Seconds);
                int nbPlacesDispo = (int)num_place_dispo.Value;

                Seance seance = new Seance(0, activiteNom, activiteType, dateHeure, nbPlacesDispo);
                SingletonSeance.getInstance().ajouterSeance(seance);

                var mainwindow = SingletonAdherent.getInstance().getMainwindow();
                mainwindow.Navigate(typeof(GestionSeance));
            }
        }
    }
}
