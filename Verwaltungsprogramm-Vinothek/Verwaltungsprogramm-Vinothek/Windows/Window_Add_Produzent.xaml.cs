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
    /// Interaktionslogik für Window_Add_Produzent.xaml
    /// </summary>
    public partial class Window_Add_Produzent : Window
    {
        private Vinothek ctx = new Vinothek();
        public Window_Add_Produzent()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Produzent produzent = new Produzent();
            var tbs = felder.GetTbs();
            if (tbs[0].Text != "")
            {
                produzent.Name = tbs[0].Text;
                produzent.Land = tbs[1].Text;
                produzent.Region = tbs[2].Text;
                if (int.TryParse(tbs[3].Text, out int i))
                    produzent.Hektar = i;
                produzent.Adresse = tbs[4].Text;
                produzent.Beschreibung = tbs[5].Text;
                ctx.Produzent.Add(produzent);
                ctx.SaveChanges();
            }
            else
            {
                MessageBox.Show("Name eingeben");
            }
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
