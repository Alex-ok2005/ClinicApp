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
    //    private static readonly ImageConverter _imageConverter = new ImageConverter();
    //    public static Bitmap GetImageFromByteArray(byte[] byteArray)
    //    {
    //        Bitmap bm = (Bitmap)_imageConverter.ConvertFrom(byteArray);
    //
    //        if (bm != null && (bm.HorizontalResolution != (int)bm.HorizontalResolution ||
    //                           bm.VerticalResolution != (int)bm.VerticalResolution))
    //        {
    //            // Correct a strange glitch that has been observed in the test program when converting 
    //            //  from a PNG file image created by CopyImageToByteArray() - the dpi value "drifts" 
    //            //  slightly away from the nominal integer value
    //            bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
    //                             (int)(bm.VerticalResolution + 0.5f));
    //        }
    //
    //        return bm;
    //    }
    //
    //    public BitmapSource ByteArrayToImage(byte[] byteArrayIn)
    //    {
    //
    //        using (var stream = new MemoryStream())
    //        {
    //            JpegBitmapDecoder jpegDecoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.Default);
    //            stream.Position = 0;
    //            var bitmapSource = jpegDecoder.Frames[0];
    //            return bitmapSource;
    //        }
    //    }
    //}
}
