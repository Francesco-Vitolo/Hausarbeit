using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Verwaltungsprogramm_Vinothek
{
    public static class PDF
    {
        public static void Create(Produkt prod)
        {
            int pos = 120;
            string hersteller = prod.Produzent.Name;
            string jahr = Convert.ToString(prod.Jahrgang);
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont ueberschrift = new XFont("Arial", 40, XFontStyle.Underline);
            XTextFormatter tf = new XTextFormatter(gfx);
            XFont font = new XFont("Arial", 20);
            XImage image = GetImg(prod.Picture);
            gfx.DrawString($"{prod.Name}", ueberschrift, XBrushes.Black, new XRect(0, 40, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString($"Bezeichnung: {prod.Qualitätssiegel}", font, XBrushes.Black, new XRect(40, pos + 30, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Region:{prod.Produzent.Region}", font, XBrushes.Black, new XRect(40, pos + 60, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Hersteller: {prod.Produzent.Name}", font, XBrushes.Black, new XRect(40, pos + 90, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Jahr: {prod.Jahrgang}", font, XBrushes.Black, new XRect(40, pos + 120, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Rebsorte(n): {prod.Rebsorten}", font, XBrushes.Black, new XRect(40, pos + 150, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Geschmack: {prod.Geschmack}", font, XBrushes.Black, new XRect(40, pos + 180, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Alkoholgehalt: {prod.Alkoholgehalt} % vol.", font, XBrushes.Black, new XRect(40, pos + 210, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Beschreibung:", font, XBrushes.Black, new XRect(40, pos + 340, page.Width, page.Height), XStringFormats.TopLeft);
            tf.DrawString($"{prod.Beschreibung}", font, XBrushes.Black, new XRect(40, pos + 420, page.Width-120, page.Height), XStringFormats.TopLeft);
            gfx.DrawImage(image, 460, 100, 100, 300);
            string filename = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{prod.Name}_{DateTime.Now.Day}_{DateTime.Now.Month}.pdf";
            doc.Save(filename);
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(filename);
            WPDF.Show();
        }
        private static XImage GetImg(byte[] b)
        {
            var img = ImageConverter.BinaryToImage(b);
            MemoryStream strm = new MemoryStream();
            img.Save(strm, System.Drawing.Imaging.ImageFormat.Png);
            return XImage.FromStream(strm);
        }
    }
}
