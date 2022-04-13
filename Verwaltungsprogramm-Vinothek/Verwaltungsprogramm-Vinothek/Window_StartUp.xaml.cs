using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_StartUp.xaml
    /// </summary>
    public partial class Window_StartUp : Window
    {
        private BrushConverter bc = new BrushConverter();
        private string location;
        Vinothek ctx = new Vinothek();

        public Window_StartUp()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_MainMenu(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string grid = b.Name;
            Grid_Menu.Visibility = Visibility.Hidden;
            switch (grid)
            {
                case "ListeProdukte":
                    Grid_ListeProdukte.Visibility = Visibility.Visible;
                    location = "ListeProdukte";
                    ctx.Produkt.Load();
                    MainGrid.DataContext = ctx.Produkt.Local;
                    //data.Children.Add(CreateDataGrid.Produkt());
                    data = CreateDataGrid.Produkt(data);
                    break;

                case "ListeProduzenten":
                    Grid_ListeProduzenten.Visibility = Visibility.Visible;
                    location = "ListeProduzenten";
                    //data.Children.Add(CreateDataGrid.Produzenten());
                    break;

                case "ListeEvents":
                    Grid_ListeEvents.Visibility = Visibility.Visible;
                    location = "ListeEvents";
                    //data.Children.Add(CreateDataGrid.Events());
                    break;
            }
        }

        private void Button_Prev_Click(object sender, RoutedEventArgs e)
        {
            switch (location)
            {
                case "ListeProdukte":
                    Grid_ListeProdukte.Visibility = Visibility.Hidden;
                    break;

                case "ListeProduzenten":
                    Grid_ListeProduzenten.Visibility = Visibility.Hidden;
                    break;

                case "ListeEvents":
                    Grid_ListeEvents.Visibility = Visibility.Hidden;
                    break;
            }
            Grid_Menu.Visibility = Visibility.Visible;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Window_Add_Product WAP = new Window_Add_Product();
            Hide();
            WAP.Show();
        }

    }
}