using System;
using System.Diagnostics;
using System.Linq;
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
            CheckTime();
        }

        private void CheckTime()
        {
            string begruessungsString = "";
            DateTime time = DateTime.Now;
            int timeString = int.Parse(time.TimeOfDay.Hours.ToString());
            if (timeString < 12)
                begruessungsString = "Guten Morgen";
            else if (timeString >= 18)
                begruessungsString = "Guten Abend";
            else
                begruessungsString = "Guten Mittag";

            var currentWindow = Application.Current.Windows.OfType<MainWindow>().LastOrDefault();
            VinothekContext ctx = ContextHelper.GetContext();
            Benutzer b = ctx.Benutzer.Find(currentWindow.GetUserID());
            begrueßung.Text = begruessungsString + "\n" + b;
        }
        private void Button_Übersicht(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string grid = b.Name;
            NavigationService.Navigate(new Page_Grid_List(grid));
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
