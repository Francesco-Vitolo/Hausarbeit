using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Kundensicht.xaml
    /// </summary>
    public partial class Page_Kundensicht : Page
    {
        List<string> Tempfiles { get; set; } //Liste, falls User zu schnell ist --> Datei noch nicht gelöscht
        Random Random { get; }
        public Page_Kundensicht()
        {
            InitializeComponent();
            Random = new Random();
            Tempfiles = new List<string>();
            VinothekContext ctx = ContextHelper.GetContext();
            var MainWindow = Application.Current.Windows.OfType<MainWindow>().LastOrDefault();
            MainWindow.GoBack.Visibility = Visibility.Hidden;    //Button_zurück und Menü unsichtbar machen
            MainWindow.expander.Visibility = Visibility.Hidden;

            var listProdukte = ctx.Produkt.Where(
                x => x.Aktiv == true
                && x.Picture != null
                && x.PDF_file != null).ToList(); //Wenn Bild vorhanden und Produkt aktiv ist

            foreach (var prod in listProdukte)
            {
                System.Drawing.Image img = ImageConverter.BinaryToImage(prod.Picture);
                System.Drawing.Bitmap bitmapImg = new System.Drawing.Bitmap(img);
                AddImgToGrid(bitmapImg, prod);
            }
        }

        private void AddImgToGrid(System.Drawing.Bitmap bmap, Produkt prod)
        {
            Button btn = new Button();
            Image img = new Image();
            MemoryStream memorystream = new MemoryStream(); //Für System.Windows.Constrols.Image
            bmap.Save(memorystream, ImageFormat.Png);
            memorystream.Position = 0;
            BitmapImage bitmapImg = new BitmapImage();
            bitmapImg.BeginInit();
            bitmapImg.StreamSource = memorystream;
            bitmapImg.EndInit();
            img.Source = bitmapImg;  //Für System.Windows.Constrols.Image
            btn.Content = img;
            btn.Style = FindResource("Button_Kunden") as Style;
            btn.Click += (sender, args) =>      //Event für Button wird erzeugt
            {
                expanderInfos.DataContext = prod; //Infos zum Produkt im Expander
                expanderInfos.Visibility = Visibility.Visible;
                expanderInfos.IsExpanded = true;

                Tempfiles.Add( $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Moin_{Random.Next(0,1000)}.pdf"); //temporäre Datei wird erstellt
                string tempfile = Tempfiles.LastOrDefault();
                File.WriteAllBytes(tempfile, prod.PDF_file);
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
            File.Delete(Tempfiles.FirstOrDefault()); //löschen
        }

        private Task Timer(int i)
        {
            return Task.Run(() => { Thread.Sleep(i); });
        }

    }
}
