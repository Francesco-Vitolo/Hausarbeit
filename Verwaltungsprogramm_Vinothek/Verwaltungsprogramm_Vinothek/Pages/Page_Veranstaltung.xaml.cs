using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Veranstaltung.xaml
    /// </summary>
    public partial class Page_Veranstaltung : Page
    {
        private VinothekContext Ctx { get; set; }
        private Event Veranstaltung { get; set; }

        private List<Produkt> PRODS { get; set; } = new List<Produkt>(); //Für datagrid
        private List<EventPos> EVNT_POS { get; set; } = new List<EventPos>(); //Falls der Vorgang nicht gespeichert wird, braucht man noch del/added EPs
        private List<EventPos> DEL_EVNT_POS { get; set; } = new List<EventPos>();
        private List<EventPos> ADDED_EVNT_POS { get; set; } = new List<EventPos>();

        public Page_Veranstaltung(Event veranstaltung)
        {
            InitializeComponent();
            Ctx = ContextHelper.GetContext();
            data = CreateDataGrid.Produkt(data);

            Veranstaltung = veranstaltung;
            DataContext = Veranstaltung;

            EVNT_POS = Ctx.EventPos.Where(x => x.ID_Veranstaltung == veranstaltung.ID_Veranstaltung).ToList(); //Liste mit EPs wird gefüllt
            foreach (var ep in EVNT_POS)
            {
                PRODS.Add(Ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == ep.ID_Produkt)); 
            }
            data.DataContext = PRODS;           
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Produkt selected_produzent = (Produkt)data.CurrentItem;
            if (selected_produzent != null)
            {
                ICollectionView collectionView = CollectionViewSource.GetDefaultView(PRODS);
                NavigationService.Navigate(new Page_Produkt(collectionView));
            }
        }
        private void UmschaltenBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (felder.IsEnabled == false)
            {
                felder.IsEnabled = true;
                modus.Text = "bearbeiten";
            }
            else
            {
                felder.IsEnabled = false;
                modus.Text = "anschauen";
            }
        }
      
        private void DelProd_Click(object sender, RoutedEventArgs e)
        {
            Produkt prod = (Produkt)data.SelectedItem;
            EventPos EP = new EventPos();
            if (EVNT_POS.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt) != null) //Wenn EP nicht vorhanden in Liste hinzugefügte EPS
            {
                EP = Ctx.EventPos.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt && x.ID_Veranstaltung == Veranstaltung.ID_Veranstaltung); //Aus dem ctx holen
                DEL_EVNT_POS.Add(EP);
            }
            else //Wenn EP schon hinzugefügt wurde und dann wieder entfernt wird ohne dazwischen zu speichern
            {
                EP = ADDED_EVNT_POS.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt && x.ID_Veranstaltung == Veranstaltung.ID_Veranstaltung);
                ADDED_EVNT_POS.Remove(EP); // Prod muss aus added_EVNT_POS gelöscht werden
            }
            PRODS.Remove(prod); //Aus Datagrid entfernt
            data.DataContext = null;
            data.DataContext = PRODS;
            DataContext = Veranstaltung;
        }

        private void AddProd_Click(object sender, RoutedEventArgs e)
        {
            Window_Select_Object WSO = new Window_Select_Object("ListeProdukte");
            WSO.ShowDialog();
            var prod = WSO.GetObj(); //keine direkte Umwandlung möglich --> Bei Abbruch ist der Rückgabetyp !Produkt
            if (prod != null)       //Falls abgebrochen wird
            {
                Produkt p = (Produkt)prod;

                if (PRODS.FirstOrDefault(x => x.ID_Produkt == p.ID_Produkt) == null) //Prüfen, ob das Produkt mit der selben ID schon vohanden
                {
                    EventPos EP = new EventPos();
                    EP.ID_Veranstaltung = Veranstaltung.ID_Veranstaltung;
                    EP.ID_Produkt = p.ID_Produkt;
                    PRODS.Add(p);
                    ADDED_EVNT_POS.Add(EP);
                    data.DataContext = null;
                    data.DataContext = PRODS;
                }
                else
                {
                    Window_Messagebox WM = new Window_Messagebox("Produkt bereits vorhanden");
                    WM.ShowDialog();
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

            List<Produkt> listRefresh = new List<Produkt>();
            ContextHelper.SetNewContext();
            Ctx = ContextHelper.GetContext();
            foreach (var prod in PRODS)
            {
                listRefresh.Add(Ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt));
            }
            PRODS = listRefresh;
            data.DataContext = null;
            data.DataContext = PRODS;
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (DEL_EVNT_POS.Count > 0) //Wenn Liste nicht leer
                DEL_EVNT_POS.ForEach(x => Ctx.EventPos.Remove(x));
            if (ADDED_EVNT_POS.Count > 0)
                ADDED_EVNT_POS.ForEach(x => Ctx.EventPos.Add(x));
            Veranstaltung.Zeit = felder.GetTime(); //Daten aus Uc_Veranstaltung holen
            Veranstaltung.Datum = DateTime.Parse(felder.GetDate());
            Ctx.SaveChanges();
            NavigationService.GoBack();
        }

        private void CreatePDF_Click(object sender, RoutedEventArgs e)
        {
            PDF pdf = new PDF();
            pdf.CreateFromEvent(PRODS, Veranstaltung); //PDF zum Event wird erstellt
        }
    }
}
