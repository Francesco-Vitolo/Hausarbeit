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
            Style = FindResource("Window_Default") as Style;
            Background = Brushes.Gray;
            prod = p;
            ctx.Produkt.Load();
            DataContext = ctx.Produkt.Local;
            felder.DataContext = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            ctx.Produzent.Load();
            //var list = ctx.Produzent.ToList();
            //foreach (var i in list)
            //{
            //    ComboBoxItem CBI = new ComboBoxItem();
            //    CBI.Content = i.Name;
            //    v.Items.Add(CBI);
            //}
        }

        private void btn_show_pdf_Click(object sender, RoutedEventArgs e)
        {
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(@"C:\Users\Francesco\Desktop\Docs\Moin_12_1.pdf");
            WPDF.ShowDialog();
        }

        private void UmschaltenBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (felder.IsEnabled == false)
            {
                felder.IsEnabled = true;
                Background = Brushes.White;
            }
            else
            {
                felder.IsEnabled = false;
                Background = Brushes.Gray;
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
            Close();
            Application.Current.MainWindow.Show();
        }
    }
}
