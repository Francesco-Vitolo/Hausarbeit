using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Uc_Menu.xaml
    /// </summary>
    public partial class Uc_Menu : UserControl
    {
        private string location;
        private Vinothek ctx = new Vinothek();
        private ICollectionView collectionView;

        public Uc_Menu()
        {
            InitializeComponent();
        }
        private void Button_MainMenu(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string grid = b.Name;
            adad.Visibility = Visibility.Hidden;
            //switch (grid)
            //{
            //    case "ListeProdukte":
            //        location = "ListeProdukte";
            //        ctx.Produkt.Load();
            //        collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local);
            //        MainGrid.DataContext = collectionView;
            //        break;

            //    case "ListeProduzenten":
            //        location = "ListeProduzenten";
            //        ctx.Produzent.Load();
            //        collectionView = CollectionViewSource.GetDefaultView(ctx.Produzent.Local);
            //        MainGrid.DataContext = collectionView;
            //        //data = CreateDataGrid.Produzent(data);
            //        break;

            //    case "ListeEvents":
            //        location = "ListeEvents";
            //        ctx.Event.Load();
            //        collectionView = CollectionViewSource.GetDefaultView(ctx.Event.Local);
            //        MainGrid.DataContext = collectionView;
            //        //data = CreateDataGrid.Event(data);
            //        break;
            //}
        }
    }
}
