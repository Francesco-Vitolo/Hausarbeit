using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Produkt.xaml
    /// </summary>
    public partial class Window_Produkt : Window
    {
        private Produkt prod;
        private Vinothek ctx = new Vinothek();
        public Window_Produkt(Produkt p)
        {
            InitializeComponent();
            ctx.Produkt.Load();
            ctx.Produzent.Load();            
            prod = p;
            Style = FindResource("Window_Default") as Style;
            prod = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            DataContext = prod;
        }

        private void btn_show_pdf_Click(object sender, RoutedEventArgs e)
        {
            //Ändern in byte[], weil in DB gespeichert
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(@"C:\Users\Francesco\Desktop\Docs\Moin_12_1.pdf");
            WPDF.ShowDialog();
        }

        private void UmschaltenBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (felder.IsEnabled == false)
            {
                felder.IsEnabled = true;
                modus.Text = "anschauen";
            }
            else
            {
                felder.IsEnabled = false;
                modus.Text = "bearbeiten";
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
            Close();
            Application.Current.MainWindow.Show();
        }

        private void Button_Click_BildAuswählen(object sender, RoutedEventArgs e)
        {
            string imgPath = SelectFile.Image();

            if (imgPath != null)
            {
                byte[] b = ImageConverter.ConvertImageToByteArray(imgPath);
                path.Content = imgPath;
                prod.Picture = b;
                pic.DataContext = null;
                pic.DataContext = prod;
            }

        }

        private void Button_Click_PDF(object sender, RoutedEventArgs e)
        {
            PDF.Create(prod);
        }
    }
}
