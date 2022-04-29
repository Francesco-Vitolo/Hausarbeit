﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;


namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Anmelden.xaml
    /// </summary>
    public partial class Window_Anmelden : Window
    {
        VinothekContext ctx = new VinothekContext();
        public Window_Anmelden()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = tb_username.Text;
            string pw = Encrypt.getHash(tb_pw.Password);
            ctx.Benutzer.Load();           
            if (ctx.Benutzer.Any(x => x.username == username && x.Passwort == pw))
            {
                MainWindow main = new MainWindow();
                main.Show();
                Close();
            }
            else
                Alert.Visibility = Visibility.Visible;
        }

        private void Window_KeyUp_Enter(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                Button_Click(null,null);
        }

        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    string username = tb_username.Text;
        //    string pw = tb_pw.Password;
        //    ctx.Benutzer.Load();
        //    Benutzer b = new Benutzer();
        //    b.username = username;
        //    b.Passwort = Encrypt.getHash(pw);
        //    b.Salt = "aa";
        //    ctx.Benutzer.Add(b);
        //    ctx.SaveChanges();
        //}

    }
}
