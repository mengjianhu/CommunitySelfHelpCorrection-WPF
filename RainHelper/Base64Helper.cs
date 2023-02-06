using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainHelper
{
    public class Base64Helper

    { 
        /// <summary>
        /// 将图片转换为字符的形式（解码）
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string GetBase64String(Bitmap image)
        {
            string UserPhoto = "";
            using (MemoryStream ms1 = new MemoryStream())
            {
                image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr1 = new byte[ms1.Length];
                ms1.Position = 0;
                ms1.Read(arr1, 0, (int)ms1.Length);
                ms1.Close();
                UserPhoto = Convert.ToBase64String(arr1);
            }
            return UserPhoto;
        }

        //编码
        public static void SaveImageJpg(string filePath, string imgBase64)
        {
            byte[] arr2 = Convert.FromBase64String(imgBase64);
            using (MemoryStream ms2 = new MemoryStream(arr2))
            {
                System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(ms2);
                if (!Directory.Exists(Directory.GetParent(filePath).FullName))
                {
                    Directory.CreateDirectory(Directory.GetParent(filePath).FullName);
                }
                bmp2.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                bmp2.Dispose();
            }
        }
        /// <summary>
        /// 把base64转为Btmap
        /// </summary>
        /// <param name="imgBase64"></param>
        /// <returns></returns>
        public static Bitmap getImgByBase64(string imgBase64)
        {
            byte[] arr2 = Convert.FromBase64String(imgBase64);
            using (MemoryStream ms2 = new MemoryStream(arr2))
            {
                System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(ms2);
                return bmp2;
            }
        }
        public static byte[] Base64ToByte(string base64)
        {
            try
            {

                byte[] bb = Convert.FromBase64String(base64);
                return bb;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void SaveImageToBmp(string filePath, string imageBase64)
        {
            try
            {
                byte[] imgArr = Convert.FromBase64String(imageBase64);
                using (MemoryStream ms = new MemoryStream(imgArr))
                {
                    Bitmap bitmap = new Bitmap(ms);
                    bitmap.Save(filePath, ImageFormat.Bmp);
                    bitmap?.Dispose();
                    ms?.Dispose();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public static bool SaveImageBmp(string filePath, string imgBase64)
        {
            try
            {
                byte[] arr2 = Convert.FromBase64String(imgBase64);
                using (MemoryStream ms2 = new MemoryStream(arr2))
                {
                    System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(ms2);
                    bmp2.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
                    bmp2.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /*
         * 在C#开发中，可以根据需求对文件进行base64的转码和解码。本文将提供两个函数实现上述功能，
         * 支持多种格式的文件和base64编码之间的相互转换，包括图片（例：gif、jpg、png、bmp）、
         * 文档（例：txt、doc、xlsx）、压缩包（例：zip、rar）等
         */
        /// <summary>
        /// 文件转为base64编码
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string FileToBase64Str(string filePath)
        {
            string base64Str = string.Empty;
            try
            {
                using (FileStream filestream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] bt = new byte[filestream.Length];
                    //调用read读取方法
                    filestream.Read(bt, 0, bt.Length);
                    base64Str = Convert.ToBase64String(bt);
                    filestream.Close();
                }
                return base64Str;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 文件base64解码
        /// </summary>
        /// <param name="base64Str">文件base64编码</param>
        /// <param name="outPath">生成文件路径</param>
        public static void Base64ToOriFile(string base64Str, string outPath)
        {
            try
            {
                var contents = Convert.FromBase64String(base64Str);
                using (var fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(contents, 0, contents.Length);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }

    }
}
