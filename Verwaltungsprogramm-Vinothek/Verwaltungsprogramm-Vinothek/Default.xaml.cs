using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Default.xaml
    /// </summary>
    public partial class Default : UserControl
    {
        Window currentWindow;
        public Default()
        {
            InitializeComponent();
            currentWindow = Application.Current.Windows.OfType<Window>().Last(); //aktuelles Fenster
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.WindowState = WindowState.Minimized;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            currentWindow.DragMove();
        }


        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (currentWindow.WindowState == WindowState.Maximized)
                currentWindow.WindowState = WindowState.Normal;
            else
                currentWindow.WindowState = WindowState.Maximized;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if(Application.Current.Windows.Count == 1) //Wenn Hauptmenü, dann App schließen
            Application.Current.Shutdown();
            else
            {
                currentWindow.Close(); 
                Application.Current.Windows.OfType<Window>().Last().Show(); //sonst Fenster schließen
            }
        }
    }
}
