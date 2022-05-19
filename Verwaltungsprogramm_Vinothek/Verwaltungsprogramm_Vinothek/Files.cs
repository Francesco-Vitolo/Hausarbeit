using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Verwaltungsprogramm_Vinothek.Properties;

namespace Verwaltungsprogramm_Vinothek
{
    public static class Files
    {
        private const string DialogFilterPic = "Portable Network Graphics (*.png)|*.png|JPEG-Image (*.jpg)|*.jpg";
        private const string DialogFilterPDF = "Pdf Files|*.pdf";

        public static string SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filter = DialogFilterPic;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        public static string SelectPdfDir(string path)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.SelectedPath = path;
            folderDlg.ShowDialog();
            return folderDlg.SelectedPath;
        }

        public static Image SelectImgfromClipboard()
        {
            if (System.Windows.Clipboard.ContainsImage())
            {
                var image = Clipboard.GetImage();
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = DialogFilterPic;
                return image;
            }
            return null;
        }

        public static void SavePDF(byte[] Img, string name)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Settings.Default.PDF_Directory;
            saveFileDialog.Filter = DialogFilterPDF;
            saveFileDialog.FileName = name;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(saveFileDialog.FileName, Img);
                Window_PDF_Viewer WPDF = new Window_PDF_Viewer(saveFileDialog.FileName);
                WPDF.Show();
            }
        }

    }
}
