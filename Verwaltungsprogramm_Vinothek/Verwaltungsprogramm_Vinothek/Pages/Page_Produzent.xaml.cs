using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using Verwaltungsprogramm_Vinothek.Pages;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Page_Produzent.xaml
    /// </summary>
    public partial class Page_Produzent : Page
    {
        VinothekContext ctx;
        List<Produkt> Produkte = new List<Produkt>();
        Produzent Produzent;
        ICollectionView collectionView;

        public Page_Produzent(Produzent produzent)
        {
            InitializeComponent();
            ctx = ContextHelper.GetContext();
            CreateDataGrid.Produkt(data); //Datagrid erstellen
            Produzent = ctx.Produzent.Find(produzent.ID_Produzent);
            prodz.DataContext = Produzent;
            Produkte = ctx.Produkt.Where(x => x.Produzent.ID_Produzent == Produzent.ID_Produzent).ToList(); //Produkte, die zu Produzent gehören
            collectionView = CollectionViewSource.GetDefaultView(Produkte.OrderBy(x => x.Name).ToList());
            data.DataContext = collectionView;
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Produzent = ctx.Produzent.Find(Produzent.ID_Produzent);
            prodz.DataContext = Produzent;
            Produkt selected_produzent = (Produkt)data.CurrentItem;
            if (selected_produzent != null)
                NavigationService.Navigate(new Page_Produkt(collectionView));           
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

        private void Button_Click_OpenMail(object sender, RoutedEventArgs e) //Email - Programm öffnen und Empfänger wird eingetragen
        {
            string OpenMailProg = "Mailto:" + Produzent.Email; //?subject= "Betreff"&body=\"Inhalt\"
            Process proc = new Process();
            proc.StartInfo.FileName = OpenMailProg;
            proc.Start();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ContextHelper.SetNewContext();
            ctx = ContextHelper.GetContext();
            Produkte = ctx.Produkt.Where(x => x.Produzent.ID_Produzent == Produzent.ID_Produzent).ToList(); //Produkte, die zu Produzent gehören
            Produkte = Produkte.OrderBy(x => x.Name).ToList();
            collectionView = CollectionViewSource.GetDefaultView(Produkte);
            data.DataContext = collectionView;
        }
    }
}