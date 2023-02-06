using FaceMatchDll;
using FaceMatchDll.Models;
using Newtonsoft.Json;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;
using 社区自助矫正系统WPF.Common;
using 社区自助矫正系统WPF.ViewModels;
using 社区自助矫正系统WPF.ViewModels.XXCXModels;
using 社区自助矫正系统WPF.Views;
using 社区自助矫正系统WPF.Views.XXCJViews;

namespace 社区自助矫正系统WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        public readonly static int indexvide0 = 0;
        public readonly static string appStartupPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        static SpeechSynthesizer speech = new SpeechSynthesizer();
        #region 人脸
        static string appId = "CWcRRZqtBsmnyhcQLcH8ocZ5WHnz9813kdjuk4KLtBYV";

        static string sdkKey32 = "ENNwTMnAHLpRePKoTe2WUbXqjvKA7JHBPvbtcdzVJWxu";
        public static string sdkKey64 = "ENNwTMnAHLpRePKoTe2WUbXqoAYQbkXE6vBXHp2uFugQ";
        /// <summary>
        /// 引擎Handle
        /// </summary>
        public static IntPtr pImageEngine = IntPtr.Zero;
        /// <summary>
        /// 视频引擎Handle
        /// </summary>
        public static IntPtr pVideoEngine = IntPtr.Zero;
        /// <summary>0
        /// RGB视频引擎 FR Handle 处理   FR和图片引擎分开，减少强占引擎的问题
        /// </summary>
        public static IntPtr pVideoRGBImageEngine = IntPtr.Zero;
        ///// <summary>
        ///// IR视频引擎 FR Handle 处理   FR和图片引擎分开，减少强占引擎的问题
        ///// </summary>
        public static IntPtr pVideoIRImageEngine = IntPtr.Zero;
        #endregion
        public static readonly string provString = File.ReadAllText(App.appStartupPath + "/Josns/priovices.json");
        public static readonly string cityString = File.ReadAllText(App.appStartupPath + "/Josns/citys.json");
        public static readonly string areaString = File.ReadAllText(App.appStartupPath + "/Josns/areas.json");
        public static readonly string townString = File.ReadAllText(App.appStartupPath + "/Josns/towns.json");
        protected override Window CreateShell()
        {

            //new Authorization.EncryptionHelper().isLicense();
            //DispatcherUnhandledException += App_DispatcherUnhandledException;//异常处理
            InitEngines();
            return Container.Resolve<MainView>();
        }
        //private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        //{
        //    MessageBox.Show("异常信息", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
        //    e.Handled = true;
        //}
        /// <summary>
        /// 像容器中注册
        /// </summary>
        /// <param name="containerRegistry"></param>
        //
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<JZXXCCView, JZXXCCViewModel>();
            containerRegistry.RegisterForNavigation<EduInfoView, EduInfoViewModel>();
            containerRegistry.RegisterForNavigation<AttendanceRecordView, AttendanceRecordViewModel>();
            containerRegistry.RegisterForNavigation<QXJXXView, QXJXXViewModel>();
            containerRegistry.RegisterForNavigation<ZXDBGXXView, ZXDBGXXViewModel>();

        }
        
        public static void InitEngines()
        {
            //判断CPU位数
            //var is64CPU = Environment.Is64BitProcess;
            //if (!is64CPU)
            //{
            //    throw new Exception("CPU不是64位");
            //}
            if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(sdkKey32))
            {
                throw new Exception("授权码不正确");
            }
            //在线激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
            int retCode = 0;
            try
            {
                retCode = ASFFunctions.ASFActivation(appId, sdkKey32);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("无法加载 DLL"))
                {
                    throw new Exception("请将sdk相关DLL放入bin对应的x86或x64下的文件夹中");
                }
                else
                {
                    throw new Exception("激活引擎失败!");
                }
            }

            //初始化引擎
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            //Video模式下检测脸部的角度优先值
            int videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //Image模式下检测脸部的角度优先值
            int imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY;
            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            int detectFaceScaleVal = 16;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 5;
            //引擎初始化时需要初始化的检测功能组合
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);
            Console.WriteLine("引擎初始化成功 Result:" + retCode);
            //初始化视频模式下人脸检测引擎
            uint detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
            retCode = ASFFunctions.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine);
            //RGB视频专用FR引擎
            detectFaceMaxNum = 1;
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS;


        }
        /// <summary>
        /// 异步语音播报
        /// </summary>
        /// <param name="msg"></param>
        public static void speak(string msg)
        {
            speech.SpeakAsyncCancelAll();//停止没有播放完成的语音
            speech.SpeakAsync(msg);//播放语音
        }
        /// <summary>
        /// 停止语音播报
        /// </summary>
        public static void speakStop()
        {
            speech.SpeakAsyncCancelAll();//停止没有播放完成的语音            
        }
    }



}
