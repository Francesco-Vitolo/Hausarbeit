using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Add_Produkt.xaml
    /// </summary>
    public partial class Page_Add_Produkt : Page
    {
        private VinothekContext ctx = new VinothekContext();
        private Produkt newProd = new Produkt();
        public Page_Add_Produkt()
        {
            InitializeComponent();
            ctx.Produkt.Load();
            ctx.Produzent.Load();
            TextBox tb_prod = test.GetProd();
            tb_prod.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window_Messagebox WM;
            var tbs = test.GetTbs();
            newProd.Name = tbs[0].Text;
            newProd.Art = tbs[1].Text;
            newProd.Qualitätssiegel = tbs[2].Text;
            if (int.TryParse(tbs[3].Text, out int i))
                newProd.Jahrgang = i;
            newProd.Rebsorten = tbs[4].Text;
            newProd.Geschmack = tbs[6].Text;
            if (int.TryParse(tbs[7].Text, out i))
                newProd.Alkoholgehalt = i;
            newProd.Beschreibung = test.GetDesc().Text;
            //Prod.Name --> Prod.ID
            string produzentName = tbs[5].Text;

            if (produzentName == "" || tbs[0].Text == "")
            {
                WM = new Window_Messagebox("Bitte eingeben du HUND");
                WM.ShowDialog();
                return;
            }
            else
            {
                List<Produzent> list = ctx.Produzent.Local.ToList();
                Produzent pp = list.FirstOrDefault(x => x.Name == produzentName);

                if (pp != null)
                {
                    newProd.Produzent = pp;
                }
                else
                {
                    WM = new Window_Messagebox($"Noch kein Produzent mit dem Namen vorhanden. Neuer Produzent wurde automatisch angelegt");
                    WM.ShowDialog();
                    Produzent produzent = new Produzent()
                    {
                        Name = produzentName
                    };
                    ctx.Produzent.Add(produzent);
                    ctx.SaveChanges();
                    newProd.Produzent = produzent;
                }
                ctx.Produkt.Add(newProd);
                ctx.SaveChanges();
                Application.Current.MainWindow.Show();
                NavigationService.GoBack();
            }
        }
        private void Button_Click_BildAuswählen(object sender, RoutedEventArgs e)
        {
            string imgPath = SelectFile.Image();

            if (imgPath != null)
            {
                ImgSrc.Content = imgPath;
                byte[] b = Imageconverter.ConvertImageToByteArray(imgPath);
                newProd.Picture = b;
            }

        }
        private void Button_Previous_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
