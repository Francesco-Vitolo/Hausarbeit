using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Veranstaltung.xaml
    /// </summary>
    public partial class Window_Veranstaltung : Window
    {
        List<EventPos> EVNT_POS = new List<EventPos>();
        List<Produkt> PRODS = new List<Produkt>();
        Vinothek ctx = new Vinothek();
        public Window_Veranstaltung(Event veranstaltung)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            data = CreateDataGrid.Produkt(data);
            EVNT_POS = ctx.EventPos.Where(x => x.ID_Veranstaltung == veranstaltung.ID_Veransatultung).ToList();
            foreach (var v in EVNT_POS)
            {
                PRODS.Add(ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == v.ID_Produkt));
            }
            DataContext = PRODS;
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ItemInfos.Show(data.SelectedItem, "ListeEvents");
        }
    }
}
