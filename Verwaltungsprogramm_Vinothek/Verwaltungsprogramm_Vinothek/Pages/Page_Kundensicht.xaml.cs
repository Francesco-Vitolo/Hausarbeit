using System;
using System.Collections.Generic;
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
        List<string> tempfiles = new List<string>(); //Liste falls User zu schnell für Zeit bis zum löschen
        Random r;
        public Page_Kundensicht()
        {
            InitializeComponent();
            r = new Random();
            VinothekContext ctx = new VinothekContext();
            var MainW = Application.Current.Windows.OfType<MainWindow>().LastOrDefault();
            MainW.GoBack.Visibility = Visibility.Hidden;    //Button_zurück und Menü unsichtbar machen
            MainW.expander.Visibility = Visibility.Hidden;
            ctx.Produkt.Load();
            var listProdukte = ctx.Produkt.Where(x => x.Picture != null && x.Aktiv == true && x.PDF_file != null).ToList(); //Wenn Bild vorhanden und Produkt aktiv ist
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
            btn.Style = FindResource("Button_Kunden") as Style;
            btn.Click += (sender, args) =>      //Event für Button wird erzeugt
            {
                this.p = p;
                expanderInfos.DataContext = p; //Infos zum Produkt im Expander
                expanderInfos.Visibility = Visibility.Visible;
                expanderInfos.IsExpanded = true;
                tempfiles.Add( $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Moin_{r.Next(0,1000)}.pdf"); //temporäre Datei wird erstellt
                string tempfile = tempfiles.LastOrDefault();
                File.WriteAllBytes(tempfile, p.PDF_file); //PDF noch nicht freigegeben, deshalb timer
                pdfBrowser.Navigate(tempfile);
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

        private async void expanderInfos_Collapsed(object sender, RoutedEventArgs e) //Wenn Expander geschlossen --> unsichtbar
        {
            expanderInfos.Visibility = Visibility.Hidden;
            pdfBrowser.Navigate("about:blank");
            await Timer(1000);      //PDF noch nicht freigegeben, deshalb timer
            File.Delete(tempfiles.FirstOrDefault()); //löschen
        }

        private Task Timer(int i)
        {
            return Task.Run(() => { Thread.Sleep(i); });
        }

    }
}
