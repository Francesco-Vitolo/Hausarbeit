using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_User.xaml
    /// </summary>
    public partial class Page_User : Page
    {
        private VinothekContext ctx = new VinothekContext();
        private Benutzer user;
        public Page_User()
        {
            InitializeComponent();
            ctx.Benutzer.Load();
            DataContext = ctx.Benutzer.Local;

        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Benutzer user = (Benutzer)lv_Users.SelectedItem;
            this.user = user;
        }

        private void btn_changePW_Click(object sender, RoutedEventArgs e)
        {
           Window_Anmelden WinAnmelden = new Window_Anmelden();
           WinAnmelden.ShowDialog();
           user.Passwort = Encrypt.getHash(tb_pw.Text);
           ctx.SaveChanges();
        }

        private void btn_delUser_Click(object sender, RoutedEventArgs e)
        {
            Benutzer user = (Benutzer)lv_Users.SelectedItem;
            if (user.username != "admin")
                ctx.Benutzer.Remove(user);
            else
                MessageBox.Show("geht nicht mfka"); 
        }

        private void btn_addUser_Click(object sender, RoutedEventArgs e)
        {
            Benutzer newUser = new Benutzer();
            newUser.username = tb_newUser_Name.Text;
            newUser.Passwort = Encrypt.getHash(tb_newUser_PW.Text);
            newUser.Salt = null;
            ctx.Benutzer.Add(newUser);
            ctx.SaveChanges();
        }
    }
}
