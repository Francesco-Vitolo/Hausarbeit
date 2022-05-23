using System.ComponentModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
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
                CreateDataGrid.Produkt(ref data);
                Ctx.Produkt.Load();
                Collection = CollectionViewSource.GetDefaultView(Ctx.Produkt.Local);
            }
            else if (gridType == "ListeProduzenten")
            {
                CreateDataGrid.Produzent(ref data);
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