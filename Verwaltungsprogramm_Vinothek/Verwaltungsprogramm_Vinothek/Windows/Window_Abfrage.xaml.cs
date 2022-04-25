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
    /// Interaktionslogik für Window_Abfrage.xaml
    /// </summary>
    public partial class Window_Abfrage : Window
    {
        bool option;
        public Window_Abfrage(string text)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            ResizeMode = ResizeMode.NoResize;
            TextBlock t = new TextBlock() { Text = text, HorizontalAlignment = HorizontalAlignment.Center, TextWrapping = TextWrapping.Wrap };
            Text.Children.Add(t);
        }
        private void Button_Click_Ja(object sender, RoutedEventArgs e)
        {
            option = true;
            Close();
        }

        private void Button_Click_Nein(object sender, RoutedEventArgs e)
        {
            option = false;
            Close();
        }

        public bool GetOption()
        {
            return option;
        }
    }
}
