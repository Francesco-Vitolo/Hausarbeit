using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    /// Interaktionslogik für Window_PDF_Viewer.xaml
    /// </summary>
    public partial class Window_PDF_Viewer : Window
    {
        public Window_PDF_Viewer(string path)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            pdfWebViewer.Navigate(path);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            pdfWebViewer.Navigate("about:blank");
            Close();
        }
    }
}
