using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Verwaltungsprogramm_Vinothek.User_Controls
{
    /// <summary>
    /// Interaktionslogik für Uc_Titlebar.xaml
    /// </summary>
    public partial class Uc_Titlebar : UserControl
    {
        private Window CurrentWindow { get; }
        public Uc_Titlebar()
        {
            InitializeComponent();
            CurrentWindow = Application.Current.Windows.OfType<Window>().LastOrDefault(); //aktuelles Fenster
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.WindowState = WindowState.Minimized;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            var res = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            if (CurrentWindow.WindowState == WindowState.Maximized)
            {
                CurrentWindow.WindowState = WindowState.Normal;
                //currentWindow.Left = res.X / 2;
                CurrentWindow.Top = res.Y;
            }
            CurrentWindow.WindowState = WindowState.Normal;
            CurrentWindow.DragMove();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentWindow.WindowState == WindowState.Maximized)
                CurrentWindow.WindowState = WindowState.Normal;
            else
                CurrentWindow.WindowState = WindowState.Maximized;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window_Abfrage WA = new Window_Abfrage("Fenster schließen");
            WA.ShowDialog();
            if (WA.GetOption())
                CurrentWindow.Close();
        }
    }
}
