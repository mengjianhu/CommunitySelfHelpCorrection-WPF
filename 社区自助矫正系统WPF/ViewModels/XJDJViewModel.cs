using InterFaceRequestInfo;
using Microsoft.Win32;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RainHelper;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using 社区自助矫正系统WPF.Views;
using Prism.Events;
using Prism.Ioc;
using HelperUnit;
using Panuon.WPF.UI;

namespace 社区自助矫正系统WPF.ViewModels
{

    public class XJDJViewModel : BindableBase
    {
        public DelegateCommand<string> ExecuteCommand { get; set; }
        public DelegateCommand<object> ExecuteCommandClose { get; set; }
        public XJDJViewModel()
        {
            //App.speak("开始销假申请登记");
            ExecuteCommand = new DelegateCommand<string>(Execute);
            ExecuteCommandClose = new DelegateCommand<object>(closeForm);
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="obj"></param>
        private void closeForm(object obj)
        {
            try
            {
                //关闭身份证信息
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                App.speakStop();
                (obj as Window).Close();
            }

        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "btn_submitXJDJ": btn_submitXJDJ(); break;
                case "openFileDialog": OpenFiles(); break;
            }
        }


        string path = "";
        /// <summary>
        /// 打开文件
        /// </summary>
        private void OpenFiles()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择附件";
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = string.Empty;

            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;//文件路径
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }
                string name = System.IO.Path.GetFileName(path);//获取文件名称名称
                this.fileName = name;
            }
        }
        public string token { get; set; }
        public string fileID { get; set; }
        
        private string _fileName;

        public string fileName
        {
            get { return _fileName; }
            set { _fileName = value; RaisePropertyChanged(); }
        }

        InterfaceHelper interfaceHelper = new InterfaceHelper();//接口相关
        UpFileBase64 upFile = new UpFileBase64();
        private void btn_submitXJDJ()
        {

            if (string.IsNullOrEmpty(IdNum.Trim()))
            {
                MessageBox.Show("请放置身份证", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBoxResult Msresult = MessageBox.Show("确定要保存吗？", "温馨提示", MessageBoxButton.YesNo);
            if (Msresult == MessageBoxResult.Yes)
            {
                var handler = PendingBox.Show("正在处理数据", "提示信息");
                try
                {
                    if (string.IsNullOrEmpty(token))
                    {
                        // token = interfaceHelper.GetSJToken();//获取token
                    }
                    if (!string.IsNullOrEmpty(reportBackFileIds))
                    {
                        if (string.IsNullOrEmpty(path))
                        {
                            return;
                        }
                        try
                        {
                            string result = upFile.getFileID(Base64Helper.FileToBase64Str(path), fileName, token);//获取文件编号
                            this.fileID = result;//文件编号
                            if (string.IsNullOrEmpty(fileID))
                            {
                                MessageBox.Show("附件信息上传异常", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            handler.Close();
                            MessageBox.Show("信息上传异常：" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                    CancelLeave cancelLeave = new CancelLeave()
                    {
                        termerId = termerId,
                        leaveApplyId = leaveApplyId,
                        reportBackFileIds = fileID
                    };
                    string jsonRequestData = JsonConvert.SerializeObject(cancelLeave);
                    string res1 = interfaceHelper.UploadSubmitCancelLeave(jsonRequestData, token);
                    handler.Close();
                    MessageBox.Show(res1, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    handler.Close();
                    MessageBox.Show("上传销假信息异常，请重新上传"+ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        private string rybh = "";
        /// <summary>
        /// 人员编号
        /// </summary>
        public string termerId
        {
            get { return rybh; }
            set { rybh = value; RaisePropertyChanged(); }
        }

        private string sfzh = "";
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdNum
        {
            get { return sfzh; }
            set { sfzh = value; RaisePropertyChanged(); }
        }

        private string leaveapplyid;

        public string leaveApplyId
        {
            get { return leaveapplyid; }
            set { leaveapplyid = value; RaisePropertyChanged(); }
        }
        private string reportBackFileId;

        public string reportBackFileIds
        {
            get { return reportBackFileId; }
            set { reportBackFileId = value; RaisePropertyChanged(); }
        }
    }
}
