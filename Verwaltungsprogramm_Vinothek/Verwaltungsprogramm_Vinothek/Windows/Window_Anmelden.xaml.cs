using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Verwaltungsprogramm_Vinothek.Properties;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Anmelden.xaml
    /// </summary>
    public partial class Window_Anmelden : Window
    {
        private VinothekContext ctx;
        private Benutzer user;
        public double Progress
        {
            get { return progressBar.Value; }
            set { progressBar.Value = value; }
        }
        public Window_Anmelden()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            InitializeContext();
            SetButtonFarbe();
            SetDirectory();
            Demo();
            tb_username.Focus();
        }
        private void Demo()
        {
            tb_username.Text = "admin";
            tb_pw.Password = "admin";
        }

        private void InitializeContext()
        {
            ContextHelper.SetNewContext();
            ContextHelper.LoadTables();
            ctx = ContextHelper.GetContext();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = tb_username.Text;
            string pw = Encrypt.getHash(tb_pw.Password);
            if (ctx.Benutzer.Any(x => x.username == username && x.Passwort == pw))
            {
                ProgressBar();
                btn_Anmelden.IsEnabled = false;
                user = ctx.Benutzer.FirstOrDefault(x => x.username == username && x.Passwort == pw);
                Logins login = new Logins();
                login.ID_Benutzer = user.ID_Benutzer;
                login.Datum = DateTime.Now;
                ctx.Logins.Add(login);
                ctx.SaveChanges();
            }
            else
                Alert.Visibility = Visibility.Visible;
        }
        private void Window_KeyUp_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(null, null);
        }

        private async void ProgressBar()
        {
            await ProgressBarTask();
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            Close();
        }
        private Task ProgressBarTask()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    System.Threading.Thread.Sleep(2);
                    Dispatcher.Invoke(() => Progress = i);
                }
            });
        }
    }
}
