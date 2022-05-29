using System;
using System.Windows;

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
            var uri = new Uri(path);    //Muss umgewnadelt werden, weil Fehler falls Umlaut
            pdfWebViewer.Navigate(uri);
            Focus();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            pdfWebViewer.Navigate("about:blank");
            Close();
        }
    }
}
