using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace Verwaltungsprogramm_Vinothek
{
    public class PDF
    {
       private PdfDocument doc;
       private PdfPage page;
       private XGraphics gfx;
       private XTextFormatter tf; //für Absatz
       private XFont ueberschrift;
       private XFont font;
       private Produkt prod;
       private static Random r;
       private int posY;
       private string filename;

        public PDF()
        {
            doc = new PdfDocument();
            ueberschrift = new XFont("Arial", 40, XFontStyle.Underline); 
            font = new XFont("Garamond", 20);
            r = new Random();
            posY = 120;
        }
        public byte[] CreateFromProd(Produkt prod)
        {
            filename = $@"{Properties.Settings.Default.PDF_Directory}\{prod.Name}_{r.Next(0, 1000)}.pdf";
            this.prod = prod;
            Create();
            return SaveAndReturn();
        }

        public byte[] CreateFromEvent(List<Produkt> produkte, Event veranstaltung)
        {
            CreateDeckblatt(veranstaltung);
            filename = $@"{Properties.Settings.Default.PDF_Directory}\{veranstaltung.Name}_{r.Next(0, 1000)}.pdf";
            foreach (var v in produkte)
            {
                prod = v;
                Create();
            }
            return SaveAndReturn();
        }
        private byte[] SaveAndReturn()
        {
            doc.Save(filename);
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(filename);
            WPDF.Show();
            return File.ReadAllBytes(filename);
        }

        private void CreateDeckblatt(Event ev)
        {
            page = doc.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            font = new XFont("Garamond", 44, (XFontStyle)4);
            posY = 300;
            gfx.DrawString(ev.Name, font, XBrushes.Black, new XRect(0, 340, page.Width, page.Height), XStringFormats.TopCenter);
            font = new XFont("Garamond", 24);
            DateTime date =(DateTime) ev.Datum;
            string dateString = date.ToString("dddd, dd.MM.yyyy");
            gfx.DrawString(dateString, font, XBrushes.Black, new XRect(0, 400, page.Width, page.Height), XStringFormats.TopCenter);
            try
            {
                gfx.DrawImage(XImage.FromFile(@"\..\Pictures\Logo.png"), 200, 460, 200, 80);
            }
            catch { }
        }

        private void Create()
        {

            page = doc.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            tf = new XTextFormatter(gfx); //für Absatz
            posY = 120;
            if (prod.Picture != null)
            {
                XImage image = GetImg(prod.Picture);
                gfx.DrawImage(image, 460, 100, 90, 300);
            }
            gfx.DrawString($"{prod.Name}", ueberschrift, XBrushes.Black, new XRect(0, 40, page.Width, page.Height), XStringFormats.TopCenter);
            Drawing($"Bezeichnung: {prod.Qualitätssiegel}");
            if (prod.Produzent.Region != "")
            {
                Drawing($"Region: { prod.Produzent.Region}");
            }
            Drawing($"Hersteller: {prod.Produzent.Name}");
            Drawing($"Jahrgang: {prod.Jahrgang}");
            Drawing($"Rebsorte(n): {prod.Rebsorten}");
            Drawing($"Geschmack: {prod.Geschmack}");
            Drawing($"Alkoholgehalt: {prod.Alkoholgehalt} % vol.");
            gfx.DrawString($"Beschreibung:", font, XBrushes.Black, new XRect(40, posY + 100, page.Width, page.Height), XStringFormats.TopLeft);
            tf.DrawString($"{prod.Beschreibung}", font, XBrushes.Black, new XRect(40, posY + 160, page.Width - 100, page.Height), XStringFormats.TopLeft);
            try
            {
                gfx.DrawImage(XImage.FromFile(@"..\Pictures\Logo.png"), 380, 740, 200, 80);
            }
            catch { }
        }

        private void Drawing(string s)
        {
            posY = posY + 30;
            gfx.DrawString(s, font, XBrushes.Black, new XRect(40, posY, page.Width, page.Height), XStringFormats.TopLeft);
        }

        private static XImage GetImg(byte[] b)
        {
            var img = Imageconverter.BinaryToImage(b);
            MemoryStream strm = new MemoryStream();
            img.Save(strm, System.Drawing.Imaging.ImageFormat.Png);
            return XImage.FromStream(strm);
        }
        public string GetPath()
        {
            return filename;
        }
    }
}
