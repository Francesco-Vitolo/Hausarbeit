﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Produkt.xaml
    /// </summary>
    public partial class Page_Produkt : Page
    {
        private Produkt prod;
        private VinothekContext ctx = new VinothekContext();
        public Page_Produkt(Produkt p)
        {
            InitializeComponent();
            ctx.Produkt.Load();
            ctx.Produzent.Load();
            prod = p;
            prod = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            DataContext = prod;
        }


        private void UmschaltenBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (felder.IsEnabled == false)
            {
                felder.IsEnabled = true;
                modus.Text = "bearbeiten";
            }
            else
            {
                felder.IsEnabled = false;
                modus.Text = "anschauen";
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
            NavigationService.GoBack();
        }

        private void Button_Click_BildAuswählen(object sender, RoutedEventArgs e)
        {
            string imgPath = SelectFile.Image();

            if (imgPath != null)
            {
                byte[] b = Imageconverter.ConvertImageToByteArray(imgPath);
                path.Content = imgPath;
                prod.Picture = b;
                pic.DataContext = null;
                pic.DataContext = prod;
            }
        }

        private void Button_Click_PDF(object sender, RoutedEventArgs e)
        {
            PDF pdf = new PDF(prod);
            byte[] b = pdf.Create();
            var v = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            v.PDF_file = b;
            ctx.SaveChanges();

        }
        private void btn_show_pdf_Click(object sender, RoutedEventArgs e)
        {
            var v = ctx.Produkt.FirstOrDefault(x => x.ID_Produkt == prod.ID_Produkt);
            string filename = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Moin.pdf";
            File.WriteAllBytes(filename, v.PDF_file);
            Window_PDF_Viewer WPDF = new Window_PDF_Viewer(filename);
            WPDF.Show();
        }

        private void Add_Produzent_Click(object sender, RoutedEventArgs e)
        {
            Window_Select_Object WSP = new Window_Select_Object("ListeProduzenten");
            WSP.ShowDialog();
            Produzent p = (Produzent)WSP.GetObj();            
            prod.Produzent = p;
            DataContext = null;
            DataContext = prod;
            prod.Produzent = null;
            prod.ID_Produzent = p.ID_Produzent;
        }

        private void Button_Previous_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

