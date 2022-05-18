using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_MainMenu.xaml
    /// </summary>
    public partial class Page_MainMenu : Page
    {
        public Page_MainMenu()
        {
            InitializeComponent();
        }

        private void Button_Übersicht(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string grid = b.Name;
            Page_Grid_List newPage = new Page_Grid_List(grid);
            NavigationService.Navigate(newPage);
        }
        private void Button_Kundensicht(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Kundensicht());
        }
        private void Dokumentation_Click(object sender, RoutedEventArgs e) 
        {
            Process.Start("https://github.com/Francesco-Vitolo/Hausarbeit/blob/main/README.md");
        }       
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Settings());
        }
    }
}
