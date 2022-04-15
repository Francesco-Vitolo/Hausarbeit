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
            switch (location)
            {
                case "ListeProdukte":
                    Produkt selected_produkt = (Produkt)o;
                    Window_Wein WW = new Window_Wein(selected_produkt);
                    WW.ShowDialog();
                    break;
                case "ListeProduzenten":
                    Produzent selected_produzent = (Produzent)o;
                    Window_Produzent WP = new Window_Produzent(selected_produzent);
                    WP.ShowDialog();
                    break;
                case "ListeEvents":
                    //...
                    break;
            }
        }
    }
}
