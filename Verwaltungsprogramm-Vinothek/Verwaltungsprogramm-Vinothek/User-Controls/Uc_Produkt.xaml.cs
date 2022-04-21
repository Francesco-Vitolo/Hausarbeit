using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Uc_Produkt.xaml
    /// </summary>
    public partial class Uc_Produkt : UserControl
    {
        private List<TextBox> tbs = new List<TextBox>();
        public Uc_Produkt()
        {
            InitializeComponent();            
            foreach (var v in stackpanel.Children)
            {
                if (v.GetType() == typeof(TextBox))
                {
                    tbs.Add((TextBox)v);
                }
            }
        }
        public List<TextBox> GetTbs()
        {
            return tbs;
        }

        public TextBox GetDesc()
        {
            return tb_desc;
        }

        public TextBox GetProd()
        {
            return tb_prod;
        }

    }
}
