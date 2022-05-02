using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private SortDescription currentSortDesc;
        public Page_Grid_List(string gridType)
        {
            InitializeComponent();
            this.gridType = gridType;
            CreateDG();
            //test();
        }

        //private async void test()
        //{
        //    while (true)
        //    {
        //        await Timer(5000);
        //    }
        //}
        private Task Timer(int i)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(i);
                Dispatcher.Invoke(() => Refresh_Click(null, null));
            });
        }
        private void CreateDG()
        {
            switch (gridType)
            {
                case "ListeProdukte":
                    CreateDataGrid.Produkt(datagrid);
                    ctx.Produkt.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local.OrderBy(x => x.Name));
                    datagrid.DataContext = collectionView;
                    cb_filter = CreateDataGrid.ProduktFilter(cb_filter);
                    cb_filter.Items.Add("Aktiv");
                    break;

                case "ListeProduzenten":
                    CreateDataGrid.Produzent(datagrid);
                    ctx.Produzent.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produzent.Local.OrderBy(x => x.Name));
                    datagrid.DataContext = collectionView;
                    cb_filter = CreateDataGrid.ProduzentFilter(cb_filter);
                    break;

                case "ListeEvents":
                    CreateDataGrid.Event(datagrid);
                    ctx.Event.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Event.Local.OrderBy(x => x.Datum));
                    datagrid.DataContext = collectionView;
                    cb_filter = CreateDataGrid.EventFilter(cb_filter);
                    break;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (gridType == "ListeProdukte")
            {
                NavigationService.Navigate(new Page_Add_Produkt());
            }
            else if (gridType == "ListeProduzenten")
            {
                NavigationService.Navigate(new Page_Add_Produzent());
            }
            else if (gridType == "ListeEvents")
            {
                NavigationService.Navigate(new Page_Add_Veranstaltung());
            }
            collectionView.Refresh();
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
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            btn_Search_Click(null, null);
        }

        private void SelectItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagrid.CurrentItem != null)
            {
                switch (gridType)
                {
                    case "ListeProdukte":
                        Produkt selected_produkt = (Produkt)datagrid.CurrentItem;
                        NavigationService.Navigate(new Page_Produkt(selected_produkt, currentSortDesc));
                        break;
                    case "ListeProduzenten":
                        Produzent selected_produzent = (Produzent)datagrid.CurrentItem;
                        NavigationService.Navigate(new Page_Produzent(selected_produzent));
                        break;
                    case "ListeEvents":
                        Event selected_event = (Event)datagrid.CurrentItem;
                        NavigationService.Navigate(new Page_Veranstaltung(selected_event));
                        break;
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ctx = new VinothekContext();
            switch (gridType)
            {
                case "ListeProdukte":
                    ctx.Produkt.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local.OrderBy(x => x.Name));
                    break;
                case "ListeProduzenten":
                    ctx.Produzent.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produzent.Local.OrderBy(x => x.Name));
                    break;
                case "ListeEvents":
                    ctx.Event.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Event.Local.OrderBy(x => x.Datum));
                    break;
            }
            datagrid.DataContext = collectionView;
            cb_filter.SelectedIndex = 0;
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
                        //if (Convert.ToDateTime(deleted_Event.Datum) < DateTime.Now)
                        //{
                        //    WM = new Window_Messagebox("Das Datum liegt in der Vergangenheit");
                        //    WM.Show();
                        //}
                        ctx.Event.Remove(deleted_Event);
                        ctx.EventPos.Where(x => x.ID_Veranstaltung == deleted_Event.ID_Veranstaltung).ToList().ForEach(x => ctx.EventPos.Remove(x));
                        WM = new Window_Messagebox(deleted_Event.Name + " wurde gelöscht.");
                    }
                    break;
            }
            ctx.SaveChanges();
            Refresh_Click(null, null);
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
            collectionView.MoveCurrentToLast();
            datagrid.ScrollIntoView(collectionView.CurrentItem);
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

        private void cb_filter_DropDownClosed(object sender, EventArgs e)
        {
            collectionView.SortDescriptions.Clear();
            int direction = 0;
            if ((bool)RB_Desc.IsChecked)
                direction = 1;

            string[] filteroptions = new string[11];
            switch (gridType)
            {
                case "ListeProdukte":
                    filteroptions = CreateDataGrid.GetFilterNamesProdukte();
                    break;
                case "ListeProduzenten":
                    filteroptions = CreateDataGrid.GetFilterNamesProduzenten();
                    break;
                case "ListeEvents":
                    filteroptions = CreateDataGrid.GetFilterNamesEvents();
                    break;
            }

            if(cb_filter.SelectedIndex == 11)
                currentSortDesc = new SortDescription("Aktiv", (ListSortDirection)direction);
            else
                currentSortDesc = new SortDescription(filteroptions[cb_filter.SelectedIndex], (ListSortDirection)direction);     
            
            collectionView.SortDescriptions.Add(currentSortDesc);

            if (currentSortDesc.Direction == 0)
                RB_Asc.IsChecked = true;
            else
                RB_Desc.IsChecked = true;
        }

        private void Radiobutton_Checked(object sender, RoutedEventArgs e)
        {
            cb_filter_DropDownClosed(null, null);
        }
    }
}
