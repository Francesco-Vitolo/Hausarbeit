using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für Window_Add_Product.xaml
    /// </summary>
    public partial class Window_Add_Product : Window
    {
        private Vinothek ctx = new Vinothek();
        private Produkt newProd = new Produkt();
        public Window_Add_Product()
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            ctx.Produkt.Load();
            ctx.Produzent.Load();
            System.Windows.Controls.TextBox tb_prod = test.GetProd();
            tb_prod.IsEnabled = true;
            //ImgSrc.DataContext = img;
            //ComboBox v = test.GetComboBox();
            //var list = ctx.Produzent.ToList();
            //foreach (var i in list)
            //{
            //    ComboBoxItem CBI = new ComboBoxItem();
            //    CBI.Content = i.Name;
            //    v.Items.Add(CBI);
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tbs = test.GetTbs();
            newProd.Name = tbs[0].Text;
            newProd.Art = tbs[1].Text;
            newProd.Qualitätssiegel = tbs[2].Text;
            if (int.TryParse(tbs[3].Text, out int i))
                newProd.Jahrgang = i;
            newProd.Rebsorten = tbs[4].Text;
            newProd.Geschmack = tbs[6].Text;
            if (int.TryParse(tbs[7].Text, out i))
                newProd.Alkoholgehalt = i;
            newProd.Beschreibung = test.GetDesc().Text;
            //Prod.Name --> Prod.ID
            string produzentName = tbs[5].Text;

            if (produzentName == "" || tbs[0].Text == "")
            {
                System.Windows.MessageBox.Show("Bitte eingeben du HUND");
                return;
            }
            else
            {
                List<Produzent> list = ctx.Produzent.Local.ToList();
                Produzent pp = list.FirstOrDefault(x => x.Name == produzentName);

                if (pp != null)
                {
                    newProd.Produzent = pp;
                }
                else
                {
                    System.Windows.MessageBox.Show("Noch kein Produzent mit dem Namen vorhanden");
                    Produzent produzent = new Produzent()
                    {
                        Name = produzentName
                    };
                    ctx.Produzent.Add(produzent);
                    ctx.SaveChanges();
                    newProd.Produzent = produzent;
                }
                ctx.Produkt.Add(newProd);
                ctx.SaveChanges();
                System.Windows.Application.Current.MainWindow.Show();
                Close();
            }
        }
        private void Button_Click_BildAuswählen(object sender, RoutedEventArgs e)
        {
            string imgPath = SelectFile.Image();
            
            if (imgPath != null)
            {
                ImgSrc.Content = imgPath;
                byte[] b = ImageConverter.ConvertImageToByteArray(imgPath);
                newProd.Picture = b;
            }

        }

        //private void Button_Click_BildausZwischenablage(object sender, RoutedEventArgs e)
        //{
        //    if (System.Windows.Clipboard.ContainsImage())
        //    {
        //        var image = System.Windows.Forms.Clipboard.GetImage();
        //        newPic.Picture = ImageConverter.ImageToByteArray(image);
        //        ctx.Pictures.Add(newPic);
        //        ctx.SaveChanges();
        //        //var saveFileDialog = new SaveFileDialog();
        //        //saveFileDialog.Filter = "Portable Network Graphics (*.png)|*.png|JPEG-Image (*.jpg)|*.jpg";
        //        //saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //        //saveFileDialog.FileName = "test";
        //        //if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        //{
        //        //    ImageFormat format = imageFormats[saveFileDialog.FilterIndex - 1];
        //        //    try //sonst allgemeiner Fehler in GDI+ bei 2 mal Methode hintereinander
        //        //    {
        //        //        image.Save(saveFileDialog.FileName, format);
        //        //    }
        //        //    catch { }
        //    //    }
        //    }
        //}
    }
}
