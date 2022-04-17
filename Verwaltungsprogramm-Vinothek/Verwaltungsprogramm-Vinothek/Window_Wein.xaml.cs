using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für Window_Wein.xaml
    /// </summary>
    public partial class Window_Wein : Window
    {
        private Produkt prod;
        public Window_Wein(Produkt p)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            Background = Brushes.Gray;
            prod = p;
            DataContext = prod;
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
    }
}
