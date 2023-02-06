using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace RainHelper
{
   public class ImageHelper
    {
        public static Image getImageBuf(byte[] buf)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(buf))
                {
                    return Image.FromStream(ms);
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static byte[] ImageToByte(Bitmap bitmap)
        {
         
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            byte[] bytes = ms.GetBuffer();  
            ms.Close();
            return bytes;
        }
        public static byte[] BitmapByte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }
    }
}
