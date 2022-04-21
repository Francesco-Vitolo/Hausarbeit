﻿using System;
using System.Collections.Generic;
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
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek.Pages
{
    /// <summary>
    /// Interaktionslogik für Page_Add_Produzent.xaml
    /// </summary>
    public partial class Page_Add_Produzent : Page
    {
        private VinothekContext ctx = new VinothekContext();
        public Page_Add_Produzent()
        {
            InitializeComponent();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Produzent produzent = new Produzent();
            var tbs = felder.GetTbs();
            if (tbs[0].Text != "")
            {
                produzent.Name = tbs[0].Text;
                produzent.Land = tbs[1].Text;
                produzent.Region = tbs[2].Text;
                if (int.TryParse(tbs[3].Text, out int i))
                    produzent.Hektar = i;
                produzent.Adresse = tbs[4].Text;
                produzent.Beschreibung = tbs[5].Text;
                ctx.Produzent.Add(produzent);
                ctx.SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                Window_Messagebox WM = new Window_Messagebox("Bitte Name eingeben");
                WM.ShowDialog();
            }
        }

        private void Button_Previous_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

