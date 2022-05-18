using System.Collections.Generic;
using System.Windows.Controls;

namespace Verwaltungsprogramm_Vinothek.User_Controls
{
    /// <summary>
    /// Interaktionslogik für Uc_Produzent.xaml
    /// </summary>
    public partial class Uc_Produzent : UserControl
    {
        private List<TextBox> Textboxen { get; } = new List<TextBox>();
        public Uc_Produzent()
        {
            InitializeComponent();
            foreach (var v in stackpanel.Children)
            {
                if (v.GetType() == typeof(TextBox))
                {
                    Textboxen.Add((TextBox)v);
                }
            }
        }
        public List<TextBox> GetTbs()
        {
            return Textboxen;
        }
    }
}
