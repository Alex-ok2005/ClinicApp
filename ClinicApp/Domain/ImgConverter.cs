using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ClinicApp.Domain
{

    public class ImgConverter
    {
        public static byte[] BitmapToByteArray(object img, string fileName)
        {
            using (var stream = new MemoryStream())
            {
                BitmapEncoder encoder;
                switch (Path.GetExtension(fileName).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case ".png":
                        encoder = new PngBitmapEncoder();
                        break;
                    case ".bmp":
                        encoder = new BmpBitmapEncoder();
                        break;
                    case ".gif":
                        encoder = new GifBitmapEncoder();
                        break;
                    case ".tiff":
                        encoder = new TiffBitmapEncoder();
                        break;
                    default:
                        throw new EncoderFallbackException("Неизвестный формат файла изображения. Подерживаемые форматы изображений: jpeg, png, bmp, gif и tiff.");
                }
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
    }
}
