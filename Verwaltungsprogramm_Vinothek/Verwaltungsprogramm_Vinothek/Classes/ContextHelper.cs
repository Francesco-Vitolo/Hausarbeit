using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Verwaltungsprogramm_Vinothek
{
    public static class ContextHelper
    {
        private static VinothekContext Context { get; set; }
        public static VinothekContext GetContext()
        {
            return Context;
        }

        public static void SetNewContext()
        {
            Context = new VinothekContext();
            LoadTables();
        }

        public static void SaveChanges()
        {
            Context.SaveChanges();
        }

        public static void LoadTables()
        {
            Context.Benutzer.Load();
            Context.Event.Load();
            Context.EventPos.Load();
            Context.Logins.Load();
            Context.Produkt.Load();
            Context.Produzent.Load();
        }
        public static void Reset()
        {
            var entries = Context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToArray();
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
