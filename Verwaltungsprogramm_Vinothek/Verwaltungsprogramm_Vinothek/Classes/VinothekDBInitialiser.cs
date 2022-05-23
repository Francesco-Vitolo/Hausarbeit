using System.Data.Entity;

namespace Verwaltungsprogramm_Vinothek
{
    public class VinothekDBInitializer : CreateDatabaseIfNotExists<VinothekContext>
    {
        protected override void Seed(VinothekContext context)
        {
            context.Benutzer.Add(new Benutzer() 
            { 
                username = "admin", 
                Passwort = "22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff" 
            });

            base.Seed(context);
        }
    }
}
