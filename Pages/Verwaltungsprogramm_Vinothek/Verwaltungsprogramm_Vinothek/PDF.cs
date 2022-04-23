using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.IO;

namespace Verwaltungsprogramm_Vinothek
{
    public class PDF
    {
        PdfDocument doc = new PdfDocument();
        PdfPage page;
        XGraphics gfx;
        XTextFormatter tf; //für Absatz
        XFont ueberschrift = new XFont("Arial", 40, XFontStyle.Underline);
        XFont font = new XFont("Arial", 20);
        int pos = 120;
        Produkt prod;
        Random r;
        string filename;

        public PDF(Produkt prod)
        {
            this.prod = prod;
            doc = new PdfDocument();
            page = doc.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            tf = new XTextFormatter(gfx); //für Absatz
            font = new XFont("Arial", 20);
            r = new Random();
        }
        public byte[] Create()
        {
            if (prod.Picture != null)
            {
                XImage image = GetImg(prod.Picture);
                gfx.DrawImage(image, 460, 100, 100, 300);
            }
            gfx.DrawString($"{prod.Name}", ueberschrift, XBrushes.Black, new XRect(0, 40, page.Width, page.Height), XStringFormats.TopCenter);
            Drawing($"Bezeichnung: {prod.Qualitätssiegel}");
            Drawing($"Region: { prod.Produzent.Region}");
            Drawing($"Hersteller: {prod.Produzent.Name}");
            Drawing($"Jahrgang: {prod.Jahrgang}");
            Drawing($"Rebsorte(n): {prod.Rebsorten}");
            Drawing($"Geschmack: {prod.Geschmack}");
            Drawing($"Alkoholgehalt: {prod.Alkoholgehalt} % vol.");
            gfx.DrawString($"Beschreibung:", font, XBrushes.Black, new XRect(40, pos + 100, page.Width, page.Height), XStringFormats.TopLeft);
            tf.DrawString($"{prod.Beschreibung}", font, XBrushes.Black, new XRect(40, pos + 160, page.Width - 100, page.Height), XStringFormats.TopLeft);
            filename = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{prod.Name}_{DateTime.Now.Day}_{DateTime.Now.Month}_{r.Next(0, 1000)}.pdf"; //Während Laufzeit geht überschreiben nicht, dehalb provisorisch r.Next
            doc.Save(filename);
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(filename);
            WPDF.Show();
            return File.ReadAllBytes(filename);
        }

        public string GetPath()
        {
            return filename;
        }
        private void Drawing(string s)
        {
            pos = pos + 30;
            gfx.DrawString(s, font, XBrushes.Black, new XRect(40, pos, page.Width, page.Height), XStringFormats.TopLeft);
        }

        private static XImage GetImg(byte[] b)
        {
            var img = Imageconverter.BinaryToImage(b);
            MemoryStream strm = new MemoryStream();
            img.Save(strm, System.Drawing.Imaging.ImageFormat.Png);
            return XImage.FromStream(strm);
        }
    }
}
