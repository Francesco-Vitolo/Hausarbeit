using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Verwaltungsprogramm_Vinothek.User_Controls
{
    /// <summary>
    /// Interaktionslogik für Uc_Default.xaml
    /// </summary>
    public partial class Uc_Default : UserControl
    {
        Window currentWindow;
        public Uc_Default()
        {
            InitializeComponent();
            currentWindow = Application.Current.Windows.OfType<Window>().LastOrDefault(); //aktuelles Fenster
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.WindowState = WindowState.Minimized;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            var res = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            if (currentWindow.WindowState == WindowState.Maximized)
            {
                currentWindow.WindowState = WindowState.Normal;
                //currentWindow.Left = res.X / 2;
                currentWindow.Top = res.Y;
            }
            currentWindow.WindowState = WindowState.Normal;
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
            Window_Abfrage WA = new Window_Abfrage("Fenster schließen");
            WA.ShowDialog();
            if (WA.GetOption())
                currentWindow.Close();
        }
    }
}
