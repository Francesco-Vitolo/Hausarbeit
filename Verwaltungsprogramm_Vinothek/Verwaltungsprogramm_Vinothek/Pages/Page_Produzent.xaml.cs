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
        private VinothekContext Ctx { get; set; }
        private List<Produkt> Produkte { get; set; }
        private Produzent Produzent { get; set; }
        private ICollectionView CollectionView { get; set; }

        public Page_Produzent(Produzent produzent)
        {
            InitializeComponent();
            Ctx = ContextHelper.GetContext();
            dataGridProdukte = CreateDataGrid.Produkt(dataGridProdukte); //Datagrid erstellen
            Produkte = new List<Produkt>();
            Produzent = produzent;
            prodz.DataContext = Produzent;
            Produkte = Ctx.Produkt.Where(x => x.Produzent.ID_Produzent == Produzent.ID_Produzent).ToList(); //Produkte, die zu Produzent gehören
            CollectionView = CollectionViewSource.GetDefaultView(Produkte.OrderBy(x => x.Name).ToList());
            dataGridProdukte.DataContext = CollectionView;
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Produkt selected_produkt = (Produkt)dataGridProdukte.CurrentItem;
            if (selected_produkt != null)
                NavigationService.Navigate(new Page_Produkt(CollectionView));
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
            Ctx.SaveChanges();
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
            Ctx = ContextHelper.GetContext();
            Produkte = Ctx.Produkt.Where(x => x.Produzent.ID_Produzent == Produzent.ID_Produzent).ToList(); //Produkte, die zu Produzent gehören
            Produkte = Produkte.OrderBy(x => x.Name).ToList();
            CollectionView = CollectionViewSource.GetDefaultView(Produkte);
            dataGridProdukte.DataContext = CollectionView;
        }
    }
}