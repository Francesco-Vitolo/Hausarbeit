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
        //static DataGrid dg = new DataGrid()
        //{
        //    AutoGenerateColumns = false,
        //    IsSynchronizedWithCurrentItem = true,
        //    //ItemsSource = 
        //};
        static Dictionary<string,string> Prod = new Dictionary<string, string>()
        {
           { "Name","Name" },
           {"Typ",          "Art" } ,
           {"Bezeichnung",  "Qualitätssiegel"}  ,
           {"Jahrgang",     "Jahrgang"}  ,
           {"Rebsorte(n)",  "Rebsorten"}  ,
           {"Produzent",    "Produzent.Name"}  ,
           {"Geschmack",    "Geschmack"}  ,
           {"Region",       "Produzent.Region"}  ,
           {"Alkoholgehalt", "Alkoholgehalt"} ,
        };

        public static DataGrid Produkt(DataGrid dg)
        {
            //ctx.Produkt.Load();
            //DataGrid dg = new DataGrid()
            //{
            //    AutoGenerateColumns = false,
            //    IsSynchronizedWithCurrentItem = true,
            //    ItemsSource = 
            //};

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
