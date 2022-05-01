using System;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            MainW.GoBack.Visibility = Visibility.Hidden;
            MainW.expander.Visibility = Visibility.Hidden;
            ctx.Produkt.Load();
            var v = ctx.Produkt.ToList();
            foreach (var vv in v)
            {
                if (vv.Picture != null)
                {
                    System.Drawing.Image img = Imageconverter.BinaryToImage(vv.Picture);
                    System.Drawing.Bitmap b = new System.Drawing.Bitmap(img);
                    AddImgToGrid(b, vv);
                }
            }
        }
        private void AddImgToGrid(System.Drawing.Bitmap b, Produkt p)
        {
            Button btn = new Button();
            Image i = new Image();
            MemoryStream ms = new MemoryStream();
            b.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            i.Source = bi;
            btn.Content = i;
            btn.Click += (sender, args) =>
            {
                this.p = p;
                Infos.DataContext = p;
                if (p.PDF_file == null)
                {
                    pdf.IsEnabled = false;
                }
                else
                    pdf.IsEnabled = true;
            };
            maingrid.Children.Add(btn);
        }
        private async void ShowPDF(Produkt p)
        {
            string tempfile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Moin.pdf";
            File.WriteAllBytes(tempfile, p.PDF_file);
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(tempfile);
            WPDF = new Window_PDF_Viewer(tempfile);
            WPDF.ShowDialog();
            await Timer(5000);
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
