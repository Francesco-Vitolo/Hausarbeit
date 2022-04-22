using System;
using System.Collections.Generic;
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
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Veranstaltung.xaml
    /// </summary>
    public partial class Page_Veranstaltung : Page
    {
        List<EventPos> EVNT_POS = new List<EventPos>();
        List<Produkt> PRODS = new List<Produkt>();

        List<EventPos> del_EVNT_POS = new List<EventPos>();
        List<EventPos> added_EVNT_POS = new List<EventPos>();

        VinothekContext ctx = new VinothekContext();
        Event veranstaltung;
        public Page_Veranstaltung(Event veranstaltung)
        {
            InitializeComponent();
            this.veranstaltung = veranstaltung;
            DataContext = ctx.Event.FirstOrDefault(x => x.ID_Veranstaltung == veranstaltung.ID_Veranstaltung);
            CreateDataGrid.Produkt(data);
            ctx.EventPos.Load();
            EVNT_POS = ctx.EventPos.Where(x => x.ID_Veranstaltung == veranstaltung.ID_Veranstaltung).ToList();
            foreach (var v in EVNT_POS)
            {
                PRODS.Add(ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == v.ID_Produkt));
            }
            data.DataContext = PRODS;
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

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            del_EVNT_POS.ForEach(x => ctx.EventPos.Remove(x));
            added_EVNT_POS.ForEach(x => ctx.EventPos.Add(x));
            ctx.SaveChanges();
            NavigationService.GoBack();
        }

        private void DelProd_Click(object sender, RoutedEventArgs e)
        {
            Produkt prod = (Produkt)data.SelectedItem;
            del_EVNT_POS.Add(ctx.EventPos.FirstOrDefault(x => x.ID_Veranstaltung == veranstaltung.ID_Veranstaltung && x.ID_Produkt == prod.ID_Produkt));
            PRODS.Remove(prod);
            data.DataContext = null;
            data.DataContext = PRODS;
        }

        private void AddProd_Click(object sender, RoutedEventArgs e)
        {
            Window_Select_Object WSO = new Window_Select_Object("ListeProdukte");
            WSO.ShowDialog();
            Produkt p = (Produkt)WSO.GetObj();
            if (PRODS.FirstOrDefault(x => x.ID_Produkt == p.ID_Produkt) == null)
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
}
