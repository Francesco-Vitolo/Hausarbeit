using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verwaltungsprogramm_Vinothek
{
    public static class ItemInfos
    {
        public static void Show(object o, string location)
        {
            if (o != null)
            {
                switch (location)
                {
                    case "ListeProdukte":
                        Produkt selected_produkt = (Produkt)o;
                        Window_Produkt WW = new Window_Produkt(selected_produkt);
                        WW.ShowDialog();
                        break;
                    case "ListeProduzenten":
                        Produzent selected_produzent = (Produzent)o;
                        Window_Produzent WP = new Window_Produzent(selected_produzent);
                        WP.ShowDialog();
                        break;
                    case "ListeEvents":
                        Event selected_event = (Event)o;
                        Window_Veranstaltung WV = new Window_Veranstaltung(selected_event);
                        WV.ShowDialog();
                        break;
                }
            }
        }
    }
}
