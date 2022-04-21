using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verwaltungsprogramm_Vinothek
{
    public static class SelectFile
    {
        private const string DialogFilter = "Portable Network Graphics (*.png)|*.png|JPEG-Image (*.jpg)|*.jpg";

        public static string Image()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filter = DialogFilter;
            if ((bool)openFileDialog.ShowDialog())
            {
                return openFileDialog.FileName;
            }
            return null;
        }
    }
}
