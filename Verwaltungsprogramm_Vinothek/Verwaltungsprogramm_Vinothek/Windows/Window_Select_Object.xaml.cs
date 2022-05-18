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
    /// Interaktionslogik für Window_Select_Object.xaml
    /// </summary>
    public partial class Window_Select_Object : Window
    {
        private VinothekContext Ctx { get; }
        private ICollectionView Collection { get; }
        private string GridType { get; }
        private object Obj { get; set; } = null;
        public Window_Select_Object(string gridType)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            Ctx = ContextHelper.GetContext();
            this.GridType = gridType;
            if (gridType == "ListeProdukte")
            {
                data = CreateDataGrid.Produkt(data);
                Ctx.Produkt.Load();
                Collection = CollectionViewSource.GetDefaultView(Ctx.Produkt.Local);
            }
            else if (gridType == "ListeProduzenten")
            {
                data = CreateDataGrid.Produzent(data);
                Ctx.Produzent.Load();
                Collection = CollectionViewSource.GetDefaultView(Ctx.Produzent.Local);
            }
            DataContext = Collection;
        }

        private void SelectItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Obj = data.SelectedItem;
            Close();
        }

        public object GetObj()
        {
            return Obj;
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string filterStr = tbSearch.Text.ToLower();
            switch (GridType)
            {
                case "ListeProdukte":
                    Collection.Filter = x => ((Produkt)x).Name.ToLower().Contains(filterStr);
                    break;
                case "ListeProduzenten":
                    Collection.Filter = x => ((Produzent)x).Name.ToLower().Contains(filterStr);
                    break;
                case "ListeEvents":
                    Collection.Filter = x => ((Event)x).Name.ToLower().Contains(filterStr);
                    break;
            }
        }
    }
}