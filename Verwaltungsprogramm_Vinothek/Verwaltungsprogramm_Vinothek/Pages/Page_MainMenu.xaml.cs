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
        }

        private void Button_MainMenu(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string grid = b.Name;
            Page_Grid_List newPage = new Page_Grid_List(grid);
            NavigationService.Navigate(newPage);
        }
    }
}
