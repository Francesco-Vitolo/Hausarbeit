using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Verwaltungsprogramm_Vinothek.Properties;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Anmelden.xaml
    /// </summary>
    public partial class Window_Anmelden : Window
    {
        VinothekContext ctx = new VinothekContext();
        Benutzer user;
        public Window_Anmelden()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            SetButtonFarbe();
            SetDirectory();
            Demo();
            tb_username.Focus();
        }

        private void SetDirectory()
        {
            if (Settings.Default.PDF_Directory == null)
                Settings.Default.PDF_Directory = Environment.SpecialFolder.MyDocuments.ToString();
        }

        private void SetButtonFarbe()
        {
            try
            {
                System.Windows.Media.Color brush1 = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(Settings.Default.Color1.ToString());
                System.Windows.Media.Color brush2 = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(Settings.Default.Color2.ToString());
                LinearGradientBrush gradient = new LinearGradientBrush()
                {
                    StartPoint = new System.Windows.Point(0, 0),
                    EndPoint = new System.Windows.Point(1, 1),
                };
                gradient.GradientStops.Add(new GradientStop(brush1, 0));
                gradient.GradientStops.Add(new GradientStop(brush2, 0.8));
                Settings.Default.LinearGradientBrush = gradient;
                Settings.Default.Save();
            }
            catch { }
        }

        private void Demo()
        {
            tb_username.Text = "admin";
            tb_pw.Password = "admin";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string username = tb_username.Text;
            string pw = Encrypt.getHash(tb_pw.Password);
            ctx.Benutzer.Load();           
            if (ctx.Benutzer.Any(x => x.username == username && x.Passwort == pw))
            {
                user = ctx.Benutzer.FirstOrDefault(x => x.username == username && x.Passwort == pw);
                Logins login = new Logins();
                login.ID_Benutzer = user.ID_Benutzer;
                login.Datum = DateTime.Now;
                ctx.Logins.Add(login);
                ctx.SaveChanges();
                MainWindow main = new MainWindow(user);
                main.Show();
                Close();
            }
            else
                Alert.Visibility = Visibility.Visible;
        }

        private void Window_KeyUp_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(null,null);
        }
    }
}
