using System;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Kundensicht.xaml
    /// </summary>
    public partial class Page_Kundensicht : Page
    {
        public Page_Kundensicht()
        {
            InitializeComponent();
            VinothekContext ctx = new VinothekContext();
            ctx.Produkt.Load();
            var v  = ctx.Produkt.ToList();      
            foreach (var vv in v)
            {
                if (vv.Picture != null)
                {
                    System.Drawing.Image img = Imageconverter.BinaryToImage(vv.Picture);
                    System.Drawing.Bitmap b = new System.Drawing.Bitmap(img);
                    moin(b, vv);
                }
            }
        }
        private void moin(System.Drawing.Bitmap b, Produkt p)
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
            btn.Click += async (sender, args) =>
            {
                if (p.PDF_file != null)
                {
                    string tempfile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Moin.pdf";
                    File.WriteAllBytes(tempfile, p.PDF_file);
                    Window_PDF_Viewer WPDF = new Window_PDF_Viewer(tempfile);
                    WPDF = new Window_PDF_Viewer(tempfile);
                    WPDF.ShowDialog();
                    await Timer(1000);
                    //File.Delete(tempfile); 
                }
            };
            maingrid.Children.Add(btn);
        }

        private Task Timer(int i)
        {
            return Task.Run(() => { Thread.Sleep(i); });
        }
    }
}
