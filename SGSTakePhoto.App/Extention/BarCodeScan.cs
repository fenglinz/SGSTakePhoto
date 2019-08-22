using com.google.zxing;
using com.google.zxing.common;
using SGSTakePhoto.Infrastructure;
using System;
using System.Drawing;
using System.IO;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// 
    /// </summary>
    public class BarCodeScan
    {
        /// <summary>
        /// 获取条形码的值
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Response<string> GetBarCode(Stream stream)
        {
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                Bitmap img = (Bitmap)Image.FromStream(stream);
                LuminanceSource source = new RGBLuminanceSource(img, img.Width, img.Height);
                BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));

                MultiFormatReader reader = new MultiFormatReader();
                Result result = reader.decode(bitmap);

                return new Response<string> { Data = result.Text };
            }
            catch (Exception ex)
            {
                return new Response<string> { Errors = ex.Message };
            }
        }
    }
}
