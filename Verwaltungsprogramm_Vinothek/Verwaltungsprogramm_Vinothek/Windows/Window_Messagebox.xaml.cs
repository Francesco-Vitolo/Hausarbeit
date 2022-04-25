using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Verwaltungsprogramm_Vinothek.Windows
{
    /// <summary>
    /// Interaktionslogik für Window_Messagebox.xaml
    /// </summary>
    public partial  class Window_Messagebox : Window
    {
        public Window_Messagebox(string text)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            ResizeMode = ResizeMode.NoResize;
            TextBlock t = new TextBlock() { Text = text, HorizontalAlignment = HorizontalAlignment.Center, TextWrapping = TextWrapping.Wrap };
            Text.Children.Add(t);
            CloseAuto();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void CloseAuto()
        {
            if (await Timer(3000))
                Close();
        }
        private Task<bool> Timer(int i)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(i);
                return true;
            });
        }
    }
}
