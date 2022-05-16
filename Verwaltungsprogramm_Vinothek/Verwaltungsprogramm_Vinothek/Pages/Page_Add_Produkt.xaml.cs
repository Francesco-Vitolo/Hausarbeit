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
            var tbs = felderProdukt.GetTbs(); //Uc_Produkt alle Textboxen

            newProd.Name = tbs[0].Text.Trim(); //Leerzeichen abschneiden
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

            if (tbs[0].Text == "" || produzent == null)
            {
                WM = new Window_Messagebox("Bitte Namen und Weingut eingeben");
                WM.ShowDialog();
            }
            else
            {
                ctx.Produkt.Add(newProd);
                ctx.SaveChanges();
                NavigationService.GoBack();
            }
        }
        private void Button_Click_BildAuswählen(object sender, RoutedEventArgs e) //Bild auswählen und un byte - Array umwandeln
        {
            string imgPath = SelectFile.Image();
            if (imgPath != null)
            {
                ImgSrc.Text = "Pfad: " + imgPath;
                byte[] binaryPic = Imageconverter.ConvertImageToByteArray(imgPath);
                newProd.Picture = binaryPic;
            }
        }

        private void Button_Click_BildEntfernen(object sender, RoutedEventArgs e) //Datacontext zurücksetzen
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
                byte[] binaryPic = Imageconverter.ConvertImageFromClipboard(img);
                newProd.Picture = binaryPic;
                ImgSrc.Text = "Aus Zwischenablage";
            }
        }

        private void Add_Produzent_Click(object sender, RoutedEventArgs e)
        {
            Window_Select_Object WSP = new Window_Select_Object("ListeProduzenten");
            WSP.ShowDialog();
            produzent = (Produzent)WSP.GetObj(); //Produzent nehmen
            if (produzent != null)
            {
                newProd.Produzent = ctx.Produzent.FirstOrDefault(x => x.Name == produzent.Name);
                TextBox tb_prod = felderProdukt.GetProd();
                tb_prod.DataContext = newProd;
            }
        }
    }
}
