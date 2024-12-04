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
    public sealed partial class GestionSeance : Page
    {
        public GestionSeance()
        {
            this.InitializeComponent();
            lv_seance.ItemsSource = SingletonSeance.getInstance().Liste_seances;
        }

        private void lv_seance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lv_seance.SelectedIndex;
            if (index != -1)
            {
                stp_detail.Visibility = Visibility.Visible;
                Seance seance = SingletonSeance.getInstance().Liste_seances[index];

                tblock_nom.Text = seance.activiteNom + " : " + seance.activiteType;
                num_place_dispo.Value = seance.nbPlacesDispos;
                date.SelectedDate = seance.DateHeure.Date;
                timePicker_heure.SelectedTime = seance.DateHeure.TimeOfDay;


            }
            else
            {
                stp_detail.Visibility = Visibility.Collapsed;
            }
        }

        private async void export_csv_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(SingletonAdherent.getInstance().getMAin_mainWindows());
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "Liste séances";
            picker.FileTypeChoices.Add("Csv", new List<string>() { ".csv" });

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            if (monFichier != null)
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, SingletonSeance.getInstance().Liste_seances.ToList<Seance>().ConvertAll(x=>x.ToString()), Windows.Storage.Streams.UnicodeEncoding.Utf8);

        }

        private void btn_ajoute_Click(object sender, RoutedEventArgs e)
        {
            SingletonAdherent.getInstance().getMainwindow().Navigate(typeof(AjoutSeance));
        }
    }
}
    
    

