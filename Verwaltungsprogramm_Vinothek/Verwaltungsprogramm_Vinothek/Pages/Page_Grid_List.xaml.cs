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
    public partial class Page_Grid_List : Page
    {
        private VinothekContext ctx = new VinothekContext();
        private ICollectionView collectionView;
        private string gridType;
        private Window_Abfrage WA;
        private Window_Messagebox WM;
        private SortDescription currentSortDesc;
        string[] filteroptions = new string[11];
        public Page_Grid_List(string gridType)
        {
            InitializeComponent();
            this.gridType = gridType;
            CreateDG();
        }

        private async void asyncRefresh()
        {
            while (true)
            {
                await Timer(5000);
            }
        }
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
                    CreateDataGrid.ProduktFilter(cb_filter);                    //Combobox füllen (ORDER BY)
                    filteroptions = CreateDataGrid.GetFilterNamesProdukte();    //Binding Namen für GridColumns
                    ctx.Produkt.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local.OrderBy(x => x.Name)); //ORDER BY Name ist Default
                    datagrid.DataContext = collectionView;
                    cb_filter.Items.Add("Aktiv");                               //"Aktiv" Eigenschaft ist nicht sichtbar im Datagrid
                    break;

                case "ListeProduzenten":
                    CreateDataGrid.Produzent(datagrid);
                    ctx.Produzent.Load();
                    CreateDataGrid.ProduzentFilter(cb_filter);
                    filteroptions =  CreateDataGrid.GetFilterNamesProduzenten();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Produzent.Local.OrderBy(x => x.Name));
                    datagrid.DataContext = collectionView;
                    break;

                case "ListeEvents":
                    CreateDataGrid.Event(datagrid);
                    CreateDataGrid.EventFilter(cb_filter);
                    filteroptions = CreateDataGrid.GetFilterNamesEvents();
                    ctx.Event.Load();
                    collectionView = CollectionViewSource.GetDefaultView(ctx.Event.Local.OrderBy(x => x.Datum)); //ORDER BY Datum
                    datagrid.DataContext = collectionView;
                    break;
            }
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string filterStr = tbSearch.Text.ToLower(); //Suche
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
       
        private void SelectItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (collectionView.CurrentItem != null) //Ausgewähltes Objekt wird weitergegeben mit Sortierreihenfolge
            {
                switch (gridType)
                {
                    case "ListeProdukte":
                        Produkt selected_produkt = (Produkt)collectionView.CurrentItem;
                        NavigationService.Navigate(new Page_Produkt(selected_produkt, currentSortDesc));
                        break;
                    case "ListeProduzenten":
                        Produzent selected_produzent = (Produzent)collectionView.CurrentItem;
                        NavigationService.Navigate(new Page_Produzent(selected_produzent));
                        break;
                    case "ListeEvents":
                        Event selected_event = (Event)collectionView.CurrentItem;
                        NavigationService.Navigate(new Page_Veranstaltung(selected_event));
                        break;
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ctx = new VinothekContext(); //neuer ctx
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            switch (gridType)
            {
                case "ListeProdukte":
                    Produkt deleted_Produ = (Produkt)collectionView.CurrentItem;
                    WA = new Window_Abfrage($"Soll {deleted_Produ.Name} gelöscht werden"); //Abfrage
                    WA.ShowDialog();
                    if (WA.GetOption()) //Option Ja oder Nein
                    {
                        ctx.Produkt.Remove(deleted_Produ); // löschen
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
                        if (ctx.Produkt.FirstOrDefault(x => x.ID_Produzent == deleted_Prodz.ID_Produzent) == null) //Prüfen, ob Produzent noch Teil von Produkt ist
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
                        ctx.Event.Remove(deleted_Event);
                        ctx.EventPos.Where(x => x.ID_Veranstaltung == deleted_Event.ID_Veranstaltung).ToList().ForEach(x => ctx.EventPos.Remove(x)); //Event-ID aus EventPos löschen 
                        WM = new Window_Messagebox(deleted_Event.Name + " wurde gelöscht.");
                    }
                    break;
            }
            ctx.SaveChanges();
            Refresh_Click(null, null);
        }

        private void Duplicate_Click(object sender, RoutedEventArgs e)
        {
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
                    ctx.SaveChanges();      //Muss gespeichert werden, um ID zu erhalten (weil automatisch generiert)
                    foreach (var item in list)
                    {
                        EventPos EP = new EventPos();   //Dann in EventPos - Tabelle eintragen
                        EP.ID_Veranstaltung = evnt.ID_Veranstaltung;
                        EP.ID_Produkt = item.ID_Produkt;
                        ctx.EventPos.Add(EP);
                    }
                    break;
            }
            ctx.SaveChanges();
            Refresh_Click(null, null);
            collectionView.MoveCurrentToLast(); //Ansicht zum neuen Objekt
            datagrid.ScrollIntoView(collectionView.CurrentItem);
        }            

        private void cb_filter_DropDownClosed(object sender, EventArgs e) //Sortieren
        {
            collectionView.SortDescriptions.Clear();
            int direction = 0;
            if ((bool)RB_Desc.IsChecked)
                direction = 1;

            if(cb_filter.SelectedIndex == 11)
            {
                currentSortDesc = new SortDescription("Aktiv", (ListSortDirection)direction);
            }
            else
            {
                currentSortDesc = new SortDescription(filteroptions[cb_filter.SelectedIndex], (ListSortDirection)direction);
            }            
            collectionView.SortDescriptions.Add(currentSortDesc); //collectionview wird sortiert --> datagrid

            if (currentSortDesc.Direction == 0) //Radiobuttons belegen
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
    }
}
