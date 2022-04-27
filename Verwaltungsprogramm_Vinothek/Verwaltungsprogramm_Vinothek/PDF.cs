﻿using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace Verwaltungsprogramm_Vinothek
{
    public class PDF
    {
       private PdfDocument doc = new PdfDocument();
       private PdfPage page;
       private XGraphics gfx;
       private XTextFormatter tf; //für Absatz
       private XFont ueberschrift = new XFont("Arial", 40, XFontStyle.Underline);
       private XFont font = new XFont("Arial", 20);
       private Produkt prod;
       private Random r;
       private int posY = 120;
       private string filename;

        public PDF()
        {
            doc = new PdfDocument();
            r = new Random();
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
        public string GetPath()
        {
            return filename;
        }

        private void CreateDeckblatt(Event ev)
        {
            page = doc.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            font = new XFont("Arial", 44, (XFontStyle)4);
            posY = 300;
            gfx.DrawString(ev.Name, font, XBrushes.Black, new XRect(0, 340, page.Width, page.Height), XStringFormats.TopCenter);
            font = new XFont("Arial", 24);
            gfx.DrawString(ev.Datum, font, XBrushes.Black, new XRect(0, 400, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawImage(XImage.FromFile(@"..\..\Pictures\Logo.png"), 200, 460, 200, 80);
        }

        private void Create()
        {
            page = doc.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            tf = new XTextFormatter(gfx); //für Absatz
            font = new XFont("Arial", 20);
            posY = 120;
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
            gfx.DrawString($"Beschreibung:", font, XBrushes.Black, new XRect(40, posY + 100, page.Width, page.Height), XStringFormats.TopLeft);
            tf.DrawString($"{prod.Beschreibung}", font, XBrushes.Black, new XRect(40, posY + 160, page.Width - 100, page.Height), XStringFormats.TopLeft);
            gfx.DrawImage(XImage.FromFile(@"..\..\Pictures\Logo.png"), 380, 740, 200, 80);
        }


        private byte[] SaveAndReturn()
        {
            doc.Save(filename);
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(filename);
            WPDF.Show();
            return File.ReadAllBytes(filename);
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
    }
}
