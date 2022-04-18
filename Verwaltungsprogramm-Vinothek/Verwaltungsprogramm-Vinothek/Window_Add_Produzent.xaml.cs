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
using System.Windows.Shapes;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Add_Produzent.xaml
    /// </summary>
    public partial class Window_Add_Produzent : Window
    {
        public Window_Add_Produzent()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
        }
    }
}
