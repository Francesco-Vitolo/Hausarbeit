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

namespace Verwaltungsprogramm_Vinothek.Windows
{
    /// <summary>
    /// Interaktionslogik für Window_Add_Veranstaltung_Add_Produkt.xaml
    /// </summary>
    public partial class Window_Add_Veranstaltung_Add_Produkt : Window
    {
        private Vinothek ctx = new Vinothek();
        private Produkt p = new Produkt();
        private ICollectionView collectionView;


        public Window_Add_Veranstaltung_Add_Produkt()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            WindowState = WindowState.Normal;
            CreateDataGrid.Produkt(data);
            ctx.Produkt.Load();
            collectionView = CollectionViewSource.GetDefaultView(ctx.Produkt.Local);
            DataContext = collectionView;
        }

        private void SelectItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            p = (Produkt)data.SelectedItem;
            Close();
        }

        public Produkt GetObj()
        {
            return p;
        }
    }
}
