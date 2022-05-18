﻿using System.Windows;
using System.Windows.Controls;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Abfrage.xaml
    /// </summary>
    public partial class Window_Abfrage : Window
    {
        private bool option { get; set; } = false;
        public Window_Abfrage(string text)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            ResizeMode = ResizeMode.NoResize;
            TextBlock t = new TextBlock() { Text = text, HorizontalAlignment = HorizontalAlignment.Center, TextWrapping = TextWrapping.Wrap };
            Text.Children.Add(t);
            btn_ja.Focus();
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
