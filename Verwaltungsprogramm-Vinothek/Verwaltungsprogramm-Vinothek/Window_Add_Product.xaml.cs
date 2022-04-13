using System.Windows;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Add_Product.xaml
    /// </summary>
    public partial class Window_Add_Product : Window
    {
        public Window_Add_Product()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
