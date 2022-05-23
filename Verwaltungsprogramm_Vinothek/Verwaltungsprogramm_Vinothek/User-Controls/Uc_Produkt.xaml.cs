using System.Collections.Generic;
using System.Windows.Controls;

namespace Verwaltungsprogramm_Vinothek.User_Controls
{
    /// <summary>
    /// Interaktionslogik für Uc_Produkt.xaml
    /// </summary>
    public partial class Uc_Produkt : UserControl
    {
        private List<TextBox> TextBoxen = new List<TextBox>();
        public Uc_Produkt()
        {
            InitializeComponent();
            foreach (var v in stackpanel.Children)
            {
                if (v.GetType() == typeof(TextBox))
                {
                    TextBoxen.Add((TextBox)v);
                }
            }
            TextBoxen.Add(tb_alk);
            TextBoxen.Add(tb_preis);
        }
        public List<TextBox> GetTbs()
        {
            return TextBoxen;
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
