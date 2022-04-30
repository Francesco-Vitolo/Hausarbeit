using System;
using System.Collections.Generic;
using System.Globalization;
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
using Verwaltungsprogramm_Vinothek.Windows;

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
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    TimeHours.Items.Add(new ComboBoxItem() { Content = "0" + i });
                    TimeMinutes.Items.Add(new ComboBoxItem() { Content = "0" + i });
                }
                else
                {

                    TimeHours.Items.Add(new ComboBoxItem() { Content = i });
                    TimeMinutes.Items.Add(new ComboBoxItem() { Content = i });
                }

            }

            for (int i = 25; i <= 59; i++)
            {
                TimeMinutes.Items.Add(new ComboBoxItem() { Content = i });
            }
        }

        public List<TextBox> GetTbs()
        {
            return listObj;
        }

        public string GetDate()
        {
            if (datepicker.SelectedDate == null)
                datepicker.SelectedDate = DateTime.Now;
            DateTime date = (DateTime)datepicker.SelectedDate;
            return date.ToString("dddd, dd MMMM yyyy");
        }

        public string GetTime()
        {
            return tb_time.Text;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TimeHours.SelectedItem != null && TimeMinutes.SelectedItem != null)
            {
                DateTime t;
                ComboBoxItem cbiHours = (ComboBoxItem)TimeHours.SelectedItem;
                ComboBoxItem cbiMinutes = (ComboBoxItem)TimeMinutes.SelectedItem;
                string time = cbiHours.Content + ":" + cbiMinutes.Content;
                CultureInfo german = new CultureInfo("de-DE");
                t = DateTime.ParseExact(time, "HH':'mm", german);
                tb_time.Text = t.ToString("HH':'mm");
            }
            else
            {
                Window_Messagebox WM = new Window_Messagebox("Bitte beide Felder ausfüllen");
                WM.Show();
            }
        }
    }
}
