using HelperUnit;
using InterFaceRequestInfo;
using InterFaceRequestInfoService;
using InterfaceReturnEntity;
using Newtonsoft.Json;
using Panuon.WPF.UI;
using Prism.Events;
using RainHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VoiceHelper;
using 社区自助矫正系统WPF.Common.Events;
using 社区自助矫正系统WPF.Extensions;
using 社区自助矫正系统WPF.ViewModels;

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// VoiceCJView.xaml 的交互逻辑
    /// </summary>
    public partial class VoiceCJView : Window
    {

        RecordVioce voice;
        VoicePlay voicePlay;
        public string vocalDir { get; set; }
        public VoiceCJView()
        {
            InitializeComponent();
            //WindowState = WindowState.Maximized;
            voice = new RecordVioce(this.DrawPanel);
            voicePlay = new VoicePlay();
            if (string.IsNullOrEmpty(vocalDir))
            {
                vocalDir = App.appStartupPath + "\\" + "Files" + "\\vocals\\";
            }
            this.btn_play.IsEnabled = false;
        }
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cj_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_sfzh.Text.Trim()))
            {
                MessageBox.Show("请输入身份证号", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (btn_cj.Content.ToString() == "开始采集")
            {
                if (!Directory.Exists(vocalDir))
                {
                    Directory.CreateDirectory(vocalDir);
                }
                voice.record(vocalDir + txt_sfzh.Text.Trim() + ".wav");
                btn_play.IsEnabled = false;
                btn_cj.Content = "停止采集";
            }
            else
            {
                voice.stopRecord();
                btn_play.IsEnabled = true;
                btn_cj.Content = "开始采集";
            }
        }
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            if (btn_play.Content.ToString() == "播放样本")
            {
                voicePlay.playVoice(vocalDir + txt_sfzh.Text.Trim() + ".wav");
                voicePlay.PlayStoppedEvent += VoicePlay_PlayStoppedEvent;
                btn_play.Content = "停止播放";
            }
            else
            {
                voicePlay.stop();
                btn_play.Content = "播放样本";
            }
        }

        private void VoicePlay_PlayStoppedEvent(object sender, EventArgs e)
        {
            btn_play.Content = "播放样本";
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                voicePlay?.stop();
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Close();
            }

        }
        /// <summary>
        /// 获取人员编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_getRybh_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_sfzh.Text.Trim()))
            {
                MessageBox.Show("请放置身份证");
                return;
            }
        //    interfaceHelper.getPersonInfo(this.txt_sfzh.Text);
        }
        public string token { get; set; }
        InterfaceHelper interfaceHelper = new InterfaceHelper();//接口相关
        UpFileBase64 upFileBase64 = new UpFileBase64();

        #region 数据上传
        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btn_uploadData_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(this.txt_rybh.Text))
        //        {
        //            MessageBox.Show("人员编号不能为空", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
        //            return;

        //        }
        //        if (!File.Exists(App.appStartupPath + "\\" + "Files\\" + "vocals\\" + this.txt_rybh.Text + ".wav"))
        //        {
        //            MessageBox.Show("未找到声纹信息文件，请重新采集", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
        //            return;
        //        }
        //        MessageBoxResult msresult = MessageBox.Show("确定要保存吗？", "温馨提示", MessageBoxButton.YesNo);
        //        if (msresult == MessageBoxResult.Yes)
        //        {                   
        //            var handler = PendingBox.Show("正在上传声纹信息", "提示信息");
        //            try
        //            {
        //                string base64 = Base64Helper.FileToBase64Str(App.appStartupPath + "\\" + "Files\\" + "vocals\\" + this.txt_rybh.Text + ".wav");
        //                string fileName = this.txt_rybh.Text + ".wav";

        //                if (string.IsNullOrEmpty(token))
        //                {
        //                    token = interfaceHelper.GetSJToken();
        //                }
        //                string result = upFileBase64.getFileID(base64, fileName, token);
        //                //MessageBox.Show("声纹编号：" + result);
        //                UpLoadFeatures upLoadFeatures = new UpLoadFeatures
        //                {
        //                    termerId = this.txt_rybh.Text.Trim(),
        //                    type = 2.ToString(),
        //                    no = 1,
        //                    fileId = result
        //                };
        //                string data = JsonConvert.SerializeObject(upLoadFeatures);
        //                //MessageBox.Show("声纹数据" + data);
        //                string ss = interfaceHelper.uploadFeatures(data, token);
        //                handler.Close();
        //                MessageBox.Show(ss);
        //                return;
        //            }
        //            catch (Exception ex)
        //            {
        //                handler.Close();
        //                MessageBox.Show("上传声纹信息异常，请重新上传:" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("上传声纹信息异常，请重新上传:" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }

        //}
        #endregion

        
         
        // 数据保存本地 后通过手动上传
        private async void btn_uploadData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt_sfzh.Text))
                {
                    MessageBox.Show("身份证号不能为空", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;

                }
                string voicePath = App.appStartupPath + "\\" + "Files\\" + "vocals\\" + this.txt_sfzh.Text + ".wav";
                if (!File.Exists(voicePath))
                {
                    MessageBox.Show("未找到声纹信息文件，请重新采集", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                MessageBoxResult msresult = MessageBox.Show("确定要保存吗？", "温馨提示", MessageBoxButton.YesNo);
                if (msresult == MessageBoxResult.Yes)
                {
                    var handler = PendingBox.Show("正在保存声纹信息", "提示信息");
                    try
                    {
                        VoiceInfoSubmitService upLoadFeaturesVoiceService = new VoiceInfoSubmitService();
                        UpLoadFeaturesVoice upLoadFeaturesVoice = new UpLoadFeaturesVoice()
                        {
                            filePath = voicePath,
                            idNum = txt_sfzh.Text
                        };
                        List<UpLoadFeaturesVoice> upLoadFeaturesVoices = await upLoadFeaturesVoiceService.findAllByIdNum(this.txt_sfzh.Text);
                        if (upLoadFeaturesVoices.Count > 0)
                        {
                            await upLoadFeaturesVoiceService.delList(upLoadFeaturesVoices);
                        }
                        int i = await upLoadFeaturesVoiceService.add(upLoadFeaturesVoice);
                        handler.Close();
                        if (i == 1)
                            MessageBox.Show("保存成功");
                        return;
                    }
                    catch (Exception ex)
                    {
                        handler.Close();
                        MessageBox.Show("异常信息，请重新上传" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常信息，请重新上传:" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
