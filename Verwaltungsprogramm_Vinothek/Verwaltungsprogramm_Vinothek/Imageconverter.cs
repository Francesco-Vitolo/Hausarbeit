using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using PdfSharp.Drawing;

namespace Verwaltungsprogramm_Vinothek
{
    public static class ImageConverter
    {
        public static Image BinaryToImage(byte[] binaryData)
        {            
            MemoryStream ms = new MemoryStream(binaryData);
            Image img = Image.FromStream(ms);
            return img;
        }

        public static XImage BinaryToXImage(byte[] binaryData)
        {
            MemoryStream ms = new MemoryStream(binaryData);
            XImage img = XImage.FromStream(ms);
            return img;
        }

        public static byte[] ConvertImageToByteArray(string fileName)
        {
            Bitmap bitMap = new Bitmap(fileName);
            ImageFormat bmpFormat = bitMap.RawFormat;
            var imageToConvert = Image.FromFile(fileName);
            using (MemoryStream ms = new MemoryStream())
            {
                imageToConvert.Save(ms, bmpFormat);
                return ms.ToArray();
            }
        }

        public static byte[] ConvertImageFromClipboard(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
