using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Verwaltungsprogramm_Vinothek.Pages;

namespace Verwaltungsprogramm_Vinothek.Windows
{
    /// <summary>
    /// Interaktionslogik für Page_Grid_List.xaml
    /// </summary>
    public partial class Page_Grid_List : Page
    {
        VinothekContext ctx = new VinothekContext();
        private ICollectionView collectionView;
        private string gridType;
        Window_Abfrage WA;
        Window_Messagebox WM;
        public Page_Grid_List(string gridType)
        {
            InitializeComponent();
            this.gridType = gridType;
            CreateDG();
        }
        private void CreateDG()
        {
            switch (gridType)
            {
                case "ListeProdukte":
                    CreateDataGrid.Produkt(datagrid);
                    ctx.Produkt.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local);
                    DataContext = collectionView;
                    break;

                case "ListeProduzenten":
                    CreateDataGrid.Produzent(datagrid);
                    ctx.Produzent.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produzent.Local);
                    DataContext = collectionView;
                    break;

                case "ListeEvents":
                    CreateDataGrid.Event(datagrid);
                    ctx.Event.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Event.Local);
                    DataContext = collectionView;
                    break;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (gridType == "ListeProdukte")
            {
                Page_Add_Produkt newPage = new Page_Add_Produkt();
                NavigationService.Navigate(newPage);
            }
            else if (gridType == "ListeProduzenten")
            {
                Page_Add_Produzent newPage = new Page_Add_Produzent();
                NavigationService.Navigate(newPage);
            }
            else if (gridType == "ListeEvents")
            {
                Page_Add_Veranstaltung newPage = new Page_Add_Veranstaltung();
                NavigationService.Navigate(newPage);
            }
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e) //filter
        {
            string filterStr = tbSearch.Text.ToLower();
            switch (gridType)
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
            Page p = ItemInfos.Show(datagrid.CurrentItem, gridType);
            NavigationService.Navigate(p);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ctx = new VinothekContext();
            switch (gridType)
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
            DataContext = collectionView;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            switch (gridType)
            {
                case "ListeProdukte":
                    Produkt deleted_Produ = (Produkt)collectionView.CurrentItem;
                    WA = new Window_Abfrage($"Soll {deleted_Produ.Name} gelöscht werden");
                    WA.ShowDialog();
                    if (WA.GetOption())
                    {
                        ctx.Produkt.Remove(deleted_Produ);
                        WM = new Window_Messagebox(deleted_Produ.Name + " wurde gelöscht.");
                        WM.ShowDialog();
                    }
                    break;

                case "ListeProduzenten":
                    Produzent deleted_Prodz = (Produzent)collectionView.CurrentItem;
                    WA = new Window_Abfrage($"Soll {deleted_Prodz.Name} gelöscht werden");
                    WA.ShowDialog();
                    if (WA.GetOption())
                    {
                        if (ctx.Produkt.FirstOrDefault(x => x.ID_Produzent == deleted_Prodz.ID_Produzent) == null)
                        {
                            ctx.Produzent.Remove(deleted_Prodz);
                            WM = new Window_Messagebox(deleted_Prodz.Name + " wurde gelöscht.");
                            WM.ShowDialog();
                        }
                        else
                        {
                            WM = new Window_Messagebox("Es gibt noch ein verlinktes Produkt ");
                            WM.ShowDialog();
                        }
                    }
                    break;
                case "ListeEvents":
                    Event deleted_Event = (Event)collectionView.CurrentItem;
                    WA = new Window_Abfrage($"Soll {deleted_Event.Name} gelöscht werden");
                    WA.ShowDialog();
                    if (WA.GetOption())
                    {
                        if (Convert.ToDateTime(deleted_Event.Datum) < DateTime.Now)
                        {
                            WM = new Window_Messagebox("Das Datum liegt in der Vergangenheit");
                        }
                        else
                        {
                            ctx.Event.Remove(deleted_Event);
                            ctx.EventPos.Where(x => x.ID_Veranstaltung == deleted_Event.ID_Veranstaltung).ToList().ForEach(x => ctx.EventPos.Remove(x));
                            WM = new Window_Messagebox(deleted_Event.Name + " wurde gelöscht.");
                        }
                    }
                    break;
            }
            ctx.SaveChanges();
        }

        private void Duplicate_Click(object sender, RoutedEventArgs e)
        {
            //Add_Click(null,null);
            switch (gridType)
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

        private void Datagrid_KeyDel(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                Delete_Click(null, null);
        }

        private void Searchbar_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btn_Search_Click(null, null);

        }

        private void Button_Previous_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
