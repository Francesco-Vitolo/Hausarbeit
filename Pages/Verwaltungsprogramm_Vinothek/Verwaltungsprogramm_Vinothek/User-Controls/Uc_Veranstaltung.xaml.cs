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

namespace Verwaltungsprogramm_Vinothek.User_Controls
{
    /// <summary>
    /// Interaktionslogik für Uc_Veranstaltung.xaml
    /// </summary>
    public partial class Uc_Veranstaltung : UserControl
    {
        private List<TextBox> listObj = new List<TextBox>();
        public Uc_Veranstaltung()
        {
            InitializeComponent();
            foreach (var v in stackpanel.Children)
            {
                if (v.GetType() == typeof(TextBox))
                {
                    listObj.Add((TextBox)v);
                }
            }
        }

        public List<TextBox> GetTbs()
        {
            return listObj;
        }

        public DatePicker GetDP()
        {
            return datepicker;
        }
    }
}
