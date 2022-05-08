﻿using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace Verwaltungsprogramm_Vinothek
{
    static class CreateDataGrid
    {
        private static Dictionary<string, string> Produkte = new Dictionary<string, string>() //Header und Rows(SQL)
        {
           { "Name",         "Name" },
           {"Typ",           "Art" } ,
           {"Bezeichnung",   "Qualitätssiegel"}  ,
           {"Rebsorte(n)",   "Rebsorten"}  ,
           {"Region",        "Produzent.Region"}  ,
           {"Jahrgang",      "Jahrgang"}  ,
           {"Produzent",     "Produzent.Name"}  ,
           {"Geschmack",     "Geschmack"}  ,
           {"Alkoholgehalt", "Alkoholgehalt"} ,
           {"Preis", "Preis"} ,
           {"Aktiv", "Aktiv"} ,
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
           //{"Beschreibung", "Beschreibung"}  ,
        };
        private static Dictionary<string, string> Events = new Dictionary<string, string>()
        {
           {"Datum",       "Datum"}  ,
           {"Zeit",       "Zeit"}  ,
           { "Name",       "Name" },
           {"Anzahl Personen",      "AnzahlPersonen" } ,
        };       

        public static DataGrid Produkt(DataGrid dg)
        {
            return Rest(dg, Produkte);
        }
        public static DataGrid Produzent(DataGrid dg)
        {
            return Rest(dg, Produzenten);

        }
        public static DataGrid Event(DataGrid dg)
        {
            return Rest(dg, Events);
        }

        public static DataGrid Rest(DataGrid dg, Dictionary<string, string> dict)
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
                dg.Columns.Add(column);
            }
            return dg;
        }

        public static ComboBox ProduktFilter(ComboBox c)
        {
            foreach (var v in Produkte)
            {
                c.Items.Add(new ComboBoxItem() { Content = v.Key });
            }
            return c;
        }
        public static ComboBox ProduzentFilter(ComboBox c)
        {
            foreach (var v in Produzenten)
            {
                c.Items.Add(new ComboBoxItem() { Content = v.Key });
            }
            return c;
        }
        public static ComboBox EventFilter(ComboBox c)
        {
            foreach (var v in Events)
            {
                c.Items.Add(new ComboBoxItem() { Content = v.Key });
            }
            return c;
        }

        public static string[] GetFilterNamesProdukte()
        {
            string[] s = new string[11];
            Produkte.Values.CopyTo(s, 0);
            return s;
        }

        public static string[] GetFilterNamesProduzenten()
        {
            string[] s = new string[7];
            Produzenten.Values.CopyTo(s, 0);
            return s;
        }

        public static string[] GetFilterNamesEvents()
        {
            string[] s = new string[4];
            Events.Values.CopyTo(s, 0);
            return s;
        }
    }
}

