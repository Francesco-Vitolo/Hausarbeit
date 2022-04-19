using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Verwaltungsprogramm_Vinothek
{
    static class CreateDataGrid
    {
        static Dictionary<string,string> Produkte = new Dictionary<string, string>() //Header und Rows(SQL)
        {
           { "Name",         "Name" },
           {"Typ",           "Art" } ,
           {"Bezeichnung",   "Qualitätssiegel"}  ,
           {"Jahrgang",      "Jahrgang"}  ,
           {"Rebsorte(n)",   "Rebsorten"}  ,
           {"Produzent",     "Produzent.Name"}  ,
           {"Geschmack",     "Geschmack"}  ,
           {"Region",        "Produzent.Region"}  ,
           {"Alkoholgehalt", "Alkoholgehalt"} ,
           {"Beschreibung",  "Beschreibung"} ,
        };

        static Dictionary<string, string> Produzenten = new Dictionary<string, string>()
        {
           { "Name",        "Name" },
           {"Land",         "Land" } ,
           {"Region",       "Region"}  ,
           {"Adresse",      "Adresse"}  ,
           {"Hektar",       "Hektar"}  ,
           {"Beschreibung", "Beschreibung"}  ,
        };
        static Dictionary<string, string> Events = new Dictionary<string, string>()
        {
           {"Datum",       "Datum"}  ,
           {"Zeit",       "Zeit"}  ,
           { "Name",       "Name" },
           {"Anzahl",      "AnzahlPersonen" } ,
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

        public static DataGrid Rest(DataGrid dg, Dictionary<string,string> dict  )
        {
            foreach (var v in dict)
            {
                DataGridTextColumn column = new DataGridTextColumn()
                {
                    Header = v.Key,
                    Binding = new Binding(v.Value),
                };
                dg.Columns.Add(column);
            }
            return dg;
        }
    }
}
