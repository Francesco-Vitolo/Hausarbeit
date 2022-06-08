using System;
using System.IO;
using OfficeOpenXml;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek
{
    public static class Excel
    {
        private static VinothekContext Ctx { get; set; } = ContextHelper.GetContext();
        private static int yAxis { get; set; } = 1;
        private static Window_Messagebox WM { get; set; }
        public static void Create()
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Produkte");
                worksheet.DefaultColWidth = 15;
                foreach (Produkt prod in Ctx.Produkt)
                {
                    worksheet.Cells['A' + yAxis.ToString()].Value = prod.Preis;
                    worksheet.Cells['A' + yAxis.ToString()].Style.Numberformat.Format = "0.00 €";
                    worksheet.Cells['B' + yAxis.ToString()].Value = prod.Name;
                    worksheet.Cells['C' + yAxis.ToString()].Value = prod.Produzent.Region;
                    worksheet.Cells['D' + yAxis.ToString()].Value = prod.Qualitätssiegel;
                    worksheet.Cells['E' + yAxis.ToString()].Value = prod.Rebsorten;
                    worksheet.Cells['F' + yAxis.ToString()].Value = prod.Jahrgang;
                    worksheet.Cells['G' + yAxis.ToString()].Value = prod.Art;
                    yAxis++;
                }
                try
                {
                    package.SaveAsAsync(new FileInfo($@"{Properties.Settings.Default.PDF_Directory}\Produkte.xlsx"));
                    WM = new Window_Messagebox($"Produkte.xlsx wurde erstellt unter:\n{Properties.Settings.Default.PDF_Directory}");
                    WM.Show();
                }
                catch { }
            }
        }
    }
}
