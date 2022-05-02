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
            if (currentWindow.getUserID() != 1)
                UserVerwaltung.Visibility = Visibility.Hidden;
        }

        private void Button_MainMenu(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string grid = b.Name;
            Page_Grid_List newPage = new Page_Grid_List(grid);
            NavigationService.Navigate(newPage);
        }

        private void PdfDir_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.PDF_Directory = SelectFile.PdfDir(Properties.Settings.Default.PDF_Directory);
            Settings.Default.Save();
        }

        private void Button_Kundensicht(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Kundensicht());
        }

        private void Button_User(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_User());
        }
        private void Background_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings.Default.Background = (SolidColorBrush)new BrushConverter().ConvertFrom(tb_background.Text);
                Settings.Default.Save();
            }
            catch { }
           
        }

        private void Foreground_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings.Default.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom(tb_foreground.Text);
                Settings.Default.Save();
            }
            catch { }
        }


        private void Color1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Color brush1 = (Color)ColorConverter.ConvertFromString(tb_Color1.Text);
                Color brush2 = (Color)ColorConverter.ConvertFromString(tb_Color2.Text);
                LinearGradientBrush gradient = new LinearGradientBrush()
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 1),
                };
                gradient.GradientStops.Add(new GradientStop(brush1, 0));
                gradient.GradientStops.Add(new GradientStop(brush2, 0.8));
                Settings.Default.LinearGradientBrush = gradient;
                Settings.Default.Save();
            }
            catch { }
        }
    }
}
