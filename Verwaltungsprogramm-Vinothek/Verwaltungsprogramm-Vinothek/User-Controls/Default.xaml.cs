using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Default.xaml
    /// </summary>
    public partial class Default : UserControl
    {
        Window currentWindow;
        private Window MainWindow = Application.Current.MainWindow;
        public Default()
        {
            InitializeComponent();
            currentWindow = Application.Current.Windows.OfType<Window>().LastOrDefault(); //aktuelles Fenster
            if (Application.Current.Windows.Count > 1)
            {
                MainWindow.Hide();
                currentWindow.Focus();
                if (MainWindow.WindowState == WindowState.Maximized)
                    currentWindow.WindowState = WindowState.Maximized;
            }
            //else
            //    MainWindow.Show();
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
            if (Application.Current.Windows.Count == 1) //Wenn Hauptmenü, dann App schließen
            {
                Window_Abfrage w = new Window_Abfrage("Anwendung schließen?");
                w.ShowDialog();
                if (w.GetOption())
                    Application.Current.Shutdown();
            }
            else
            {
                Window_Abfrage w = new Window_Abfrage("Fenster schließen?");
                w.ShowDialog();
                if (w.GetOption())
                    Application.Current.Windows.OfType<Window>().LastOrDefault().Close();
                Application.Current.Windows.OfType<Window>().LastOrDefault().Show();
            }
        }

        private void btn_prev(object sender, RoutedEventArgs e)
        {
            currentWindow = Application.Current.Windows.OfType<Window>().LastOrDefault();
            if (Application.Current.Windows.Count > 1)
            {
                bool max = false;
                if (currentWindow.WindowState == WindowState.Maximized)
                {
                    max = true;
                }
                currentWindow.Close();
                MainWindow.Show();
                if (max)
                currentWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            }
        }
    }
}
