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

        private void btn_modif_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
