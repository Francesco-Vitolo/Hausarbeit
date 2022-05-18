using System;
using System.ComponentModel;
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
    public partial class Page_Grid_List : Page
    {
        private VinothekContext Ctx { get; set; }
        private ICollectionView CollectionView { get; set; }
        private string GridType { get; }
        private Window_Abfrage WA { get; set; }
        private Window_Messagebox WM { get; set; }
        private SortDescription CurrentSortDesc { get; set; }
        private string[] Filteroptions { get; set; } = new string[11];
        private string TotalItemsGrid { get; set; }
        public Page_Grid_List(string gridType)
        {
            InitializeComponent();
            Ctx = ContextHelper.GetContext();
            GridType = gridType;
            CreateDG();
            TotalItemsGrid = CollectionView.Cast<object>().Count().ToString();
            labelResults.DataContext = TotalItemsGrid;
            tbSearch.Focus();
        }

        //private async void asyncRefresh()
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
            switch (GridType)
            {
                case "ListeProdukte":
                    datagrid = CreateDataGrid.Produkt(datagrid);
                    CreateDataGrid.ProduktFilter(cb_filter);                    //Combobox füllen (ORDER BY)
                    Filteroptions = CreateDataGrid.GetFilterNamesProdukte();    //Binding Namen für GridColumns
                    CollectionView = CollectionViewSource.GetDefaultView(Ctx.Produkt.Local.OrderBy(x => x.Name)); //ORDER BY Name ist Default
                    datagrid.DataContext = CollectionView;
                    break;

                case "ListeProduzenten":
                    datagrid = CreateDataGrid.Produzent(datagrid);
                    CreateDataGrid.ProduzentFilter(cb_filter);
                    Filteroptions =  CreateDataGrid.GetFilterNamesProduzenten();
                    CollectionView = CollectionViewSource.GetDefaultView(Ctx.Produzent.Local.OrderBy(x => x.Name));
                    datagrid.DataContext = CollectionView;
                    break;

                case "ListeEvents":
                    datagrid = CreateDataGrid.Event(datagrid);
                    CreateDataGrid.EventFilter(cb_filter);
                    Filteroptions = CreateDataGrid.GetFilterNamesEvents();
                    CollectionView = CollectionViewSource.GetDefaultView(Ctx.Event.Local.OrderBy(x => x.Datum)); //ORDER BY Datum
                    datagrid.DataContext = CollectionView;
                    break;
            }
            cb_filter_DropDownClosed(null, null);
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string filterStr = tbSearch.Text.ToLower(); //Suche
            switch (GridType)
            {
                case "ListeProdukte":
                    CollectionView.Filter = x => ((Produkt)x).Name.ToLower().Contains(filterStr);
                    break;
                case "ListeProduzenten":
                    CollectionView.Filter = x => ((Produzent)x).Name.ToLower().Contains(filterStr);
                    break;
                case "ListeEvents":
                    CollectionView.Filter = x => ((Event)x).Name.ToLower().Contains(filterStr);
                    break;
            }
            TotalItemsGrid = CollectionView.Cast<object>().Count().ToString();
            labelResults.DataContext = null;
            labelResults.DataContext = TotalItemsGrid;
        }
       
        private void SelectItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CollectionView.CurrentItem != null)
            {
                switch (GridType)
                {
                    case "ListeProdukte":
                        Produkt selected_produkt = (Produkt)CollectionView.CurrentItem;
                        NavigationService.Navigate(new Page_Produkt(CollectionView));
                        break;
                    case "ListeProduzenten":
                        Produzent selected_produzent = (Produzent)CollectionView.CurrentItem;
                        NavigationService.Navigate(new Page_Produzent(selected_produzent));
                        break;
                    case "ListeEvents":
                        Event selected_event = (Event)CollectionView.CurrentItem;
                        NavigationService.Navigate(new Page_Veranstaltung(selected_event));
                        break;
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ContextHelper.SetNewContext();
            Ctx = ContextHelper.GetContext();
            ContextHelper.LoadTables();
            switch (GridType)
            {
                case "ListeProdukte":
                    CollectionView = CollectionViewSource.GetDefaultView(Ctx.Produkt.Local.OrderBy(x => x.Name));
                    break;
                case "ListeProduzenten":
                    CollectionView = CollectionViewSource.GetDefaultView(Ctx.Produzent.Local.OrderBy(x => x.Name));
                    break;
                case "ListeEvents":
                    CollectionView = CollectionViewSource.GetDefaultView(Ctx.Event.Local.OrderBy(x => x.Datum));
                    break;
            }
            datagrid.DataContext = CollectionView;
            cb_filter.SelectedIndex = 0;

            TotalItemsGrid = CollectionView.Cast<object>().Count().ToString();
            labelResults.DataContext = null;
            labelResults.DataContext = TotalItemsGrid;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (GridType == "ListeProdukte")
            {
                NavigationService.Navigate(new Page_Add_Produkt());
            }
            else if (GridType == "ListeProduzenten")
            {
                NavigationService.Navigate(new Page_Add_Produzent());
            }
            else if (GridType == "ListeEvents")
            {
                NavigationService.Navigate(new Page_Add_Veranstaltung());
            }
            CollectionView.Refresh();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            switch (GridType)
            {
                case "ListeProdukte":
                    Produkt deleted_Produ = (Produkt)CollectionView.CurrentItem;
                    WA = new Window_Abfrage($"Soll {deleted_Produ.Name} gelöscht werden"); //Abfrage
                    WA.ShowDialog();
                    if (WA.GetOption()) //Option Ja oder Nein
                    {
                        Ctx.Produkt.Remove(deleted_Produ); // löschen
                        var list = Ctx.EventPos.Where(x => x.ID_Produkt == deleted_Produ.ID_Produkt).ToList();
                        list.ForEach(x => Ctx.EventPos.Remove(x));
                        WM = new Window_Messagebox(deleted_Produ.Name + " wurde gelöscht.");
                        WM.ShowDialog();
                    }
                    break;

                case "ListeProduzenten":
                    Produzent deleted_Prodz = (Produzent)CollectionView.CurrentItem;
                    WA = new Window_Abfrage($"Soll {deleted_Prodz.Name} gelöscht werden");
                    WA.ShowDialog();
                    if (WA.GetOption())
                    {
                        if (Ctx.Produkt.FirstOrDefault(x => x.ID_Produzent == deleted_Prodz.ID_Produzent) == null) //Prüfen, ob Produzent noch Teil von Produkt ist
                        {
                            Ctx.Produzent.Remove(deleted_Prodz);
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
                    Event deleted_Event = (Event)CollectionView.CurrentItem;
                    WA = new Window_Abfrage($"Soll {deleted_Event.Name} gelöscht werden");
                    WA.ShowDialog();
                    if (WA.GetOption())
                    {
                        Ctx.Event.Remove(deleted_Event);
                        Ctx.EventPos.Where(x => x.ID_Veranstaltung == deleted_Event.ID_Veranstaltung).ToList().ForEach(x => Ctx.EventPos.Remove(x)); //Event-ID aus EventPos löschen 
                        WM = new Window_Messagebox(deleted_Event.Name + " wurde gelöscht.");
                    }
                    break;
            }
            Ctx.SaveChanges();
            Refresh_Click(null, null);
        }

        private void Duplicate_Click(object sender, RoutedEventArgs e)
        {
            object DuplicateObj = null;
            switch (GridType)
            {
                case "ListeProdukte":
                    DuplicateObj = Ctx.Produkt.Add((Produkt)CollectionView.CurrentItem);
                    break;
                case "ListeProduzenten":
                    DuplicateObj = Ctx.Produzent.Add((Produzent)CollectionView.CurrentItem);
                    break;
                case "ListeEvents":
                    Event evnt = (Event)CollectionView.CurrentItem;
                    DuplicateObj = evnt;
                    var list = Ctx.EventPos.Where(x => x.ID_Veranstaltung == evnt.ID_Veranstaltung).ToList();
                    Ctx.Event.Add(evnt);
                    Ctx.SaveChanges();      //Muss gespeichert werden, um ID zu erhalten (weil automatisch generiert)
                    foreach (var item in list)
                    {
                        EventPos EP = new EventPos();   //Dann in EventPos - Tabelle eintragen
                        EP.ID_Veranstaltung = evnt.ID_Veranstaltung;
                        EP.ID_Produkt = item.ID_Produkt;
                        Ctx.EventPos.Add(EP);
                    }
                    break;
            }
            Ctx.SaveChanges();
            Refresh_Click(null, null);
            datagrid.ScrollIntoView(DuplicateObj);
        }

        private void cb_filter_DropDownClosed(object sender, EventArgs e) //Sortieren
        {
            CollectionView.SortDescriptions.Clear();
            int direction = 0;
            if ((bool)RB_Desc.IsChecked)
                direction = 1;

            CurrentSortDesc = new SortDescription(Filteroptions[cb_filter.SelectedIndex], (ListSortDirection)direction);
            CollectionView.SortDescriptions.Add(CurrentSortDesc); //collectionview wird sortiert --> datagrid

            if (CurrentSortDesc.Direction == 0) //Radiobuttons belegen
            {
                RB_Asc.IsChecked = true;
            }
            else
            {
                RB_Desc.IsChecked = true;
            }
        }

        private void Radiobutton_Checked(object sender, RoutedEventArgs e) //Neu Ordnen
        {
            cb_filter_DropDownClosed(null, null);
        }

        private void Datagrid_KeyDel(object sender, KeyEventArgs e) //ausgewähltes Objekt löschen
        {
            if (e.Key == Key.Delete)
                Delete_Click(null, null);
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Key == Key.R)
                    Refresh_Click(null, null);
                if (e.Key == Key.N)
                    Add_Click(null, null);
                if (e.Key == Key.D)
                    Duplicate_Click(null, null);
            }
        }
    }
}
