using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Management;
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
using 社区自助矫正系统WPF.Common.Helper;
using Rectangle = System.Drawing.Rectangle;
using Window = System.Windows.Window;

namespace 社区自助矫正系统WPF.Views.VideoShow
{
    /// <summary>
    /// OpenCVSharpVideo.xaml 的交互逻辑
    /// </summary>
    public partial class OpenCVSharpVideo : Window
    {
        #region 字段
        private VideoCapture capCamera;
        private VideoWriter videoWriter;
        private Mat matImage = new Mat();

        private Thread cameraThread;
        private Thread writerThread;
        private CascadeClassifier haarCascade;
        private WriteableBitmap writeableBitmap;

        private Rectangle rectangle;
        private OpenCvSharp.Rect[] faces;
        private Mat gray;
        private Mat result;
        #endregion
        public OpenCVSharpVideo()
        {
            InitializeComponent();
            Width = SystemParameters.WorkArea.Width / 1.5;
            Height = SystemParameters.WorkArea.Height / 1.5;
            this.Loaded += OpenCVSharpVideo_Loaded;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCVSharpVideo_Loaded(object sender, RoutedEventArgs e)
        {
            // InitializeCamera();
            CreateCamera();
            writeableBitmap = new WriteableBitmap(capCamera.FrameWidth, capCamera.FrameHeight, 0, 0, System.Windows.Media.PixelFormats.Bgra32, null);
            imgViewport.Source = writeableBitmap;
        }
        #region 依赖属性


        

        public List<string> CameraArray
        {
            get { return (List<string>)GetValue(CameraArrayProperty); }
            set { SetValue(CameraArrayProperty, value); }
        }

        public static readonly DependencyProperty CameraArrayProperty =
            DependencyProperty.Register("CameraArray", typeof(List<string>), typeof(OpenCVSharpVideo), new PropertyMetadata(null));
        public int CameraIndex
        {
            get { return (int)GetValue(CameraIndexProperty); }
            set { SetValue(CameraIndexProperty, value); }
        }

        public static readonly DependencyProperty CameraIndexProperty =
            DependencyProperty.Register("CameraIndex", typeof(int), typeof(OpenCVSharpVideo), new PropertyMetadata(0));
        public List<string> takePhotoSource
        {
            get { return (List<string>)GetValue(takePhotoSourceProperty); }
            set { SetValue(takePhotoSourceProperty, value); }
        }
        public static readonly DependencyProperty takePhotoSourceProperty =
          DependencyProperty.Register("takePhotoSource", typeof(List<string>), typeof(OpenCVSharpVideo), new PropertyMetadata(null));

        #endregion


        #region 方法
        void CloseFace()
        {
            if (haarCascade != null)
            {
                haarCascade.Dispose();
                haarCascade = null;
                gray.Dispose();
                gray = null;
                result.Dispose();
                result = null;
                faces = null;
            }
        }
        
        private void InitializeCamera()
        {
            CameraArray = GetAllConnectedCameras();
        }
        List<string> GetAllConnectedCameras()
        {
            var cameraNames = new List<string>();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE (PNPClass = 'Image' OR PNPClass = 'Camera')"))
            {
                foreach (var device in searcher.Get())
                {
                    cameraNames.Add(device["Caption"].ToString());
                }
            }

            return cameraNames;
        }

        void CreateCamera()
        {
            capCamera = new VideoCapture(0);
            capCamera.Fps = 60;
            cameraThread = new Thread(PlayCamera);
            cameraThread.Start();
        }

        private void PlayCamera()
        {
            while (capCamera != null && !capCamera.IsDisposed)
            {
                capCamera.Read(matImage);
                if (matImage.Empty()) break;
                using (var img = BitmapConverter.ToBitmap(matImage))
                {
                    var now = DateTime.Now;
                    var g = Graphics.FromImage(img);
                    var brush = new SolidBrush(System.Drawing.Color.Red);
                    System.Globalization.CultureInfo cultureInfo = new CultureInfo("zh-CN");
                    var week = cultureInfo.DateTimeFormat.GetAbbreviatedDayName(now.DayOfWeek);
                    g.DrawString($"{week} {now.ToString("yyyy年MM月dd日 HH:mm:ss ")} ", new System.Drawing.Font(System.Drawing.SystemFonts.DefaultFont.Name, System.Drawing.SystemFonts.DefaultFont.Size), brush, new PointF(0, matImage.Rows - 20));
                    brush.Dispose();
                    g.Dispose();
                    rectangle = new Rectangle(0, 0, img.Width, img.Height);
                    Dispatcher.Invoke(new Action(() =>
                    {
                        WriteableBitmapHelper.BitmapCopyToWriteableBitmap(img, writeableBitmap, rectangle, 0, 0, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    }));
                    img.Dispose();
                };

                Thread.Sleep(100);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("是否关闭?", "询问", MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
            {
                e.Cancel = true;
                return;
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            StopDispose();
        }

        private void AddCameraFrameToRecording()
        {
            var waitTimeBetweenFrames = 1_000 / capCamera.Fps;
            var lastWrite = DateTime.Now;

            while (!videoWriter.IsDisposed)
            {
                if (DateTime.Now.Subtract(lastWrite).TotalMilliseconds < waitTimeBetweenFrames)
                    continue;
                lastWrite = DateTime.Now;
                videoWriter.Write(matImage);
            }
        }
        void StopDispose()
        {
            if (capCamera != null && capCamera.IsOpened())
            {
                capCamera.Dispose();
                capCamera = null;
            }

            if (videoWriter != null && !videoWriter.IsDisposed)
            {
                videoWriter.Release();
                videoWriter.Dispose();
                videoWriter = null;
            }
            CloseFace();
           
            GC.Collect();
        }


        #endregion

        private void btn_takePhoto(object sender, RoutedEventArgs e)
        {
            
            this.list_photo.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".jpg");
            
        }

        private void btn_del(object sender, RoutedEventArgs e)
        {
            


            if (list_photo.Items.Count == 1)
            {
                this.list_photo.Items.Clear();
                return;
            }

            if (list_photo.SelectedIndex >= 0)
            {
                this.list_photo.Items.Remove(list_photo.SelectedItem);
            }
            else
            {
                MessageBox.Show("删除失败");
            }
          
        }
    }
}
