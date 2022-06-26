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
        private VinothekContext Ctx { get; }
        private Produkt NewProd { get; set; }
        private Produzent Produzent { get; set; } = null;
        public Page_Add_Produkt()
        {
            InitializeComponent();
            Ctx = ContextHelper.GetContext();
            NewProd = new Produkt();
        }

        private void Button_Click_SaveChanges(object sender, RoutedEventArgs e)
        {   
            var tbs = felderProdukt.GetTbs(); //Uc_Produkt alle Textboxen

            NewProd.Name = tbs[0].Text.Trim(); //Leerzeichen abschneiden
            NewProd.Art = tbs[1].Text;
            NewProd.Qualitätssiegel = tbs[2].Text;

            if (int.TryParse(tbs[3].Text, out int i))
                NewProd.Jahrgang = i;

            NewProd.Rebsorten = tbs[4].Text;
            NewProd.Geschmack = tbs[6].Text;

            if (int.TryParse(tbs[7].Text, out i))
                NewProd.Alkoholgehalt = i;

            if (double.TryParse(tbs[8].Text,out double j))
                NewProd.Preis = j;

            NewProd.Aktiv = true;

            NewProd.Beschreibung = felderProdukt.GetDesc().Text;

            if (tbs[0].Text == "" || Produzent == null)
            {
                Window_Messagebox WM = new Window_Messagebox("Bitte Namen und Weingut eingeben");
                WM.ShowDialog();
            }
            else
            {
                Ctx.Produkt.Add(NewProd);
                PDF pdf = new PDF();
                byte[] byteArrayPDF = pdf.CreateFromProd(NewProd);
                NewProd.PDF_file = byteArrayPDF;
                Ctx.SaveChanges();
                NavigationService.GoBack();
            }
        }
        private void Button_Click_BildAuswählen(object sender, RoutedEventArgs e) //Bild auswählen und un byte - Array umwandeln
        {
            string imgPath = Files.SelectImage();
            if (imgPath != null)
            {
                ImgSrc.Text = "Pfad: " + imgPath;
                byte[] binaryPic = ImageConverter.ConvertImageToByteArray(imgPath);
                NewProd.Picture = binaryPic;
            }
        }

        private void Button_Click_BildEntfernen(object sender, RoutedEventArgs e) //Datacontext zurücksetzen
        {
            NewProd.Picture = null;
            NewProd.Picture = null;
            ImgSrc.Text = null;
        }

        private void Button_Click_BildausZwischenablage(object sender, RoutedEventArgs e)
        {
            var img = Files.SelectImgfromClipboard();
            if (img != null)
            {
                byte[] binaryPic = ImageConverter.ConvertImageFromClipboard(img);
                NewProd.Picture = binaryPic;
                ImgSrc.Text = "Aus Zwischenablage";
            }
        }

        private void Add_Produzent_Click(object sender, RoutedEventArgs e)
        {
            Window_Select_Object WSP = new Window_Select_Object("ListeProduzenten");
            WSP.ShowDialog();
            Produzent = (Produzent)WSP.GetObj(); //Produzent nehmen
            if (Produzent != null)
            {
                NewProd.Produzent = Ctx.Produzent.FirstOrDefault(x => x.Name == Produzent.Name);
                TextBox tb_prod = felderProdukt.GetProd();
                tb_prod.DataContext = NewProd;
            }
        }
    }
}
