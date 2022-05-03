using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Verwaltungsprogramm_Vinothek.Properties;
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
            MainWindow currentWindow = Application.Current.Windows.OfType<MainWindow>().LastOrDefault();
            if (currentWindow.getUserID() != 1) //User - ID 1 --> admin
                UserVerwaltung.Visibility = Visibility.Hidden;
        }

        private void Button_Übersicht(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string grid = b.Name;
            Page_Grid_List newPage = new Page_Grid_List(grid);
            NavigationService.Navigate(newPage);
        }

        private void PdfDir_Click(object sender, RoutedEventArgs e) //PDF - Verzeichnis ändern
        {
            Settings.Default.PDF_Directory = SelectFile.PdfDir(Settings.Default.PDF_Directory);
            Settings.Default.Save();
        }

        private void Button_Kundensicht(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Kundensicht());
        }

        private void Button_User(object sender, RoutedEventArgs e) //Userverwaltung
        {
            NavigationService.Navigate(new Page_User());
        }
        private void Background_Click(object sender, RoutedEventArgs e) //Hintergrund ändern
        {
            try
            {
                Settings.Default.Background = (SolidColorBrush)new BrushConverter().ConvertFrom(tb_background.Text);
                Settings.Default.Save();
            }
            catch { }
           
        }

        private void Foreground_Click(object sender, RoutedEventArgs e) //Vordergrund ändern
        {
            try
            {
                Settings.Default.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom(tb_foreground.Text);
                Settings.Default.Save();
            }
            catch { }
        }


        private void Color_Click(object sender, RoutedEventArgs e) //Farbe für Buttons ändern (und andere Dinge)
        {
            try
            {
                Settings.Default.Color1 = (SolidColorBrush)new BrushConverter().ConvertFrom(tb_Color1.Text); //Farben 1 und 2 werden gespeichert
                Settings.Default.Color2 = (SolidColorBrush)new BrushConverter().ConvertFrom(tb_Color2.Text);
                Color brush1 = (Color)ColorConverter.ConvertFromString(tb_Color1.Text);
                Color brush2 = (Color)ColorConverter.ConvertFromString(tb_Color2.Text);
                LinearGradientBrush gradient = new LinearGradientBrush()
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 1),
                };
                gradient.GradientStops.Add(new GradientStop(brush1, 0));
                gradient.GradientStops.Add(new GradientStop(brush2, 0.8));
                Settings.Default.LinearGradientBrush = gradient;    //Farbverlauf wird erstellt und gespeichert
                Settings.Default.Save();
            }
            catch { }
        }

        private void Dokumentation_Click(object sender, RoutedEventArgs e) 
        {
            Process.Start("https://github.com/Francesco-Vitolo/Hausarbeit/blob/main/README.md");
        }

        private void ColorPickerLink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://g.co/kgs/M1YyXm");
        }
    }
}
