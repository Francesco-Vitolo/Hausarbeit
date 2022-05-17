using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Logins.xaml
    /// </summary>
    public partial class Page_Logins : Page
    {
        public Page_Logins()
        {
            InitializeComponent();
            VinothekContext ctx = ContextHelper.GetContext();
            datagrid.DataContext = ctx.Logins.Local.Reverse();
        }
    }
}
