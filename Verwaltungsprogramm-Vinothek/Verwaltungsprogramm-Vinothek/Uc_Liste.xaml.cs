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
    /// Interaktionslogik für Uc_Liste.xaml
    /// </summary>
    public partial class Uc_Liste : UserControl
    {
        private string location;
        //private Vinothek ctx = new Vinothek();
        private ICollectionView collectionView;
        public Uc_Liste()
        {
            InitializeComponent();
            location = "ListeProdukte";
            //ctx.Produkt.Load();
            //collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local);
            DataContext = collectionView;
        }       

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (location == "ListeProdukte")
            {
                Window_Add_Product WAP = new Window_Add_Product();
                WAP.Show();
            }
            else if (location == "ListeProduzenten")
            {
                //Add ...
            }
            else if (location == "ListeEvents")
            {
                //Add ...
            }

        }

        private void btn_Search_Click(object sender, RoutedEventArgs e) //filter
        {
            string filterStr = tbSearch.Text.ToLower();
            switch (location)
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
            ItemInfos.Show(collectionView.CurrentItem, location);

            //class ItemInfos
            //switch(location)
            //{
            //    case "ListeProdukte":
            //        Produkt selected_produkt = (Produkt)collectionView.CurrentItem;
            //        Window_Wein WW = new Window_Wein(selected_produkt);
            //        WW.ShowDialog();
            //        break;
            //    case "ListeProduzenten":
            //        Produzent selected_produzent = (Produzent)collectionView.CurrentItem;
            //        Window_Produzent WP = new Window_Produzent(selected_produzent);
            //        WP.ShowDialog();
            //        break;
            //    case "ListeEvents":

            //        break;                    
            //}
        }
    }
}
