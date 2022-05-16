using System.Windows;

namespace Verwaltungsprogramm_Vinothek.Windows
{
    /// <summary>
    /// Interaktionslogik für Window_SplashScreen.xaml
    /// </summary>
    public partial class Window_SplashScreen : Window
    {
        public Window_SplashScreen()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
        }
        public double Progress
        {
            get { return progressBar.Value; }
            set { progressBar.Value = value; }
        }
    }
}
