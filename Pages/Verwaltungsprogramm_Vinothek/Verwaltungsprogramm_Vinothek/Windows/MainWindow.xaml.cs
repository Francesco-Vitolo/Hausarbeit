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
using Verwaltungsprogramm_Vinothek.Pages;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            Frame_Main.Content = new Page_MainMenu();
        }

        private void Button_Click_Expander(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string name = b.Name;
            switch (name)
            {
                case "_1":
                    Frame_Main.Content = new Page_Grid_List("ListeProdukte");
                    break;
                case "_2":
                    Frame_Main.Content = new Page_Grid_List("ListeProduzenten");
                    break;
                case "_3":
                    Frame_Main.Content = new Page_Grid_List("ListeEvents");
                    break;
                case "_4":
                    Frame_Main.Content = new Page_Add_Produkt();
                    break;
                case "_5":
                    Frame_Main.Content = new Page_Add_Produzent();
                    break;
                case "_6":
                    Frame_Main.Content = new Page_Add_Veranstaltung();
                    break;
            }
            expander.IsExpanded = false;
        }
    }
}
