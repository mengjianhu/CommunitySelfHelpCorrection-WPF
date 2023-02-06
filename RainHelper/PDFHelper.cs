using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RainHelper
{
    /// <summary>
    /// pdf相关操作工具类，实现多张图片转换成PDF操作
    /// </summary>
    public class PDFHelper
    {
        private PDFHelper() { }

        /// <summary>
        /// 　两个参数，一个是要转换铁图片文件，一个是新生成的PDF文件。记得路径要写全。
        /// </summary>
        /// <param name="jpgfile">图片文件</param>
        /// <param name="pdf">新生成的PDF文件</param>
        public static void ConvertJPG2PDF(string jpgfile, string pdf)
        {
            var document = new Document(iTextSharp.text.PageSize.A4, 25, 25, 25, 25);


            using (var stream = new FileStream(pdf, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();
                using (var imageStream = new FileStream(jpgfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var image = iTextSharp.text.Image.GetInstance(imageStream);
                    if (image.Height > iTextSharp.text.PageSize.A4.Height - 25)
                    {
                        image.ScaleToFit(iTextSharp.text.PageSize.A4.Width - 25, iTextSharp.text.PageSize.A4.Height - 25);
                    }
                    else if (image.Width > iTextSharp.text.PageSize.A4.Width - 25)
                    {
                        image.ScaleToFit(iTextSharp.text.PageSize.A4.Width - 25, iTextSharp.text.PageSize.A4.Height - 25);
                    }
                    image.Alignment = iTextSharp.text.Image.ALIGN_MIDDLE;
                    document.Add(image);
                }

                document.Close();
            }
        }
        /// <summary>
        /// files参数是存放图片文件的数组 newpdf是新生成的PDF文件的路径及名称。
        /// </summary>
        /// <param name="files">存放图片文件的数组</param>
        /// <param name="newpdf">新生成的PDF文件的路径及名称</param>
        /// <returns></returns>
        public static bool process(List<String> files, string newpdf)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 25, 25, 25, 25);

            try
            {
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(newpdf, FileMode.Create, FileAccess.ReadWrite));

                document.Open();
                iTextSharp.text.Image image;
                for (int i = 0; i < files.Count; i++)
                {
                    if (String.IsNullOrEmpty(files[i])) break;

                    image = iTextSharp.text.Image.GetInstance(files[i]);

                    if (image.Height > iTextSharp.text.PageSize.A4.Height - 25)
                    {
                        image.ScaleToFit(iTextSharp.text.PageSize.A4.Width - 25, iTextSharp.text.PageSize.A4.Height - 25);
                    }
                    else if (image.Width > iTextSharp.text.PageSize.A4.Width - 25)
                    {
                        image.ScaleToFit(iTextSharp.text.PageSize.A4.Width - 25, iTextSharp.text.PageSize.A4.Height - 25);
                    }
                    image.Alignment = iTextSharp.text.Image.ALIGN_MIDDLE;
                    //image.SetDpi(72, 72);

                    document.NewPage();
                    document.Add(image);

                }
                //MessageBox.Show("转换pdf成功", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                // MessageBox.Show("转换pdf出错：" + ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("转换pdf出错：" + ex.ToString());
            }
            finally
            {
                document.Close();
            }
        }
    }
}
