using System;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Kundensicht.xaml
    /// </summary>
    public partial class Page_Kundensicht : Page
    {
        Produkt p;
        public Page_Kundensicht()
        {
            InitializeComponent();
            VinothekContext ctx = new VinothekContext();
            var MainW = Application.Current.Windows.OfType<MainWindow>().LastOrDefault();
            MainW.GoBack.Visibility = Visibility.Hidden;    //Button_zurück und Menü unsichtbar machen
            MainW.expander.Visibility = Visibility.Hidden;
            ctx.Produkt.Load();
            var listProdukte = ctx.Produkt.Where(x => x.Picture != null && x.Aktiv == true).ToList(); //Wenn Bild vorhanden und Produkt aktiv ist
            foreach (var prod in listProdukte)
            {
                System.Drawing.Image img = Imageconverter.BinaryToImage(prod.Picture);
                System.Drawing.Bitmap bitmapImg = new System.Drawing.Bitmap(img);
                AddImgToGrid(bitmapImg, prod);
            }
        }

        private void AddImgToGrid(System.Drawing.Bitmap b, Produkt p)
        {
            Button btn = new Button();
            Image i = new Image();
            MemoryStream ms = new MemoryStream(); //Für System.Windows.Constrols.Image
            b.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            i.Source = bi;  //Für System.Windows.Constrols.Image
            btn.Content = i;
            btn.Click += (sender, args) =>      //Event für Button wird erzeugt
            {
                this.p = p;
                expanderInfos.DataContext = p; //Infos zum Produkt im Expander
                expanderInfos.Visibility = Visibility.Visible;
                expanderInfos.IsExpanded = true;
            };
            maingrid.Children.Add(btn);
        }

        private void Page_KeyUp(object sender, KeyEventArgs e) //STRG + e --> Programm wird neu gestartet (Passwort eingeben)
        {
            if ((e.Key == Key.E) && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                Application.Current.Shutdown();
                System.Windows.Forms.Application.Restart();
            }
        }

        private void expanderInfos_Collapsed(object sender, RoutedEventArgs e) //Wenn Expander geschlossen --> unsichtbar
        {
            expanderInfos.Visibility = Visibility.Hidden;
        }
        
        private async void ShowPDF() //Option PDF anschuen (nicht im Programm drin)
        {
            string tempfile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Moin.pdf";
            File.WriteAllBytes(tempfile, p.PDF_file);
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(tempfile);
            WPDF = new Window_PDF_Viewer(tempfile);
            WPDF.ShowDialog();
            await Timer(20000);
            File.Delete(tempfile);
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string tempfile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Moin.pdf";
            File.WriteAllBytes(tempfile, p.PDF_file);
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(tempfile);
            WPDF.ShowDialog();
            await Timer(1000);
            File.Delete(tempfile);
        }
        private Task Timer(int i)
        {
            return Task.Run(() => { Thread.Sleep(i); });
        }
    }
}
