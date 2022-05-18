using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace Verwaltungsprogramm_Vinothek
{
    public static class CreateDataGrid
    {
        private static Dictionary<string, string> Produkte = new Dictionary<string, string>() //Header und Rows(SQL)
        {
           { "Name",         "Name" },
           {"Typ",           "Art" } ,
           //{"Bezeichnung",   "Qualitätssiegel"}  ,
           {"Rebsorte(n)",   "Rebsorten"}  ,
           {"Region",        "Produzent.Region"}  ,
           {"Jahrgang",      "Jahrgang"}  ,
           {"Produzent",     "Produzent.Name"}  ,
           //{"Alkoholgehalt", "Alkoholgehalt"} ,
           {"Preis",         "Preis"} ,
           {"Aktiv",         "Aktiv"} ,
           //{"Beschreibung",  "Beschreibung"} ,
        };

    private static Dictionary<string, string> Produzenten = new Dictionary<string, string>()
        {
           { "Name",        "Name" },
           {"Land",         "Land" } ,
           {"Region",       "Region"}  ,
           {"Adresse",      "Adresse"}  ,
           {"Hektar",       "Hektar"}  ,
           {"Email",       "Email"}  ,
           {"Telefon",       "Telefon"}  ,
        };
        private static Dictionary<string, string> Events = new Dictionary<string, string>()
        {
           {"Datum",                "Datum"}  ,
           {"Zeit",                 "Zeit"}  ,
           { "Name",                "Name" },
           {"Anzahl Personen",      "AnzahlPersonen" } ,
        };

        public static DataGrid Produkt(DataGrid datagrid)
        {
            return FillWithHeaderAndBinding(datagrid, Produkte);
        }
        public static DataGrid Produzent(DataGrid datagrid)
        {
            return FillWithHeaderAndBinding(datagrid, Produzenten);

        }
        public static DataGrid Event(DataGrid datagrid)
        {
            return FillWithHeaderAndBinding(datagrid, Events);
        }

        private static DataGrid FillWithHeaderAndBinding(DataGrid datagrid, Dictionary<string, string> dict)
        {
            foreach (var name in dict)
            {
                Binding b = new Binding(name.Value);
                if (name.Key == "Datum") 
                    b.StringFormat = "dd.MM.yyyy";
                DataGridTextColumn column = new DataGridTextColumn()
                {
                    Header = name.Key,
                    Binding = b,

                };
                datagrid.Columns.Add(column);
            }
            return datagrid;
        }

        public static ComboBox ProduktFilter(ComboBox combobox)
        {
            foreach (var filter in Produkte)
            {
                combobox.Items.Add(new ComboBoxItem() { Content = filter.Key });
            }
            return combobox;
        }
        public static ComboBox ProduzentFilter(ComboBox combobox)
        {
            foreach (var filter in Produzenten)
            {
                combobox.Items.Add(new ComboBoxItem() { Content = filter.Key });
            }
            return combobox;
        }
        public static ComboBox EventFilter(ComboBox combobox)
        {
            foreach (var filter in Events)
            {
                combobox.Items.Add(new ComboBoxItem() { Content = filter.Key });
            }
            return combobox;
        }

        public static string[] GetFilterNamesProdukte()
        {
            string[] s = new string[Produkte.Count];
            Produkte.Values.CopyTo(s, 0);
            return s;
        }

        public static string[] GetFilterNamesProduzenten()
        {
            string[] s = new string[Produzenten.Count];
            Produzenten.Values.CopyTo(s, 0);
            return s;
        }

        public static string[] GetFilterNamesEvents()
        {
            string[] s = new string[Events.Count];
            Events.Values.CopyTo(s, 0);
            return s;
        }
    }
}

