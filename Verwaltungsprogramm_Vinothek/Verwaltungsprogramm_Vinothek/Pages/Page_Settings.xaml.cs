using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Verwaltungsprogramm_Vinothek.Properties;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Settings.xaml
    /// </summary>
    public partial class Page_Settings : Page
    {
        public Page_Settings()
        {
            InitializeComponent();
            MainWindow currentWindow = Application.Current.Windows.OfType<MainWindow>().LastOrDefault();
            if (currentWindow.GetUserID() != 1) //User - ID 1 --> admin
            {
                UserVerwaltung.Visibility = Visibility.Hidden;
                UserVerwaltungBtn.Visibility = Visibility.Hidden;
                Logins.Visibility = Visibility.Hidden;
            }
        }

        private void PdfDir_Click(object sender, RoutedEventArgs e) //PDF - Verzeichnis ändern
        {
            Settings.Default.PDF_Directory = Files.SelectPdfDir();
            Settings.Default.Save();
        }
     
        private void Button_User(object sender, RoutedEventArgs e) //Userverwaltung
        {
            HiddenUserverwaltung.NavigationService.Navigate(new Page_User());
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
        private void ColorPickerLink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://g.co/kgs/M1YyXm");
        }

        private void Button_Logins(object sender, RoutedEventArgs e)
        {
            HiddenUserverwaltung.NavigationService.Navigate(new Page_Logins());
        }

        private void ChangeConnectionString_Click(object sender, RoutedEventArgs e)
        {
            ContextHelper.ChangeDatabase();
        }
    }
}
