using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FaceMatchDll;
using FaceMatchDll.Models;
using OpenCvSharp;
using Window = System.Windows.Window;

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// SignInView.xaml 的交互逻辑
    /// </summary>
    public partial class SignInView : Window
    {
        VideoCapture videoCapture = null;
        Timer timer = null;
        public SignInView()
        {
           
            InitializeComponent();
           // videoCapture = new VideoCapture(0);
            this.Closing += SignInView_Closing;
            timer = new Timer();
            timer.Interval =15;
            timer.Tick += Timer_Tick;
            //timer.Start();
            this.WindowState = WindowState.Maximized;
        }

        private void SignInView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer?.Stop();
            videoCapture?.Dispose();
        }
        private void videoRun()
        {
            try
            {
                Mat curMat = videoCapture.RetrieveMat();
                //得到当前RGB摄像头下的图片
                Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(curMat);
                if (bitmap == null)
                {
                    return;
                }
                //检测人脸，得到Rect框
                ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(App.pVideoEngine, bitmap);
                //得到最大人脸
                ASF_SingleFaceInfo maxFace = FaceUtil.GetMaxFace(multiFaceInfo);
                //得到Rect
                MRECT rect = maxFace.faceRect;
                //检测RGB摄像头下最大人脸
                Graphics g = Graphics.FromImage(bitmap);
                float offsetX = 1;
                float offsetY = 1;
                float x = rect.left * offsetX;
                float width = rect.right * offsetX - x;
                float y = rect.top * offsetY;
                float height = rect.bottom * offsetY - y;
                Width = (int)width;
                Height = (int)height;
                g.DrawRectangle(Pens.Red, x, y, width, height);
                this.pic_video.Image = bitmap;
            }
            catch (Exception)
            {
                return;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Bitmap bitmap = null;
            try
            {
              
                if (videoCapture != null)
                {
                    Mat mat = videoCapture.RetrieveMat();
                    if (!mat.Empty())
                    {
                        bitmap=OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
                       // this.pic_video.Image = bitmap;
                       
                    } 
                }
                //videoRun();
            }
            catch (Exception)
            {

            }
            finally
            {
                if (bitmap != null)
                {
                  
                }

            }

        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
