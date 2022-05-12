using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Produkt.xaml
    /// </summary>
    public partial class Page_Produkt : Page
    {
        private Produkt prod;
        private VinothekContext ctx = new VinothekContext();
        private Window_PDF_Viewer WPDF;
        private Window_Messagebox WM;
        private ICollectionView collectionView;
        //string filename überarbeiten
        public Page_Produkt(ICollectionView collectionView)
        {
            InitializeComponent();
            ctx.Produkt.Load();
            ctx.Produzent.Load();
            this.collectionView = collectionView;
            prod = (Produkt)this.collectionView.CurrentItem;
            prod = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            DataContext = prod;
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
            ctx.SaveChanges();
            NavigationService.GoBack();
        }

        private void Button_Click_BildAuswählen(object sender, RoutedEventArgs e) //Bild auswählen
        {
            string imgPath = SelectFile.Image();

            if (imgPath != null)
            {
                byte[] b = Imageconverter.ConvertImageToByteArray(imgPath);
                path.Content = imgPath;
                prod.Picture = b;
                pic.DataContext = null;
                pic.DataContext = prod;
            }
        }


        private void Add_Produzent_Click(object sender, RoutedEventArgs e) //Produzent auswählen
        {
            Window_Select_Object WSP = new Window_Select_Object("ListeProduzenten");
            WSP.ShowDialog();
            Produzent p = (Produzent)WSP.GetObj(); //Produzent nehmen
            if(p != null)
            {
                prod.Produzent = p;
                DataContext = null;
                DataContext = prod;
                prod.Produzent = null;
                prod.ID_Produzent = p.ID_Produzent;
            }

        }
        private void btn_download_pdf_Click(object sender, RoutedEventArgs e) //PDF download, wenn in db vorhanden. Sonst zuerst btn_create_pdf_Click
        {
            if (prod.PDF_file != null)
            {
                var v = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
                SelectFile.SavePDF(v.PDF_file, prod.Name + ".pdf");
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
            byte[] byteArrayPDF = pdf.CreateFromProd(prod);
            var produkt = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            produkt.PDF_file = byteArrayPDF;
            ctx.SaveChanges();
            WM = new Window_Messagebox($"Eine PDF - Datei wurde erstellt:\n{pdf.GetPath()}");
            WM.Show();
        }
        private async void btn_show_pdf_Click(object sender, RoutedEventArgs e) 
        {
            var produkt = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            if (produkt.PDF_file != null)
            {
                string tempfile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Moin.pdf"; //temporäre Datei wird erstellt
                File.WriteAllBytes(tempfile, prod.PDF_file);
                WPDF = new Window_PDF_Viewer(tempfile);
                WPDF.ShowDialog();
                await Timer(1000);      //PDF noch nicht freigegeben, deshalb timer
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

            var img = SelectFile.SelectImgfromClipboard();
            if (img != null)
            {
                var v = Imageconverter.ConvertImageFromClipboard(img);
                prod.Picture = v;
                pic.DataContext = null;
                pic.DataContext = prod;
            }
        }
        private void MoveNext_Click(object sender, RoutedEventArgs e) //Nächstes Obj
        {          
            collectionView.MoveCurrentToNext();
            if (collectionView.IsCurrentAfterLast)
            {
                collectionView.MoveCurrentToFirst();
            }
            prod = (Produkt)collectionView.CurrentItem;
            prod = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            DataContext = prod;
        }

        private void MovePrev_Click(object sender, RoutedEventArgs e) //Vorheriges Obj
        {
            collectionView.MoveCurrentToPrevious();
            if (collectionView.IsCurrentBeforeFirst)
            {
                collectionView.MoveCurrentToLast();
            }
            prod = (Produkt)collectionView.CurrentItem;
            prod = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            DataContext = prod;
        }

        private void Button_Click_BildEntfernen(object sender, RoutedEventArgs e)
        {
            prod.Picture = null;
            pic.DataContext = null;
        }

        private void WebSuche_Click(object sender, RoutedEventArgs e)
        {
            Process.Start($"https://google.com/search?q={prod.Name}+{prod.Produzent.Name}");
        }
    }
}

