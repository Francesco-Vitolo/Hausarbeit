using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Add_Product.xaml
    /// </summary>
    public partial class Window_Add_Product : Window
    {
        private Vinothek ctx = new Vinothek();
        private Produkt newProd = new Produkt();
        public Window_Add_Product()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ctx.Produkt.Load();
            ctx.Produzent.Load();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var v = test.GetTbs();
            newProd.Name = v[0].Text;
            newProd.Art = v[1].Text;
            newProd.Qualitätssiegel = v[2].Text;
            newProd.Jahrgang = int.Parse(v[3].Text);
            newProd.Rebsorten = v[4].Text;
            newProd.Geschmack = v[6].Text;
            newProd.Alkoholgehalt = int.Parse(v[7].Text);
            //Prod.Name --> Prod.ID
            List<Produzent> list = ctx.Produzent.Local.ToList();
            Produzent pp = list.FirstOrDefault(x => x.Name == v[5].Text);
            newProd.Produzent =pp;
            ctx.Produkt.Add(newProd);
            ctx.SaveChanges();
            //close window auto
        }
    }
}
