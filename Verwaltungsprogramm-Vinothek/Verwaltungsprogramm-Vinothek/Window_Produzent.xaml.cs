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
            data = CreateDataGrid.Produkt(data);
            ctx.Produkt.Load();
            Produkte = ctx.Produkt.Where(x => x.Produzent.ID_Produzent == produzent.ID_Produzent).ToList();
            collectionView = CollectionViewSource.GetDefaultView(Produkte);
            DataContext = collectionView;
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ItemInfos.Show(collectionView.CurrentItem, "ListeProdukte");
        }
    }
}