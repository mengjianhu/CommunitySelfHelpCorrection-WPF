using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZKFinger
{
    public class ZK_Finger
    {

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        //身份证采集库
        /// <summary>
        /// 初始化采集器
        /// </summary>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_Init();
        /// <summary>
        /// 释放采集器
        /// </summary>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_Close();
        /// <summary>
        /// 获得采集器通道数
        /// </summary>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_GetChannelCount();
        /// <summary>
        /// 设置采集器当前的亮度
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <param name="nBright">亮度，范围0-255</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_SetBright(int nChannel, int nBright);
        /// <summary>
        /// 设置采集器当前对比度
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <param name="nContrast">对比度，范围0-255</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_SetContrast(int nChannel, int nContrast);
        /// <summary>
        /// 获得采集器当前亮度
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <param name="nBright">存放当前亮度的整形指针</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int LIVESCAN_GetBright(int nChannel, int* nBright);
        /// <summary>
        /// 获得采集器当前对比度
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <param name="nContrast">存放当前对比度</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int LIVESCAN_GetContrast(int nChannel, int* nContrast);
        /// <summary>
        /// 获得采集器可采集图像的宽度、高度的最大值
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <param name="nOutWidth">图像的宽度</param>
        /// <param name="nOutHeight">图像的高度</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int LIVESCAN_GetMaxImageSize(int nChannel, int* nOutWidth, int* nOutHeight);
        /// <summary>
        /// 获得采集器当前图像的采集位置、宽度和高度。当前图像宽度初始值为 256，高度初始值为 360。
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <param name="pnOriginX">存放图像采集窗口的采集原点坐标X值</param>
        /// <param name="pnOriginY">存放图像采集窗口的采集原点坐标Y值</param>
        /// <param name="pnWidth">采集图像的宽度</param>
        /// <param name="pnHeight">采集图像的高度</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int LIVESCAN_GetCaptWindow(int nChannel, int* pnOriginX, int* pnOriginY, int* pnWidth, int* pnHeight);
        /// <summary>
        /// 设置采集器当前图像的次啊及位置、宽度和高度
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <param name="nOriginX">图像采集窗口的采集原点坐标X值</param>
        /// <param name="nOriginY">图像采集窗口的采集原点坐标Y值</param>
        /// <param name="nWidth">采集图像的宽度。对于居民身份证用单指指纹采集，应大于等于256.</param>
        /// <param name="nHeight">采集图像的高度。对于居民身份证用单指指纹采集，应大于等于360</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int LIVESCAN_SetCaptWindow(int nChannel, int nOriginX, int nOriginY, int nWidth, int nHeight);
        /// <summary>
        /// 调用采集器的属性设置对话框。
        ///此函数弹出一个模式对话框，用户可以设置除去对比度、亮度、采集窗口参数外的其它参数，如GAMMA 值等, 使得设置适合采集器本身的特点。
        /// </summary>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_Setup();
        /// <summary>
        /// 准备采集一帧图像
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_BeginCapture(int nChannel);
        /// <summary>
        /// 采集一帧图像
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <param name="pRawImage">存放采集数据的内存块。</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int LIVESCAN_GetFPRawData(int nChannel, IntPtr pRawImage);
        /// <summary>
        /// 采集一帧BMP格式图像
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <param name="pBmpData">指向存放 8 位灰度 BMP 格式采集数据的内存块，调用者分配。返回 8 位灰度 BMP 格式图像数据。大小应为：当前图像采集宽度×当前图像采集高度+1078。</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int LIVESCAN_GetFPBmpData(int nChannel, byte[] pBmpData);
        /// <summary>
        /// 结束采集一帧图像或预览图像
        /// </summary>
        /// <param name="nChannel">通道号</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_EndCapture(int nChannel);
        /// <summary>
        /// 采集器是否支持设置对话框
        /// </summary>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_IsSupportSetup();
        /// <summary>
        /// 取得接口规范的版本
        /// </summary>
        /// <returns></returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_GetVersion();
        /// <summary>
        /// 获取接口规范的说明
        /// </summary>
        /// <param name="pszDesc">接口说明</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_GetDesc(string pszDesc);
        /// <summary>
        /// 获取采集接口错误信息
        /// </summary>
        /// <param name="nErrorNo">错误代码(<0)</param>
        /// <param name="pszErrorInfo">存放错误信息</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int LIVESCAN_GetErrorInfo(int nErrorNo, string pszErrorInfo);
        /// <summary>
        /// 设置存放采集数据的内存为空
        /// </summary>
        /// <param name="pImageData">存放采集数据的内存块</param>
        /// <param name="imageLength">存放采集数据的内存长度</param>
        /// <returns></returns>
        [DllImport("ID_FprCap.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int LIVESCAN_SetBufferEmpty(byte* pImageData, long imageLength);

        //身份证算法库
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <param name="code">版本信息格式xxYY</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int FP_GetVersion(byte[] code);
        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int FP_Begin();
        /// <summary>
        /// 指纹图像特征提取
        /// </summary>
        /// <param name="cScannerType">指纹采集器代码</param>
        /// <param name="cFingerCode">指位代码</param>
        /// <param name="pFingerImgBuf">指纹图像数据指针，指纹图像为 RAW 格式。</param>
        /// <param name="pFeatureData">指纹特征数据指针，存储生成的指纹特征数据，由调 用者分配内存空间，指纹特征数据文件结构应符合附录 A 要求</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int FP_FeatureExtract(byte cScannerType, byte cFingerCode, IntPtr pFingerImgBuf, IntPtr pFeatureData);
        /// <summary>
        /// 指纹特征数据比对
        /// </summary>
        /// <param name="pFeatureData1">指纹特征数据1</param>
        /// <param name="pFeatureData2">指纹特征数据2</param>
        /// <param name="pfSimilarity">相似度，取值范围为 0.00 ～ 1.00，值 0.00 表示不匹配，值 1.00表示完全匹配。</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int FP_FeatureMatch(byte[] pFeatureData1, byte[] pFeatureData2, ref float pfSimilarity);
        /// <summary>
        /// 指纹图像数据与指纹特征数据比对
        /// </summary>
        /// <param name="pFingerImgBuf">指纹图像数据</param>
        /// <param name="pFeatureData">指纹特征数据</param>
        /// <param name="pfSimilarity">相识度。取值范围为 0.00 ～ 1.00，值 0.00 表示不匹配，值 1.00表示完全匹配</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int FP_ImageMatch(byte[] pFingerImgBuf, byte[] pFeatureData, ref float pfSimilarity);
        /// <summary>
        /// 指纹图像数据压缩
        /// </summary>
        /// <param name="cScannerType">指纹采集器代码</param>
        /// <param name="cEnrolResult">注册结果代码</param>
        /// <param name="cFingerCode">指位代码</param>
        /// <param name="pFingerImgBuf">指纹图像数据</param>
        /// <param name="nCompressRatio">指纹数据压缩倍数</param>
        /// <param name="pCompressedImgBuf">指纹压缩图像数据指针，调用者在调用此函数前，应当分配不小于 20 480 字节的内存，指纹压缩图像数据文件结构应符合附录 A 要求。</param>
        /// <param name="charstrBuf">错误信息，如果压缩图像发生错误，并且返回值为-9 的情况下，strBuf 填写错误信息。错误信息为以数值 0 结尾的字符串，采用 GB 13000 中规定的字符</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int FP_Compress(byte cScannerType, byte cEnrolResult, byte cFingerCode, byte* pFingerImgBuf, int nCompressRatio, byte* pCompressedImgBuf, byte[] charstrBuf);
        /// <summary>
        /// 对指纹图像压缩数据进行复现
        /// </summary>
        /// <param name="pCompressedImgBuf">指纹压缩数据</param>
        /// <param name="pFingerImgBuf">指纹复现图像数据</param>
        /// <param name="charstrBuf">错误信息，如果压缩图像发生错误，并且返回值为-9 的情况下，strBuf 填写错误信息。错误信息为以数值 0 结尾的字符串，采用 GB 13000 中规定的字符。</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int FP_Decompress(byte* pCompressedImgBuf, byte* pFingerImgBuf, byte[] charstrBuf);
        /// <summary>
        /// 指纹图像质量值获取
        /// </summary>
        /// <param name="pFingerImgBuf">指纹图像数据</param>
        /// <param name="pnScore">指纹图像质量值，范围为00H-64H，值01H表示最低质量，值64H表示最高质量，值00H表示未知</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int FP_GetQualityScore(IntPtr pFingerImgBuf, byte* pnScore);

        /// <summary>
        /// 生成“注册失败”指纹特征数据
        /// </summary>
        /// <param name="cScannerType">指纹采集器代码</param>
        /// <param name="cFingerCode">指位代码</param>
        /// <param name="pFeatureData">指纹特征数据</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int FP_GenFeatureFromEmpty1(byte cScannerType, byte cFingerCode, byte* pFeatureData);
        /// <summary>
        /// 生成"未注册"指纹特征数据
        /// </summary>
        /// <param name="cFingerCode">指位代码</param>
        /// <param name="pFeatureData">指纹特征数据</param>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        unsafe public extern static int FP_GenFeatureFromEmpty2(byte cFingerCode, byte* pFeatureData);
        /// <summary>
        /// 结束操作
        /// </summary>
        /// <returns>成功返回1，否则返回错误代码</returns>
        [DllImport("ID_Fpr.dll", CallingConvention = CallingConvention.StdCall)]
        public extern static int FP_End();
        /// <summary>
        /// C# byte数组转换成C++相应的IntPtr
        /// </summary>
        /// <param name="imgBuf"></param>
        /// <returns></returns> 
        public static IntPtr getIntPtrFromByte(byte[] imgBuf)
        {
            unsafe
            {
                fixed (byte* p = imgBuf)
                {
                    IntPtr pRawImage = (IntPtr)p;
                    return pRawImage;

                }
            }

        }

        public static float FeatureMatch(byte[] feature1, byte[] feature2)
        {

            float score = 0.0f;
            if (1 == ZK_Finger.FP_FeatureMatch(feature1, feature2, ref score))
            {
                return score * 100;
            }
            return 0;
        }
        public static byte[] FeatureExtract(byte[] imgBuf)
        {
            IntPtr pFeature = Marshal.AllocHGlobal(512);
            byte[] feature = new byte[512];
            if (1 == FP_FeatureExtract(255, 0, getIntPtrFromByte(imgBuf), pFeature))
            {
                Marshal.Copy(pFeature, feature, 0, 512);
                return feature;
            }
            return null;
        }

    }
}
