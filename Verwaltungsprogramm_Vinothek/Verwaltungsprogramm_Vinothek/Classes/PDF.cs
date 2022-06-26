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
       private PdfDocument Document { get; }
       private XFont Font { get; }
       private XFont Ueberschrift { get; }
       private PdfPage Page { get; set; }
       private XTextFormatter Tf { get; set; }
       private XGraphics Gfx { get; set; }
       private Produkt Prod { get; set; }
       private Event Veranstaltung { get; set; }
       private int PosY { get; set; } = 120;
       private Random Rand { get; set; }
       public string filename { get; private set; }

        public PDF()
        { 
            Document = new PdfDocument();
            Ueberschrift = new XFont("Garamond", 44, (XFontStyle)4);
            Font = new XFont("Garamond", 20);
            Rand = new Random();
        }
        public byte[] CreateFromProd(Produkt prod)
        {
            filename = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\tmp\{ prod.Name}_{Rand.Next(0, 1000)}.pdf";
            Prod = prod;
            Create();
            return Save();
        }

        public bool CreateFromEvent(List<Produkt> produkte, Event veranstaltung)
        {
            Veranstaltung = veranstaltung;
            CreateDeckblatt();
            DateTime date = (DateTime)Veranstaltung.Datum;
            string dateString = date.ToString("dd_MM_yy");
            filename = $@"{Properties.Settings.Default.PDF_Directory}\{Veranstaltung.Name}_{dateString}.pdf";
            foreach (var aktuellesProd in produkte)
            {
                Prod = aktuellesProd;
                Create();
            }
            try
            {
                string eventFilename = $@"{Properties.Settings.Default.PDF_Directory}\{veranstaltung.Name}_{Rand.Next(0, 1000)}.pdf";
                Document.Save(eventFilename);
                Window_PDF_Viewer WPDF = new Window_PDF_Viewer(eventFilename);
                WPDF.Show();
                return true;
            }
            catch { }
            return false;
        }

        private void CreateDeckblatt()
        {
            Page = Document.AddPage();
            Gfx = XGraphics.FromPdfPage(Page);
            PosY = 300;
            Gfx.DrawString(Veranstaltung.Name, Font, XBrushes.Black, new XRect(0, 340, Page.Width, Page.Height), XStringFormats.TopCenter);
            DateTime date =(DateTime) Veranstaltung.Datum;
            string dateString = date.ToString("dddd, dd.MM.yyyy");
            Gfx.DrawString(dateString, Font, XBrushes.Black, new XRect(0, 400, Page.Width, Page.Height), XStringFormats.TopCenter);
            try
            {
                Gfx.DrawImage(XImage.FromFile(@"\..\Images\Logo.png"), 200, 460, 200, 80);
            }
            catch { }
        }

        private void Create()
        {
            Page = Document.AddPage();
            Gfx = XGraphics.FromPdfPage(Page);
            Tf = new XTextFormatter(Gfx); //Absatz automatisch
            PosY = 120;
            if (Prod.Picture != null)
            {
                XImage image = ImageConverter.BinaryToXImage(Prod.Picture);
                Gfx.DrawImage(image, 460, 100, 90, 300);
            }
            Gfx.DrawString($"{Prod.Name}", Ueberschrift, XBrushes.Black, 
                new XRect(0, 40, Page.Width, Page.Height), XStringFormats.TopCenter);
            Drawing($"Bezeichnung: {Prod.Qualitätssiegel}");
            if (Prod.Produzent.Region != "")
            {
                Drawing($"Region: { Prod.Produzent.Region}");
            }
            Drawing($"Hersteller: {Prod.Produzent.Name}");
            Drawing($"Jahrgang: {Prod.Jahrgang}");
            Drawing($"Rebsorte(n): {Prod.Rebsorten}");
            Drawing($"Geschmack: {Prod.Geschmack}");
            Drawing($"Alkoholgehalt: {Prod.Alkoholgehalt} % vol.");
            Gfx.DrawString($"Beschreibung:", Font, XBrushes.Black, 
                new XRect(40, PosY + 100, Page.Width, Page.Height), XStringFormats.TopLeft);
            Tf.DrawString($"{Prod.Beschreibung}", Font, XBrushes.Black, 
                new XRect(40, PosY + 160, Page.Width - 100, Page.Height), XStringFormats.TopLeft);
            try
            {
                Gfx.DrawImage(XImage.FromFile(@"..\Images\Logo.png"), 380, 740, 200, 80);
            }
            catch { }
        }

        private void Drawing(string s)
        {
            PosY = PosY + 30;
            Gfx.DrawString(s, Font, XBrushes.Black, 
                new XRect(40, PosY, Page.Width, Page.Height), XStringFormats.TopLeft);
        }
        private byte[] Save()
        {
            try
            {
                Document.Save(filename);
                //Window_PDF_Viewer WPDF = new Window_PDF_Viewer(filename);
                //WPDF.Show();
                return File.ReadAllBytes(filename);
            }
            catch { }
            return null;
        }
    }
}
