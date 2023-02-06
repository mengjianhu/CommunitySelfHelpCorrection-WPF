using libzkfpcsharp;
using RainHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ZKFingerLive20R
{
    public class UseFinger
    {
        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;
        byte[] FPBuffer;

        const int REGISTER_FINGER_COUNT = 3;

        byte[][] RegTmps = new byte[3][];
        byte[] RegTmp = new byte[2048];
        public byte[] CapTmp = new byte[2048];
        int cbCapTmp = 2048;
        private int mfpWidth = 0;
        private int mfpHeight = 0;
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public UseFinger()
        {
            closeDevice();
            if (initDevice() != 0)
            {
                MessageBox.Show("设备初始化失败", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (openDevice() != 0)
            {
                MessageBox.Show("设备打开失败", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
        /// <summary>
        /// 
        /// 初始化设备 返回 0成功 -1事变
        //
        /// </summary>
        /// <returns></returns>
        public int initDevice()
        {
            try
            {
                int ret = zkfperrdef.ZKFP_ERR_OK;
                mDBHandle = zkfp2.DBInit();
                if ((ret = zkfp2.Init()) == zkfperrdef.ZKFP_ERR_OK)
                {
                    return ret;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public void closeDevice()
        {
            zkfp2.CloseDevice(mDevHandle);
            zkfp2.Terminate();//释放资源
        }

        /// <summary>
        /// 打开设备连接
        /// </summary>
        public int openDevice()
        {
            try
            {
                int ret = zkfp.ZKFP_ERR_OK;
                if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(0)))
                {
                    return -1;
                }
                for (int i = 0; i < 3; i++)
                {
                    RegTmps[i] = new byte[2048];
                }
                byte[] paramValue = new byte[4];
                int size = 4;
                zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);
                zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

                size = 4;
                zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);
                zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

                FPBuffer = new byte[mfpWidth * mfpHeight];

                Thread captureThread = new Thread(new ThreadStart(DoCapture));
                captureThread.IsBackground = true;
                captureThread.Start();
                bIsTimeToDie = false;
                return 0;
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 采集指纹
        /// </summary>
        private void DoCapture()
        {
            try
            {
                while (!bIsTimeToDie)
                {
                    cbCapTmp = 2048;

                    int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);
                   // KK = CapTmp;
                   // int ret1 = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, KK, ref cbCapTmp);
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 得到指纹图形 bitmap图片
        /// </summary>
        /// <returns></returns>
        public Bitmap getBitmap()
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                if (cbCapTmp > 0)
                {
                    BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
                    if (ms != null)
                    {
                        Bitmap bmp = new Bitmap(ms);
                        return bmp;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public int MatchFinger(byte[] temp1, byte[] temp2)
        {
            return zkfp2.DBMatch(mDBHandle, temp1, temp2);
        }
        public byte[] KK= new byte[2048];
     
        public  int  ExtractFromImage()
        {
            int size = 2048;
            string filePath = @"E:\U盘备份\社区自助矫正系统V2201\社区自助矫正系统V2201\bin\x64\Debug\Files\fingers\test\testPLANE_LEFT_116.bmp";
            Bitmap bitmap=new Bitmap(filePath);
          byte [] bb= ImageHelper.BitmapByte(bitmap);
            int i = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, KK, ref cbCapTmp);
            //int i = zkfp2.ExtractFromImage(mDBHandle, filePath, 96,KK,ref size);
            return i;
        }
    }
}
