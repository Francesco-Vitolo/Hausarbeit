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
        private VinothekContext ctx;
        private object obj = null;
        private string gridType;
        private ICollectionView collection;
        public Window_Select_Object(string gridType)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            ctx = ContextHelper.GetContext();
            this.gridType = gridType;
            if (gridType == "ListeProdukte")
            {
                CreateDataGrid.Produkt(data);
                ctx.Produkt.Load();
                collection = CollectionViewSource.GetDefaultView(ctx.Produkt.Local);
            }
            else if (gridType == "ListeProduzenten")
            {
                CreateDataGrid.Produzent(data);
                ctx.Produzent.Load();
                collection = CollectionViewSource.GetDefaultView(ctx.Produzent.Local);
            }
            DataContext = collection;
        }

        private void SelectItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            obj = data.SelectedItem;
            Close();
        }

        public object GetObj()
        {
            return obj;
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string filterStr = tbSearch.Text.ToLower();
            switch (gridType)
            {
                case "ListeProdukte":
                    collection.Filter = x => ((Produkt)x).Name.ToLower().Contains(filterStr);
                    break;
                case "ListeProduzenten":
                    collection.Filter = x => ((Produzent)x).Name.ToLower().Contains(filterStr);
                    break;
                case "ListeEvents":
                    collection.Filter = x => ((Event)x).Name.ToLower().Contains(filterStr);
                    break;
            }
        }
    }
}