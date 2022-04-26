using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Verwaltungsprogramm_Vinothek
{
    public static class SelectFile
    {
        private const string DialogFilterPic = "Portable Network Graphics (*.png)|*.png|JPEG-Image (*.jpg)|*.jpg";
        private const string DialogFilterPDF = "Pdf Files|*.pdf";

        public static string Image()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filter = DialogFilterPic;
            if ((bool)openFileDialog.ShowDialog())
            {
                return openFileDialog.FileName;
            }
            return null;
        }


        public static string PdfDir(string path)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.SelectedPath = path;
            folderDlg.ShowDialog();
            return folderDlg.SelectedPath;
        }


        public static void SavePDF(byte[] b, string name)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = DialogFilterPDF;
            saveFileDialog.FileName = name;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(saveFileDialog.FileName, b);
                Window_PDF_Viewer WPDF = new Window_PDF_Viewer(saveFileDialog.FileName);
                WPDF.Show();
            }
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
    }
}
