using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FaceMatchDll;
using FaceMatchDll.Models;
using HelperUnit;
using InterfaceReturnEntity;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Panuon.WPF.UI;
using RainHelper;
using Timer = System.Windows.Forms.Timer;
using Window = System.Windows.Window;

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// FaceCjView.xaml 的交互逻辑
    /// </summary>
    public partial class FaceCjView : Window
    {
        VideoCapture videoCapture = null;
        Timer videoTimer = null;
        InterfaceHelper interfaceHelper = new InterfaceHelper();
        public string token { get; set; }
        public FaceCjView()
        {
            Task.Run(() =>
            {
                videoCapture = new VideoCapture(App.indexvide0);//开启一个线程去初始化人像摄像头
                //videoCapture.Fps = (double)30;
            });
            this.WindowState = WindowState.Maximized;
            videoTimer = new Timer();
            videoTimer.Interval = 15;
            videoTimer.Tick += VideoTimer_Tick;
            videoTimer.Start();
            InitializeComponent();
        }
        private void videoRun()
        {
            try
            {
                Mat curMat = videoCapture.RetrieveMat();
                //得到当前RGB摄像头下的图片
                Bitmap bitmap = curMat.ToBitmap();
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
        private void VideoTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (videoCapture != null)
                {
                    Mat mat = videoCapture.RetrieveMat();
                    if (!mat.Empty())
                    {
                        pic_video.Image = mat.ToBitmap();
                    }
                    videoRun();
                }
            }
            catch (Exception)
            {

            }

        }
        /// <summary>
        /// 正面照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_zm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 
                if (string.IsNullOrEmpty(this.txt_rybh.Text))
                {
                    MessageBox.Show("请输入人员编号");
                    return;
                }
                if (this.videoCapture.IsOpened())
                {
                    System.Drawing.Image image = this.videoCapture.RetrieveMat().ToBitmap();
                    this.pic_zm.Image = image;

                    if (!Directory.Exists(App.appStartupPath + "\\Files\\face\\" + this.txt_rybh.Text))
                    {
                        Directory.CreateDirectory(App.appStartupPath + "\\Files\\face\\" + txt_rybh.Text);
                    }
                    string path = App.appStartupPath + "\\Files\\face\\" + txt_rybh.Text + "\\" + this.txt_rybh.Text + "正面照" + ".jpg";
                    pic_zm.Image.Save(path);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "异常信息", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        /// <summary>
        /// 左侧面照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_zcmz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt_rybh.Text))
                {
                    MessageBox.Show("请输入人员编号");
                    return;
                }
                if (this.videoCapture.IsOpened())
                {
                    System.Drawing.Image image = this.videoCapture.RetrieveMat().ToBitmap();
                    this.pic_zcm.Image = image;

                    if (!Directory.Exists(App.appStartupPath + "\\Files\\face\\" + this.txt_rybh.Text))
                    {
                        Directory.CreateDirectory(App.appStartupPath + "\\Files\\face\\" + txt_rybh.Text);
                    }
                    string path = App.appStartupPath + "\\Files\\face\\" + txt_rybh.Text + "\\" + this.txt_rybh.Text + "左侧面照" + ".jpg";
                    pic_zcm.Image.Save(path);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "异常信息", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btn_ycmz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt_rybh.Text))
                {
                    MessageBox.Show("请输入人员编号");
                    return;
                }
                if (this.videoCapture.IsOpened())
                {
                    System.Drawing.Image image = this.videoCapture.RetrieveMat().ToBitmap();
                    this.pic_ycm.Image = image;

                    if (!Directory.Exists(App.appStartupPath + "\\Files\\face\\" + this.txt_rybh.Text))
                    {
                        Directory.CreateDirectory(App.appStartupPath + "\\Files\\face\\" + txt_rybh.Text);
                    }
                    string path = App.appStartupPath + "\\Files\\face\\" + txt_rybh.Text + "\\" + this.txt_rybh.Text + "右侧面照" + ".jpg";
                    pic_ycm.Image.Save(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常信息", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                videoTimer?.Stop();
                videoCapture?.Dispose();
            });
            this.Close();
        }

        UpFileBase64 upFile = new UpFileBase64();
        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_rybh.Text))
            {
                MessageBox.Show("人员编号不能为空", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (pic_zm.Image == null || pic_zcm.Image == null || pic_ycm.Image == null)
            {
                MessageBox.Show("请将人像信息采集完成", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var handler = PendingBox.Show("正在处理数据", "提示信息");
            try
            {
                int j = 0;
                if (string.IsNullOrEmpty(token))
                {
                    token = interfaceHelper.GetSJToken();
                    // MessageBox.Show(token);
                }
                for (int i = 1; i <= 3; i++)
                {
                    UpLoadFeatures upLoadFeatures = new UpLoadFeatures();//左手拇指
                    upLoadFeatures.termerId = this.txt_rybh.Text;
                    upLoadFeatures.type = 0.ToString();
                    upLoadFeatures.no = i;
                    switch (i)
                    {
                        case 1:
                            string base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files\\face\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "左侧面照" + ".jpg");
                            string filid = upFile.getFileID(base64File, this.txt_rybh.Text + "左侧面照", token);
                            if (string.IsNullOrEmpty(filid))
                            {
                                handler.Close();
                                MessageBox.Show("上传人脸信息获取编号异常", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                            upLoadFeatures.fileId = filid;
                            break;
                        case 2:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files\\face\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "正面照" + ".jpg");
                            string filid1 = upFile.getFileID(base64File, this.txt_rybh.Text + "正面照", token);
                            if (string.IsNullOrEmpty(filid1))
                            {
                                handler.Close();
                                MessageBox.Show("上传人脸信息获取编号异常", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                            upLoadFeatures.fileId = filid1;
                            break;
                        case 3:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files\\face\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "右侧面照" + ".jpg");
                            string filid2 = upFile.getFileID(base64File, this.txt_rybh.Text + "右侧面照", token);
                            if (string.IsNullOrEmpty(filid2))
                            {
                                handler.Close();
                                MessageBox.Show("上传人脸信息获取编号异常", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                            upLoadFeatures.fileId = filid2;
                            break;
                    }
                    //MessageBox.Show(upLoadFeatures.fileId, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    string tzID = interfaceHelper.uploadFeatures(JsonConvert.SerializeObject(upLoadFeatures), token);
                    handler.Close();
                    j++;
                 
                }
                if (j >= 3)
                {
                    MessageBox.Show("上传人像信息完成", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                handler.Close();
                MessageBox.Show("上传数据异常:" + ex.Message);
                return;
            }
        }
    }
}
