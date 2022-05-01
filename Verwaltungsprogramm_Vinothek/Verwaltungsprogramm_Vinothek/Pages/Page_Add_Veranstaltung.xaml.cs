using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Add_Veranstaltung.xaml
    /// </summary>
    public partial class Page_Add_Veranstaltung : Page
    {
        private List<Produkt> listProd = new List<Produkt>();
        private VinothekContext ctx = new VinothekContext();
        public Page_Add_Veranstaltung()
        {
            InitializeComponent();
            CreateDataGrid.Produkt(data);
            data.DataContext = listProd;
            ctx.Event.Load();
            ctx.EventPos.Load();
        }

        private void AddProd_Click(object sender, RoutedEventArgs e)
        {
            Window_Select_Object WSO = new Window_Select_Object("ListeProdukte");
            WSO.ShowDialog();
            Produkt p = (Produkt)WSO.GetObj();
            if (p != null && listProd.FirstOrDefault(x => x.ID_Produkt == p.ID_Produkt) == null)
            {
                listProd.Add(p);
                data.DataContext = null;
                data.DataContext = listProd;
            }
        }

        private void SaveEvent_Click(object sender, RoutedEventArgs e)
        {
            Event E = new Event();
            List<TextBox> tbs = felder.GetTbs();
            //if (tbs[0].Text != "")
            //{
            if (tbs[0].Text == "")
            {
                Window_Messagebox WM = new Window_Messagebox("Bitte Namen eingeben");
                WM.ShowDialog();
            }
            else
            {
                E.Name = tbs[0].Text;
                if (int.TryParse(tbs[1].Text, out int i))
                    E.AnzahlPersonen = i;
                E.Datum = felder.GetDate();
                E.Zeit = felder.GetTime();
                ctx.Event.Add(E);
                ctx.SaveChanges();

                foreach (var v in listProd)
                {
                    //int i = E.ID_Veranstaltung;
                    EventPos EP = new EventPos();
                    EP.ID_Veranstaltung = E.ID_Veranstaltung;
                    EP.ID_Produkt = v.ID_Produkt;
                    ctx.EventPos.Add(EP);
                    ctx.SaveChanges();
                }
                NavigationService.GoBack();
            }
        }

        private void DelProd_Click(object sender, RoutedEventArgs e)
        {
            listProd.Remove((Produkt)data.SelectedItem);
            data.DataContext = null;
            data.DataContext = listProd;
        }     
    }
}
