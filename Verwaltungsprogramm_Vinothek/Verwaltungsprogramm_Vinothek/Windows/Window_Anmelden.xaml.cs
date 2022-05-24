using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
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
        private VinothekContext Ctx { get; set; }
        private Benutzer User { get; set; }
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
            Ctx = ContextHelper.GetContext();
        }
        private void SetDirectory()
        {
            if (Settings.Default.PDF_Directory == null)
                Settings.Default.PDF_Directory = Environment.SpecialFolder.MyDocuments.ToString();
            Settings.Default.Save();
        }

        private void SetButtonFarbe()
        {
            Color brush1 = (Color)ColorConverter.ConvertFromString(Settings.Default.Color1.ToString());
            Color brush2 = (Color)ColorConverter.ConvertFromString(Settings.Default.Color2.ToString());
            LinearGradientBrush gradient = new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
            };
            gradient.GradientStops.Add(new GradientStop(brush1, 0));
            gradient.GradientStops.Add(new GradientStop(brush2, 0.8));
            Settings.Default.LinearGradientBrush = gradient;
            Settings.Default.Save();
        }

        private void Button_Click_Anmelden(object sender, RoutedEventArgs e)
        {
            string username = tb_username.Text;
            string pw = Encrypt.GetHash(tb_pw.Password);
            if (Ctx.Benutzer.Any(x => x.username == username && x.Passwort == pw))
            {
                //ProgressBar();
                btn_Anmelden.IsEnabled = false;
                User = Ctx.Benutzer.FirstOrDefault(x => x.username == username && x.Passwort == pw);
                Logins login = new Logins();
                login.ID_Benutzer = User.ID_Benutzer;
                login.Datum = DateTime.Now;
                Ctx.Logins.Add(login);
                Ctx.SaveChanges();
                MainWindow mainWindow = new MainWindow(User);
                mainWindow.Show();
                Close();
            }
            else
                Alert.Visibility = Visibility.Visible;
        }
        private void Window_KeyUp_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click_Anmelden(null, null);
        }

        //private async void ProgressBar()
        //{
        //    await ProgressBarTask();
           
        //}
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
