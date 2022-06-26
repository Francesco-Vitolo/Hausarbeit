using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Produkt.xaml
    /// </summary>
    public partial class Page_Produkt : Page
    {
        private Produkt Produkt { get; set; }
        private VinothekContext Ctx { get; set; }
        private Window_PDF_Viewer WPDF { get; set; }
        private Window_Messagebox WM { get; set; }
        private ICollectionView CollectionView { get; }

        private Random Rand { get; set; }

        public Page_Produkt(ICollectionView collectionView)
        {
            InitializeComponent();
            Ctx = ContextHelper.GetContext();
            CollectionView = collectionView;
            Produkt = (Produkt)CollectionView.CurrentItem;
            DataContext = Produkt;
            Rand = new Random();
        }

        private void UmschaltenBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (felder.IsEnabled == false)
            {
                felder.IsEnabled = true;
                modus.Text = "bearbeiten";
                TextBox tbFokus = felder.GetTbs().FirstOrDefault();
                tbFokus.Focus();
                tbFokus.CaretIndex = 999;
            }
            else
            {
                felder.IsEnabled = false;
                modus.Text = "anschauen";
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            //Ctx.SaveChanges();
            btn_create_pdf_Click(null, null);
            NavigationService.GoBack();
        }

        private void Button_Click_BildAuswählen(object sender, RoutedEventArgs e) //Bild auswählen
        {
            string imgPath = Files.SelectImage();

            if (imgPath != null)
            {
                byte[] b = ImageConverter.ConvertImageToByteArray(imgPath);
                path.Text = imgPath;
                Produkt.Picture = b;
                pic.DataContext = null;
                pic.DataContext = Produkt;
            }
        }


        private void Add_Produzent_Click(object sender, RoutedEventArgs e) //Produzent auswählen
        {
            Window_Select_Object WSP = new Window_Select_Object("ListeProduzenten");
            WSP.ShowDialog();
            Produzent p = (Produzent)WSP.GetObj(); //Produzent nehmen
            if(p != null)
            {
                Produkt.Produzent = p;
                DataContext = null;
                DataContext = Produkt;
                Produkt.Produzent = null;
                Produkt.ID_Produzent = p.ID_Produzent;
            }

        }
        private void btn_download_pdf_Click(object sender, RoutedEventArgs e) //PDF download, wenn in db vorhanden. Sonst zuerst btn_create_pdf_Click
        {
            if (Produkt.PDF_file != null)
            {
                Files.SavePDF(Produkt.PDF_file, Produkt.Name + ".pdf");
            }
            else
            {
                WM = new Window_Messagebox("Bitte erst eine PDF - Datei erstellen");
                WM.Show();
            }
        }
        private void btn_create_pdf_Click(object sender, RoutedEventArgs e)
        {
            PDF pdf = new PDF();
            byte[] byteArrayPDF = pdf.CreateFromProd(Produkt);
            Produkt.PDF_file = byteArrayPDF;
            Ctx.SaveChanges();
            //WM = new Window_Messagebox($"Eine PDF - Datei wurde erstellt:\n{pdf.filename}");
            //WM.Show();
        }
        private async void btn_show_pdf_Click(object sender, RoutedEventArgs e) 
        {
            if (Produkt.PDF_file != null)
            {
                string tempfile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\tmp\tmp_{Rand.Next(0,1000)}.pdf"; //temporäre Datei wird erstellt
                File.WriteAllBytes(tempfile, Produkt.PDF_file);
                WPDF = new Window_PDF_Viewer(tempfile);
                WPDF.ShowDialog();
                await Timer(5000);      //PDF noch nicht freigegeben, deshalb timer
                File.Delete(tempfile); //löschen
            }
            else
            {
                WM = new Window_Messagebox("Noch keine PDF - Datei vorhanden");
                WM.Show();
            }
        }
        private Task Timer(int i)
        {
            return Task.Run(() => { Thread.Sleep(i); });
        }

        private void btn_zwischenablage_Click(object sender, RoutedEventArgs e)
        {

            var img = Files.SelectImgfromClipboard();
            if (img != null)
            {
                var picByteArray = ImageConverter.ConvertImageFromClipboard(img);
                Produkt.Picture = picByteArray;
                pic.DataContext = null;
                pic.DataContext = Produkt;
                path.Text = "Zwischenablage";
            }
        }
        private void MoveNext_Click(object sender, RoutedEventArgs e) //Nächstes Obj
        {
            CollectionView.MoveCurrentToNext();
            if (CollectionView.IsCurrentAfterLast)
            {
                CollectionView.MoveCurrentToFirst();
            }
            Produkt = (Produkt)CollectionView.CurrentItem;
            DataContext = Produkt;
            pic.DataContext = Produkt;
            path.Text = null;
        }

        private void MovePrev_Click(object sender, RoutedEventArgs e) //Vorheriges Obj
        {
            CollectionView.MoveCurrentToPrevious();
            if (CollectionView.IsCurrentBeforeFirst)
            {
                CollectionView.MoveCurrentToLast();
            }
            Produkt = (Produkt)CollectionView.CurrentItem;
            DataContext = Produkt;
            pic.DataContext = Produkt;
            path.Text = null;
        }

        private void Button_Click_BildEntfernen(object sender, RoutedEventArgs e)
        {
            Produkt.Picture = null;
            pic.DataContext = null;
            path.Text = "";
        }

        private void WebSuche_Click(object sender, RoutedEventArgs e)
        {
            Process.Start($"https://google.com/search?q={Produkt.Name}+{Produkt.Produzent.Name}");
        }
    }
}

