using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Produzent.xaml
    /// </summary>
    public partial class Window_Produzent : Window
    {
        Vinothek ctx = new Vinothek();
        ICollectionView collectionView;
        List<Produkt> Produkte = new List<Produkt>();
        public Window_Produzent(Produzent produzent)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            CreateDataGrid.Produkt(data);
            ctx.Produzent.Load();
            Produzent p = ctx.Produzent.Find(produzent.ID_Produzent);
            prodz.DataContext = p;
            ctx.Produkt.Load();
            Produkte = ctx.Produkt.Where(x => x.Produzent.ID_Produzent == produzent.ID_Produzent).ToList();
            collectionView = CollectionViewSource.GetDefaultView(Produkte);
            data.DataContext = collectionView;
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ItemInfos.Show(collectionView.CurrentItem, "ListeProdukte");
        }

        private void UmschaltenBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (prodz.IsEnabled == false)
            {
                prodz.IsEnabled = true;
                Background = Brushes.White;
            }
            else
            {
                prodz.IsEnabled = false;
                Background = Brushes.Gray;
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
            Close();
            Application.Current.MainWindow.Show();
        }
    }
}