using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private VinothekContext ctx = new VinothekContext();
        private Event veranstaltung;
        private List<Produkt> PRODS = new List<Produkt>(); //Für datagrid

        private List<EventPos> EVNT_POS = new List<EventPos>(); //Falls der Vorgang nicht gespeichert wird, braucht man noch del/added EPs
        private List<EventPos> del_EVNT_POS = new List<EventPos>();
        private List<EventPos> added_EVNT_POS = new List<EventPos>();

        public Page_Veranstaltung(Event veranstaltung)
        {
            InitializeComponent();
            this.veranstaltung = ctx.Event.FirstOrDefault(x => x.ID_Veranstaltung == veranstaltung.ID_Veranstaltung);
            DataContext = this.veranstaltung;
            CreateDataGrid.Produkt(data);
            ctx.EventPos.Load();
            EVNT_POS = ctx.EventPos.Where(x => x.ID_Veranstaltung == veranstaltung.ID_Veranstaltung).ToList(); //Liste mit EPs wird gefüllt
            foreach (var ep in EVNT_POS)
            {
                PRODS.Add(ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == ep.ID_Produkt)); 
            }
            data.DataContext = PRODS;
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Produkt selected_produzent = (Produkt)data.CurrentItem;
            if (selected_produzent != null)
                NavigationService.Navigate(new Page_Produkt(selected_produzent, new System.ComponentModel.SortDescription("Name", 0)));
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
                EP = ctx.EventPos.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt && x.ID_Veranstaltung == veranstaltung.ID_Veranstaltung); //Aus dem ctx holen
                del_EVNT_POS.Add(EP);
            }
            else //Wenn EP schon hinzugefügt wurde und dann wieder entfernt wird ohne dazwischen zu speichern
            {
                EP = added_EVNT_POS.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt && x.ID_Veranstaltung == veranstaltung.ID_Veranstaltung);
                added_EVNT_POS.Remove(EP); // Prod muss aus added_EVNT_POS gelöscht werden
            }
            PRODS.Remove(prod); //Aus Datagrid entfernt
            data.DataContext = null;
            data.DataContext = PRODS;
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
                    EP.ID_Veranstaltung = veranstaltung.ID_Veranstaltung;
                    EP.ID_Produkt = p.ID_Produkt;
                    PRODS.Add(p);
                    added_EVNT_POS.Add(EP);
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


        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (del_EVNT_POS.Count > 0) //Wenn Liste nicht leer
                del_EVNT_POS.ForEach(x => ctx.EventPos.Remove(x));
            if (added_EVNT_POS.Count > 0)
                added_EVNT_POS.ForEach(x => ctx.EventPos.Add(x));
            veranstaltung.Zeit = felder.GetTime(); //Daten aus Uc_Veranstaltung holen
            veranstaltung.Datum = DateTime.Parse(felder.GetDate());
            ctx.SaveChanges();
            NavigationService.GoBack();
        }

        private void CreatePDF_Click(object sender, RoutedEventArgs e)
        {
            PDF pdf = new PDF();
            pdf.CreateFromEvent(PRODS, veranstaltung); //PDF zum Event wird erstellt
        }
    }
}
