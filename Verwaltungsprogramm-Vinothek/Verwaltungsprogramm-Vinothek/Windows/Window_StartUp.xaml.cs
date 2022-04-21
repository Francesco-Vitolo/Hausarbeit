using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        }

        private void Button_MainMenu(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string grid = b.Name;
            Grid_Menu.Visibility = Visibility.Hidden;
            Grid_Liste.Visibility = Visibility.Visible;
            hidebackbutton.Visibility = Visibility.Hidden;
            switch (grid)
            {
                case "ListeProdukte":
                    location = "ListeProdukte";
                    data = CreateDataGrid.Produkt(data);
                    ctx.Produkt.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local);
                    MainGrid.DataContext = collectionView;
                    break;

                case "ListeProduzenten":
                    location = "ListeProduzenten";
                    data = CreateDataGrid.Produzent(data);
                    ctx.Produzent.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produzent.Local);
                    MainGrid.DataContext = collectionView;
                    break;

                case "ListeEvents":
                    location = "ListeEvents";
                    data = CreateDataGrid.Event(data);
                    ctx.Event.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Event.Local);
                    MainGrid.DataContext = collectionView;
                    break;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (location == "ListeProdukte")
            {
                Window_Add_Product WAP = new Window_Add_Product();
                Hide();
                WAP.Show();
            }
            else if (location == "ListeProduzenten")
            {
                Window_Add_Produzent WAP = new Window_Add_Produzent();
                Hide();
                WAP.Show();
            }
            else if (location == "ListeEvents")
            {
                Window_Add_Veranstaltung WAV = new Window_Add_Veranstaltung();
                Hide();
                WAV.Show();
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
            Refresh_Click(null, null);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ctx = new Vinothek();
            switch (location)
            {
                case "ListeProdukte":
                    ctx.Produkt.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local);
                    break;
                case "ListeProduzenten":
                    ctx.Produzent.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produzent.Local);
                    break;
                case "ListeEvents":
                    ctx.Event.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Event.Local);
                    break;
            }
            MainGrid.DataContext = collectionView;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            switch (location)
            {
                case "ListeProdukte":
                    Produkt deleted_Produ = ctx.Produkt.Remove((Produkt)collectionView.CurrentItem);
                    MessageBox.Show(deleted_Produ.Name + " wurde gelöscht.");
                    break;
                case "ListeProduzenten":
                    Produzent deleted_Prodz = (Produzent)collectionView.CurrentItem;
                    if(ctx.Produkt.FirstOrDefault(x => x.ID_Produzent == deleted_Prodz.ID_Produzent) == null)
                    {
                        deleted_Prodz = ctx.Produzent.Remove((Produzent)collectionView.CurrentItem);
                        MessageBox.Show(deleted_Prodz.Name + " wurde gelöscht.");
                    }
                    else
                        MessageBox.Show("Es gibt noch ein verlinktes Produkt ");
                    break;
                case "ListeEvents":
                    Event deleted_Event = (Event)collectionView.CurrentItem;
                    if(Convert.ToDateTime(deleted_Event.Datum) < DateTime.Now)
                    {
                        MessageBox.Show("Datum liegt in der Vergangenheit");
                    }
                    else
                    {
                        ctx.Event.Remove(deleted_Event);
                        ctx.EventPos.Where(x => x.ID_Veranstaltung == deleted_Event.ID_Veranstaltung).ToList().ForEach(x => ctx.EventPos.Remove(x));
                        MessageBox.Show(deleted_Event.Name + " wurde gelöscht.");
                    }                  
                    break;
            }
            ctx.SaveChanges();
        }

        private void Duplicate_Click(object sender, RoutedEventArgs e)
        {
            //Add_Click(null,null);
            switch (location)
            {
                case "ListeProdukte":
                    ctx.Produkt.Add((Produkt)collectionView.CurrentItem);
                    break;
                case "ListeProduzenten":
                    ctx.Produzent.Add((Produzent)collectionView.CurrentItem);
                    break;
                case "ListeEvents":
                    Event evnt = (Event)collectionView.CurrentItem;
                    var list = ctx.EventPos.Where(x => x.ID_Veranstaltung == evnt.ID_Veranstaltung).ToList();
                    ctx.Event.Add(evnt);
                    ctx.SaveChanges();
                    foreach (var v in list)
                    {
                        EventPos EP = new EventPos();
                        EP.ID_Veranstaltung = evnt.ID_Veranstaltung;
                        EP.ID_Produkt = v.ID_Produkt;
                        ctx.EventPos.Add(EP);
                    }
                    break;
            }
            ctx.SaveChanges();
            Refresh_Click(null, null);
        }

        private void datagrid_keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                Delete_Click(null, null);
        }

        private void Searchbar_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btn_Search_Click(null,null);

        }
    }
}