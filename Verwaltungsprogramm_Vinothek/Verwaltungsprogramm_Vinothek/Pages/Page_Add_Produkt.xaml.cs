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
        private Produzent produzent = null;
        public Page_Add_Produkt()
        {
            InitializeComponent();
            ctx.Produkt.Load();
            ctx.Produzent.Load();
        }

        private void Button_Click_SaveChanges(object sender, RoutedEventArgs e)
        {
            Window_Messagebox WM;
            var tbs = felderProdukt.GetTbs();

            newProd.Name = tbs[0].Text.Trim();
            newProd.Art = tbs[1].Text;
            newProd.Qualitätssiegel = tbs[2].Text;

            if (int.TryParse(tbs[3].Text, out int i))
                newProd.Jahrgang = i;

            newProd.Rebsorten = tbs[4].Text;
            newProd.Geschmack = tbs[6].Text;

            if (int.TryParse(tbs[7].Text, out i))
                newProd.Alkoholgehalt = i;

            if (double.TryParse(tbs[8].Text,out double j))
                newProd.Preis = j;

            newProd.Aktiv = true;

            newProd.Beschreibung = felderProdukt.GetDesc().Text;
            //Prod.Name --> Prod.ID

            string produzentName = tbs[5].Text;
            if (tbs[0].Text == "" || produzent == null)
            {
                WM = new Window_Messagebox("Bitte eingeben du HUND");
                WM.ShowDialog();
                return;
            }
            else
            {
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
                ImgSrc.Text = "Pfad: " + imgPath;
                byte[] b = Imageconverter.ConvertImageToByteArray(imgPath);
                newProd.Picture = b;
            }
        }
        private void Button_Previous_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_BildEntfernen(object sender, RoutedEventArgs e)
        {
            newProd.Picture = null;
            newProd.Picture = null;
            ImgSrc.Text = null;
        }

        private void Button_Click_BildausZwischenablage(object sender, RoutedEventArgs e)
        {
            var img = SelectFile.SelectImgfromClipboard();
            if (img != null)
            {
                var v = Imageconverter.ConvertImageFromClipboard(img);
                newProd.Picture = v;
                ImgSrc.Text = "Aus Zwischenablage";
            }
        }

        private void Add_Produzent_Click(object sender, RoutedEventArgs e)
        {
            Window_Select_Object WSP = new Window_Select_Object("ListeProduzenten");
            WSP.ShowDialog();
            produzent = (Produzent)WSP.GetObj();      
            newProd.Produzent = ctx.Produzent.FirstOrDefault(x => x.Name == produzent.Name);
            TextBox tb_prod = felderProdukt.GetProd();
            tb_prod.DataContext = newProd;
        }
    }
}
