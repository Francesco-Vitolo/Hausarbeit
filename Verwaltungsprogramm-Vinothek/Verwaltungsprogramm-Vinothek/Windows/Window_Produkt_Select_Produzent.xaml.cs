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
    /// Interaktionslogik für Window_Produkt_Select_Produzent.xaml
    /// </summary>
    public partial class Window_Produkt_Select_Produzent : Window
    {
        private Vinothek ctx = new Vinothek();
        private Produzent p = new Produzent();
        private ICollectionView collectionView;
        public Window_Produkt_Select_Produzent()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            WindowState = WindowState.Normal;
            CreateDataGrid.Produzent(data);
            ctx.Produzent.Load();
            collectionView = CollectionViewSource.GetDefaultView(ctx.Produzent.Local);
            DataContext = collectionView;
        }

        private void SelectItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            p = (Produzent)data.SelectedItem;
            Close();
        }

        public Produzent GetObj()
        {
            return p;
        }
    }
}
