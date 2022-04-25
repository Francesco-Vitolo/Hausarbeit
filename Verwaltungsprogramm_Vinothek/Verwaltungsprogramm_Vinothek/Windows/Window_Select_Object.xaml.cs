using System;
using System.Collections.Generic;
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
        private VinothekContext ctx = new VinothekContext();
        private object obj = new object();
        public Window_Select_Object(string gridType)
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            if (gridType == "ListeProdukte")
            {
                CreateDataGrid.Produkt(data);
                ctx.Produkt.Load();
                DataContext = ctx.Produkt.Local;
            }
            else if (gridType == "ListeProduzenten")
            {
                CreateDataGrid.Produzent(data);
                ctx.Produzent.Load();
                DataContext = ctx.Produzent.Local;
            }
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
    }
}