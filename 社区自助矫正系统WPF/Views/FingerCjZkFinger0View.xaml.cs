using HelperUnit;
using InterfaceReturnEntity;
using Newtonsoft.Json;
using RainHelper;
using SQIDCardHelper;
using SQIDCardHelper.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using ZKFinger;

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// FingerCjZkFinger0View.xaml 的交互逻辑
    /// </summary>
    public partial class FingerCjZkFinger0View : Window
    {
        private volatile bool m_bTimeToStop;
        private Thread m_threadCapture;
        private bool isOpen = false;
        IDCardReader cardReader = new IDCardReader();
        InterfaceHelper interfaceHelper = new InterfaceHelper();
        UpFileBase64 upFile = new UpFileBase64();//上传文件
        private string token { get; set; }
        Random random = new Random();
        public FingerCjZkFinger0View()
        {
            InitializeComponent();
            //Thread thread = new Thread(new ThreadStart(initDevice));
            //thread.IsBackground = true;
            //thread.Start();
            // Task.Run(() => { initDevice(); });
            //initDevice();
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.txt_sfzh.Text = random.Next(1, 100).ToString();
            }));


        }

        /// <summary>
        /// 初始化采集器
        /// </summary>
        private void initDevice()
        {
            int nRet = 0;
            nRet = ZK_FingerDll.LIVESCAN_Init();
            if (1 != nRet)//初始化采集失败
            {
                MessageBox.Show("初始化采集器失败,请检查设备是否正确连接", "提示信息", MessageBoxButton.OK, MessageBoxImage.Error);

                //MessageBox.Show("初始化采集器失败", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
                return;
            }
            else
            {
                //MessageBox.Show("初始化采集器成功");
                isOpen = true;
            }
            nRet = ZK_FingerDll.FP_Begin();//初始化比对操作（指纹比对算法）
            if (1 == nRet)//初始化成功
            {
                //指纹比对
                // MessageBox.Show("初始化比对器成功");
            }
            else
            {
                MessageBox.Show("初始化比对器失败");
                return;
            }
            m_bTimeToStop = false;
            m_threadCapture = new Thread(new ThreadStart(ThreadCapture));
            m_threadCapture.Name = "指纹线程";
            m_threadCapture.IsBackground = true;//后台线程
            m_threadCapture.Start();
        }
        private void ThreadCapture()
        {
            while (!m_bTimeToStop)
            {
                IntPtr pRawImage = Marshal.AllocHGlobal(256 * 360);
                //实例化一个计时器
                //Stopwatch watch = new Stopwatch();
                ////開始计时
                //watch.Start();
                ZK_FingerDll.LIVESCAN_BeginCapture(0);

                int nRet = 0;
                if (0 < (nRet = ZK_FingerDll.LIVESCAN_GetFPRawData(0, pRawImage)))
                {
                    try
                    {
                        if (IntPtr.Zero != pRawImage)
                        {
                            byte nScore = 0;
                            unsafe
                            {
                                if (1 == ZK_FingerDll.FP_GetQualityScore(pRawImage, &nScore) && nScore > 0)
                                {
                                    double nScore1 = nScore;
                                    byte[] byRawImage = new byte[256 * 360];
                                    Marshal.Copy(pRawImage, byRawImage, 0, 256 * 360);
                                    Bitmap bmp = CreateBitmap(byRawImage, 256, 360);
                                    if (null != bmp)
                                    {
                                        Dispatcher.BeginInvoke(new Action(() =>
                                        {
                                            picFinger.Image = System.Drawing.Image.FromHbitmap(bmp.GetHbitmap());
                                        }));

                                        //IntPtr pFeature = Marshal.AllocHGlobal(512);
                                        //if (1 == ZK_FingerDll.FP_FeatureExtract(225, 0, pRawImage, pFeature))
                                        //{
                                        //    byte[] feaature = new byte[512];
                                        //    Marshal.Copy(pFeature, feaature, 0, 512);
                                        //    FingerAttribute = feaature;//指纹特征数据
                                        //    File.WriteAllBytes("指纹特征提取字节数组", FingerAttribute);
                                        //}
                                    }
                                    Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        this.progressBar1.Value = nScore1;//指纹质量
                                    }));
                                }
                                else
                                {
                                    Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        this.progressBar1.Value = 0;//指纹质量
                                    }));
                                   
                                    Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        picFinger.Image = null;
                                    }));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                Marshal.FreeHGlobal(pRawImage);
                ZK_FingerDll.LIVESCAN_EndCapture(0);//结束采集
                                                    //结束计时
                                                    //watch.Stop();
                                                    ////获取当前实例測量得出的总执行时间（以毫秒为单位）
                                                    //string time = watch.ElapsedMilliseconds.ToString();
                                                    //Console.WriteLine(time);
            }
        }

        /// <summary>
        /// 获得图像
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            Bitmap bmp = null;
            try
            {
                byte[] rgbBytes = new byte[bytes.Length * 3];

                for (int i = 0; i <= bytes.Length - 1; i++)
                {
                    rgbBytes[(i * 3)] = bytes[i];
                    rgbBytes[(i * 3) + 1] = bytes[i];
                    rgbBytes[(i * 3) + 2] = bytes[i];
                }
                bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                for (int i = 0; i <= bmp.Height - 1; i++)
                {
                    IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                    System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
                }
                bmp.UnlockBits(data);
                return bmp;
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        int j = 0;
        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_rybh.Text))
            {
                MessageBox.Show("请输入人员编号", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (PLANE_LEFT_116.Image == null || PLANE_LEFT_217.Image == null || PLANE_LEFT_318.Image == null || PLANE_LEFT_419.Image == null || PLANE_LEFT_520.Image == null || PLANE_RIGHT_111.Image == null || PLANE_RIGHT_212.Image == null || PLANE_RIGHT_313.Image == null || PLANE_RIGHT_414.Image == null || PLANE_RIGHT_515.Image == null)
            {
                MessageBox.Show("请将指纹信息采集完成");
                return;
            }
            //SplashScreenManager ssm = new SplashScreenManager(this, typeof(Waiting), true, true);
            //ssm.ShowWaitForm();
            //ssm.SetWaitFormCaption("正在处理数据");
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    token = interfaceHelper.GetSJToken();//重新获取token
                }
                for (int i = 1; i <= 10; i++)
                {
                    UpLoadFeatures upLoadFeatures = new UpLoadFeatures();//左手拇指
                    upLoadFeatures.termerId = this.txt_rybh.Text;
                    upLoadFeatures.type = 1.ToString();
                    upLoadFeatures.no = i;
                    switch (i)
                    {
                        case 1:
                            string base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_LEFT_116.bmp");
                            string fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_LEFT_116.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传左手拇指指纹信息异常");
                                // ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;
                            break;
                        case 2:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_LEFT_217.bmp");
                            fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_LEFT_217.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传左手食指指纹信息异常");
                                // ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;
                            break;
                        case 3:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_LEFT_318.bmp");
                            fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_LEFT_318.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传左手中指指纹信息异常");
                                // ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;
                            break;
                        case 4:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_LEFT_419.bmp");
                            fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_LEFT_419.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传左手无名指指纹信息异常");
                                //ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;
                            break;
                        case 5:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_LEFT_520.bmp");
                            fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_LEFT_520.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传左手小拇指指纹信息异常");
                                //ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;
                            break;
                        case 6:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_RIGHT_515.bmp");
                            fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_RIGHT_515.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传右手小拇指指纹信息异常");
                                //ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;

                            break;
                        case 7:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_RIGHT_414.bmp");
                            fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_RIGHT_414.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传右手无名指指纹信息异常");
                                //ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;
                            break;
                        case 8:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_RIGHT_313.bmp");
                            fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_RIGHT_313.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传右手中指指纹信息异常");
                                //ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;
                            break;
                        case 9:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_RIGHT_212.bmp");
                            fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_RIGHT_212.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传右手食指指纹信息异常");
                                //ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;
                            break;
                        case 10:
                            base64File = Base64Helper.FileToBase64Str(App.appStartupPath + "\\Files" + "\\fingers" + "\\" + this.txt_rybh.Text + "\\" + this.txt_rybh.Text + "PLANE_RIGHT_111.bmp");
                            fileId1 = upFile.getFileID(base64File, this.txt_rybh.Text + "PLANE_RIGHT_111.bmp", token);
                            if (string.IsNullOrEmpty(fileId1))
                            {
                                MessageBox.Show("上传右手拇指指纹信息异常");
                                //ssm.CloseWaitForm();
                                return;
                            }
                            upLoadFeatures.fileId = fileId1;
                            break;
                    }
                    string tzID = interfaceHelper.uploadFeatures(JsonConvert.SerializeObject(upLoadFeatures), token);
                    j++;
                }
                // ssm.CloseWaitForm();
                if (j == 10)
                {
                    MessageBox.Show("指纹信息上传成功");
                }
            }
            catch (Exception ex)
            {
                //ssm.CloseWaitForm();
                MessageBox.Show("上传指纹信息异常" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public string fileDir = App.appStartupPath + "//" + "Files/";
        private void PLANE_LEFT_116_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_rybh.Text))
            {
                MessageBox.Show("请输入人员编号", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                if (progressBar1.Value > 50)//进行采集
                {

                    string fgDir = fileDir + "fingers\\";
                    if (!Directory.Exists(fgDir))
                    {
                        Directory.CreateDirectory(fgDir);
                    }
                    string ss = fgDir + this.txt_rybh.Text + "\\";
                    if (!Directory.Exists(ss))
                    {
                        Directory.CreateDirectory(ss);
                    }
                    System.Windows.Forms.PictureBox pic = sender as System.Windows.Forms.PictureBox;
                    byte[] buf = new byte[256 * 360 + 1078];
                    int n = ZK_FingerDll.LIVESCAN_GetFPBmpData(0, buf);//
                    pic.Image = System.Drawing.Image.FromStream(new MemoryStream(buf));
                    pic.Image.Save(ss + this.txt_rybh.Text + pic.Tag.ToString() + ".bmp");
                }
                else
                {
                    App.speak("指纹质量不合格");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常信息：" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PLANE_LEFT_116_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_rybh.Text))
            {
                MessageBox.Show("请输入人员编号", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                string fgDir = fileDir + "fingers\\";
                if (!Directory.Exists(fgDir))
                {
                    Directory.CreateDirectory(fgDir);
                }
                string ss = fgDir + this.txt_rybh.Text + @"\";
                if (!Directory.Exists(ss))
                {
                    Directory.CreateDirectory(ss);
                }
                System.Windows.Forms.PictureBox pic = sender as System.Windows.Forms.PictureBox;
                if (!isOpen)
                {
                    MessageBox.Show("采集器未成功打开", "提示信息");
                    return;
                }
                string pa = App.appStartupPath + @"\Images\" + "Nodata-.bmp";
                Bitmap bitmap = new Bitmap(pa);
                pic.Image = bitmap;
                pic.Image.Save(ss + this.txt_rybh.Text + pic.Tag.ToString() + ".bmp");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btn_readCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //SplashScreenManager ssm = new SplashScreenManager(this, typeof(Waiting), true, true);
                //ssm.ShowWaitForm();
                //ssm.SetWaitFormCaption("正在处理数据");
                cardReader.initDevice();
                IDCardInfo cardInfo = cardReader.readInfo();//读取身份证
                if (cardInfo != null)
                {
                    this.txt_sfzh.Text = cardInfo.idNum;
                    try
                    {
                        if (string.IsNullOrEmpty(token))
                        {
                            token = interfaceHelper.GetSJToken();
                        }
                        GetPersonInfo getPersonInfo = new GetPersonInfo()
                        {
                            idCard = cardInfo.idNum
                        };
                        string result = interfaceHelper.getPersonInfo(JsonConvert.SerializeObject(getPersonInfo), token);
                        InterfaceReturnEntity.Result<PersonInfo> per = JsonConvert.DeserializeObject<InterfaceReturnEntity.Result<PersonInfo>>(result);
                        this.txt_rybh.Text = per.rows[0].termerId;
                        //ssm.CloseWaitForm();
                    }
                    catch (Exception ex)
                    {
                        //ssm.CloseWaitForm();
                        MessageBox.Show("获取人员编号信息异常：" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    //ssm.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void test()
        {



        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



        }
    }
    class GetPersonInfo
    {
        public string idCard { get; set; }
    }
}
