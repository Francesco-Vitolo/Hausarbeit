using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Verwaltungsprogramm_Vinothek
{
    public static class ContextHelper
    {
        private static VinothekContext ctx { get; set; }
        public static VinothekContext GetContext()
        {
            return ctx;
        }

        public static void SetNewContext()
        {
            ctx = new VinothekContext();
            LoadTables();
        }

        public static void SaveChanges()
        {
            ctx.SaveChanges();
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
        public static void Reset()
        {
            var entries = ctx.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToArray();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
