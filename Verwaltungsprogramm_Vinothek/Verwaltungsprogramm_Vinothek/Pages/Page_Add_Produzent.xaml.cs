using System.Collections.Generic;
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
        private VinothekContext Ctx { get; }
        private List<TextBox> Tbs { get; set; }
        public Page_Add_Produzent()
        {
            InitializeComponent();
            Ctx = ContextHelper.GetContext();
            Tbs = felder.GetTbs();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Produzent produzent = new Produzent(); //Daten aus Uc_Produzent holen
            if (Tbs[0].Text != "") //Wenn Name nicht eingegeben
            {
                produzent.Name = Tbs[0].Text.Trim(); //Leerzeichen abschneiden
                produzent.Land = Tbs[1].Text;
                produzent.Region = Tbs[2].Text;
                if (int.TryParse(Tbs[3].Text, out int i))
                    produzent.Hektar = i;
                produzent.Adresse = Tbs[4].Text;
                produzent.Email = Tbs[5].Text;
                produzent.Telefon= Tbs[6].Text;
                Ctx.Produzent.Add(produzent);
                Ctx.SaveChanges();
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

