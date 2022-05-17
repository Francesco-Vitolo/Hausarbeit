using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_User.xaml
    /// </summary>
    public partial class Page_User : Page
    {
        private VinothekContext ctx;
        private Benutzer user;
        private Window_Messagebox WM;
        public Page_User()
        {
            InitializeComponent();
            ctx = ContextHelper.GetContext();
            DataContext = ctx.Benutzer.Local;
        }  

        private void btn_changePW_Click(object sender, RoutedEventArgs e) //Passwort ändern
        {
           user = (Benutzer)lv_Users.SelectedItem;
           user.Passwort = Encrypt.getHash(tb_pw.Text);
           ctx.SaveChanges();
           WM = new Window_Messagebox("Das Passwort wurde geändert");
           WM.Show();
        }

        private void btn_delUser_Click(object sender, RoutedEventArgs e) //User wird gelöscht (außer admin)
        {
            Benutzer user = (Benutzer)lv_Users.SelectedItem;
            if (user.ID_Benutzer != 1)
            {
                Window_Abfrage WA = new Window_Abfrage($"Soll {user.username} gelöscht werden?");
                WA.ShowDialog();
                if (WA.GetOption())
                {
                    ctx.Benutzer.Remove(user);
                    ctx.SaveChanges();
                    WM = new Window_Messagebox($"{user.username} wurde gelöscht");
                    WM.Show();
                }
            }
            else
                MessageBox.Show("Admin kann nicht gelöscht werden");
        }

        private void btn_addUser_Click(object sender, RoutedEventArgs e) //neuen Nutzer anlegen
        {
            Benutzer newUser = new Benutzer();
            newUser.username = tb_newUser_Name.Text;
            newUser.Passwort = Encrypt.getHash(tb_newUser_PW.Text);
            ctx.Benutzer.Add(newUser);
            ctx.SaveChanges();
        }
    }
}
