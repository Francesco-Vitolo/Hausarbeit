using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Add_Produzent.xaml
    /// </summary>
    public partial class Page_Add_Produzent : Page
    {
        private VinothekContext ctx;
        public Page_Add_Produzent()
        {
            InitializeComponent();
            ctx = ContextHelper.GetContext();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Produzent produzent = new Produzent(); //Daten aus Uc_Produzent holen
            var tbs = felder.GetTbs();
            if (tbs[0].Text != "") //Wenn Name nicht eingegeben
            {
                produzent.Name = tbs[0].Text.Trim(); //Leerzeichen abschneiden
                produzent.Land = tbs[1].Text;
                produzent.Region = tbs[2].Text;
                if (int.TryParse(tbs[3].Text, out int i))
                    produzent.Hektar = i;
                produzent.Adresse = tbs[4].Text;
                produzent.Email = tbs[6].Text;
                produzent.Telefon= tbs[7].Text;
                ctx.Produzent.Add(produzent);
                ctx.SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                Window_Messagebox WM = new Window_Messagebox("Bitte Namen eingeben");
                WM.ShowDialog();
            }
        }
    }
}

