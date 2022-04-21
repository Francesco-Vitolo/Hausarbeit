using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Page_Produzent.xaml
    /// </summary>
    public partial class Page_Produzent : Page
    {
        VinothekContext ctx = new VinothekContext();
        List<Produkt> Produkte = new List<Produkt>();
        public Page_Produzent(Produzent produzent)
        {
            InitializeComponent();
            CreateDataGrid.Produkt(data);
            ctx.Produzent.Load();
            Produzent p = ctx.Produzent.Find(produzent.ID_Produzent);
            prodz.DataContext = p;
            ctx.Produkt.Load();
            Produkte = ctx.Produkt.Where(x => x.Produzent.ID_Produzent == produzent.ID_Produzent).ToList();
            data.DataContext = Produkte;
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ItemInfos.Show(data.CurrentItem, "ListeProdukte");
        }

        private void UmschaltenBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (prodz.IsEnabled == false)
            {
                prodz.IsEnabled = true;
                modus.Text = "bearbeiten";
            }
            else
            {
                prodz.IsEnabled = false;
                modus.Text = "anschauen";
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
            NavigationService.GoBack();
        }
    }
}