using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        VinothekContext ctx = new VinothekContext();
        List<Produkt> Produkte = new List<Produkt>();
        Produzent p;
        public Page_Produzent(Produzent produzent)
        {
            InitializeComponent();
            CreateDataGrid.Produkt(data); //Datagrid erstellen
            ctx.Produzent.Load();
            p = ctx.Produzent.Find(produzent.ID_Produzent); //selbe wie ...FirstOrDefault(x => x.Produzent == produzent.ID_Produzent)
            prodz.DataContext = p;
            ctx.Produkt.Load();
            Produkte = ctx.Produkt.Where(x => x.Produzent.ID_Produzent == produzent.ID_Produzent).ToList(); //Produkte, die zu Produzent gehören
            data.DataContext = Produkte;
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Produkt selected_produzent = (Produkt)data.CurrentItem;
            if(selected_produzent != null)
                NavigationService.Navigate(new Page_Produkt(selected_produzent,new System.ComponentModel.SortDescription("Name",0)));
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
            string OpenMailProg = "Mailto:" + p.Email; //?subject= "Betreff"&body=\"Inhalt\"
            Process proc = new Process();
            proc.StartInfo.FileName = OpenMailProg;
            proc.Start();
        }
    }
}