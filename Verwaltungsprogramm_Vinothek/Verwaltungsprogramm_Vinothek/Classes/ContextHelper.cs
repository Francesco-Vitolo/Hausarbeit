using System.Configuration;
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

        public static void ChangeDatabase()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["VinothekContext"].ConnectionString =
            @"metadata=res://*/Vinothek.csdl|res://*/Vinothek.ssdl|res://*/Vinothek.msl;
            provider=System.Data.SqlClient;
            provider connection string=&quot;
            Data Source=localhost; 
            initial catalog=DB_Vinothek;
	        integrated security=true;
	        MultipleActiveResultSets=True
	        App=EntityFramework&quot;""providerName = ""System.Data.SqlClient"" />";
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("connectionStrings");
            //Context = new VinothekContext();
            //Context.Database.Connection.ConnectionString = Context.Database.Connection.ConnectionString.Replace("(LocalDb)\\MSSQLLocalDB", $"{newConn}");
            //LoadTables();
            //SaveChanges();
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
