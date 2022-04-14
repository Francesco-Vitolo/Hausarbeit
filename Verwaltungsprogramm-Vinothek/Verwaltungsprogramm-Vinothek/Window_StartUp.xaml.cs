using System.Collections;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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
        private Vinothek ctx = new Vinothek();
        private ICollectionView collectionView;

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
            Grid_Liste.Visibility = Visibility.Visible;
            switch (grid)
            {
                case "ListeProdukte":                   
                    location = "ListeProdukte";
                    ctx.Produkt.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local);
                    MainGrid.DataContext = collectionView;
                    data = CreateDataGrid.Produkt(data);
                    break;

                case "ListeProduzenten":
                    location = "ListeProduzenten";
                    ctx.Produzent.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produzent.Local);
                    MainGrid.DataContext = collectionView;
                    data = CreateDataGrid.Produzent(data);
                    break;

                case "ListeEvents":
                    location = "ListeEvents";
                    ctx.Event.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Event.Local);
                    MainGrid.DataContext = collectionView;
                    data = CreateDataGrid.Event(data);
                    break;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(location == "ListeProdukte")
            {
                Window_Add_Product WAP = new Window_Add_Product();
                Hide();
                WAP.Show();
            }
            else if(location == "ListeProduzenten")
            {
                //Add ...
            }
            else if(location == "ListeEvents")
            {
                //Add ...
            }

        }

        private void btn_Search_Click(object sender, RoutedEventArgs e) //filter
        {
            string filterStr = tbSearch.Text.ToLower();
            switch(location)
            {
                case "ListeProdukte":
                    collectionView.Filter = x => ((Produkt)x).Name.ToLower().Contains(filterStr);
                    break;
                case "ListeProduzenten":
                    collectionView.Filter = x => ((Produzent)x).Name.ToLower().Contains(filterStr);
                    break;
                case "ListeEvents":
                    collectionView.Filter = x => ((Event)x).Name.ToLower().Contains(filterStr);
                    break;
            }
            //search
            //string searchStr = tbSearch.Text;
            //collectionView.Filter = null;
            //// Typumwandlung der Liste mit der Cast() Methode
            //var list = collectionView.Cast<Produkt>();
            //Produkt p = list.FirstOrDefault(x => x.Name.Contains(searchStr));
            //collectionView.MoveCurrentTo(p);
        }

        private void SelectItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(data.SelectedItem.ToString());
            // new Window Wein bearbeiten/anschauen
        }
    }
}