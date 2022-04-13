using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Verwaltungsprogramm_Vinothek
{
    static class CreateDataGrid
    {
        //static DataGrid dg = new DataGrid()
        //{
        //    AutoGenerateColumns = false,
        //    IsSynchronizedWithCurrentItem = true,
        //    //ItemsSource = 
        //};

        static Dictionary<string,string> Prod = new Dictionary<string, string>()
        {
           { "Name","name" },
           {"Typ",          "dada" } ,
           {"Bezeichnung",  "dada"}  ,
           {"Jahrgang",     "dada"}  ,
           {"Rebsorte(n)",  "dada"}  ,
           {"Produzent",    "dada"}  ,
           {"Geschmack",    "dada"}  ,
           {"Region",       "dada"}  ,
           {"Alkoholgehalt", "dada"} ,
        };

        public static DataGrid Produkt(DataGrid dg)
        {
            foreach (var v in Prod)
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
        public static DataGrid Produzenten()
        {
            return new DataGrid();
        }
        public static DataGrid Events()
        {
            return new DataGrid();
        }
    }
}
