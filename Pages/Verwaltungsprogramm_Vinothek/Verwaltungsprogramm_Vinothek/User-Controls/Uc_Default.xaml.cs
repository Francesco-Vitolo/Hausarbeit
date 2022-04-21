using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Verwaltungsprogramm_Vinothek.User_Controls
{
    /// <summary>
    /// Interaktionslogik für Uc_Default.xaml
    /// </summary>
    public partial class Uc_Default : UserControl
    {
        Window currentWindow;
        NavigationService n;
        public Uc_Default()
        {
            InitializeComponent();
            currentWindow = Application.Current.Windows.OfType<Window>().LastOrDefault(); //aktuelles Fenster
            n = NavigationService.GetNavigationService(this);
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
            currentWindow.Close();
        }

        private void btn_prev(object sender, RoutedEventArgs e)
        {
            n.GoBack();
            //Application.Current.Shutdown();
            //System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }
    }
}
