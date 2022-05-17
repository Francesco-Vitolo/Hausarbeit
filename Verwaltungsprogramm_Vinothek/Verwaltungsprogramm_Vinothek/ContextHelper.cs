using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Verwaltungsprogramm_Vinothek
{
    public static class ContextHelper
    {
        private static VinothekContext ctx;
        public static VinothekContext GetContext()
        {
            return ctx;
        }

        public static void SetNewContext()
        {            
            ctx = new VinothekContext();
            LoadTables();
        }

        public static void LoadTables()
        {            
            ctx.Benutzer.Load();
            ctx.Event.Load();
            ctx.EventPos.Load();
            ctx.Logins.Load();
            ctx.Produkt.Load();
            ctx.Produzent.Load();
        }
    }
}
