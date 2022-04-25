using System.Windows.Controls;
using Verwaltungsprogramm_Vinothek.Pages;

namespace Verwaltungsprogramm_Vinothek
{
    public static class ItemInfos
    {
        public static Page Show(object o, string location)
        {
            if (o != null)
            {
                switch (location)
                {
                    case "ListeProdukte":
                        Produkt selected_produkt = (Produkt)o;
                        Page_Produkt PProdukte = new Page_Produkt(selected_produkt);
                        return PProdukte;
                    case "ListeProduzenten":
                        Produzent selected_produzent = (Produzent)o;
                        Page_Produzent PProduzenten = new Page_Produzent(selected_produzent);
                        return PProduzenten;
                    case "ListeEvents":
                        Event selected_event = (Event)o;
                        Page_Veranstaltung PEvent = new Page_Veranstaltung(selected_event);
                        return PEvent;
                }
            }
            return null;
        }
    }
}