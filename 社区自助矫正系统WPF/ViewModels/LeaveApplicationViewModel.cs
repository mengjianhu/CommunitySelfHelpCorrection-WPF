using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
 
using HelperUnit;
using InterFaceRequestInfo;
using Microsoft.Win32;
using Newtonsoft.Json;
using Panuon.WPF.UI;
using Prism.Commands;
using Prism.Mvvm;
using RainHelper;

namespace 社区自助矫正系统WPF.ViewModels
{
    public class LeaveApplicationViewModel : BindableBase
    {
        InterfaceHelper interfaceHelper = new InterfaceHelper();//接口相关
        UpFileBase64 upFile = new UpFileBase64();
        public DelegateCommand<object> ExecuteCommand { get; set; }
        public DelegateCommand<object> ExecuteCommand1 { get; set; }
        public DelegateCommand ExecuteCommandFiles { get; set; }

        public DelegateCommand EndTimeChangeCommand { get; set; }
         
        public string token { get; set; }
        public string fileID { get; set; }
        public LeaveApplicationViewModel()
        {
            ExecuteCommand = new DelegateCommand<object>(Execute);
            ExecuteCommand1 = new DelegateCommand<object>(Close);
            ExecuteCommandFiles = new DelegateCommand(ChooseFiles);
            EndTimeChangeCommand=new DelegateCommand(EndTimeChange);
            applyStartTime = DateTime.Now;
            applyEndTime = DateTime.Now;
            Time = DateTime.Now;
            Time1 = DateTime.Now;
        }
        private static int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());

            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());

            TimeSpan sp = end.Subtract(start);

            return sp.Days;
        }
        private void EndTimeChange()
        {
            applyDays= DateDiff(applyStartTime, applyEndTime);
            if (applyDays <= 0  )
            {
                MessageBox.Show("请选择正确的时间" );

                return;
            }
        }

        string path = "";//文件路径
        private void ChooseFiles()
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
                string fileName = System.IO.Path.GetFileName(path);//获取文件名称名称
                this.applyFileIds = fileName;

            }
        }
        private void Close(object obj)
        {
            try
            {
                //关闭身份证事件
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                (obj as Window).Close();
            }

        }

        string pro = "";
        string cit = "";
        string area = "";
        string town = "";
        private void Execute(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(termerId))
                {
                    MessageBoxX.Show("请输入人员编号或者刷身份证 ", "提示信息", MessageBoxButton.OK, MessageBoxIcon.Info, DefaultButton.YesOK, 5);
                    return;
                }
                if (string.IsNullOrEmpty(applyReason))
                {
                    MessageBoxX.Show("请输入申请理由 ", "提示信息", MessageBoxButton.OK, MessageBoxIcon.Info, DefaultButton.YesOK, 5);
                    return;

                }
                if (string.IsNullOrEmpty(applyFileIds))
                {
                    MessageBoxX.Show("请选择上传的附件 ", "提示信息", MessageBoxButton.OK, MessageBoxIcon.Info, DefaultButton.YesOK, 5);
                    return;
                }
                string detaisAdderss = (obj as Window).Title;
                if (!string.IsNullOrEmpty(detaisAdderss))
                {
                    string[] detaisAdderssList = detaisAdderss.Split(",");
                    if (detaisAdderssList.Length != 4)
                    {
                        MessageBoxX.Show("请选择目的地信息 ", "提示信息", MessageBoxButton.OK, MessageBoxIcon.Info, DefaultButton.YesOK, 5);
                        return;
                    }

                    pro = detaisAdderssList[0];
                    cit = detaisAdderssList[1];
                    area = detaisAdderssList[2];
                    town = detaisAdderssList[3];
                    if (string.IsNullOrEmpty(pro) || string.IsNullOrEmpty(cit) || string.IsNullOrEmpty(area) || string.IsNullOrEmpty(town))
                    {
                        MessageBoxX.Show("请选择目的地信息 ", "提示信息", MessageBoxButton.OK, MessageBoxIcon.Info, DefaultButton.YesOK, 5);
                        return;
                    }
                }
                if (string.IsNullOrEmpty(destinationDetail))
                {
                    MessageBoxX.Show("请输入详细的地址信息。。。。", "提示信息", MessageBoxButton.OK, MessageBoxIcon.Info, DefaultButton.YesOK, 5);
                    return;
                }

                string fileBase64 = Base64Helper.FileToBase64Str(path);
                string fileName = applyFileIds;
                MessageBoxResult result = MessageBox.Show("确定要保存吗？", "温馨提示", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var handler = PendingBox.Show("正在上传数据", "提示信息");
                    try
                    {
                        if (string.IsNullOrEmpty(token))
                        {
                            token = interfaceHelper.GetSJToken();//获取token
                        }
                        fileID = upFile.getFileID(fileBase64, fileName, token);//获取文件编号
                        handler.Close();
                        if (string.IsNullOrEmpty(fileID))
                        {
                            MessageBox.Show("附件信息上传异常", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        handler.Close();
                        MessageBox.Show("附件信息上传异常:" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    LeaveSubmit leaveSubmit = new LeaveSubmit()
                    {
                        termerId = termerId,
                        applyDays = applydays,
                        applyStartTime = applyStartTime,
                        applyEndTime = applyEndTime,
                        applyFileIds = fileID,
                        applyReason = applyReason,
                        destinationSelIdL1 = destinationSelIdL1,
                        destinationSelIdL2 = destinationSelIdL2,
                        destinationSelIdL3 = destinationSelIdL3,
                        destinationSelIdL4 = destinationSelIdL4,
                        destinationDetail = destinationDetail
                    };
                    string josnRes = JsonConvert.SerializeObject(leaveSubmit);
                    var handler1 = PendingBox.Show("正在上传数据", "提示信息");
                    try
                    {
                        if (string.IsNullOrEmpty(token))
                        {
                            token = interfaceHelper.GetSJToken();//获取token
                        }
                        string res = interfaceHelper.UploadSubmitAddressChange(josnRes, token);
                        handler1.Close();
                        MessageBox.Show(res);
                    }
                    catch (Exception ex)
                    {
                        handler1.Close();
                        MessageBox.Show("请假信息上传异常：" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxX.Show("异常信息" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxIcon.Info, DefaultButton.YesOK, 5);
            }
        }
        //private void upLoadFileData()
        //{
        //    if (string.IsNullOrEmpty(path))
        //    {
        //        return;
        //    }
        //    MessageBox.Show(path);
        //    string base64File = Base64Helper.FileToBase64Str(path);
        //    MessageBox.Show(base64File.Length.ToString());
        //}

        #region 参数

        private DateTime _date;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime applyStartTime
        {
            get { return _date; }
            set { _date = value; RaisePropertyChanged(); }
        }
        private DateTime _date1;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime applyEndTime
        {
            get { return _date1; }
            set { _date1 = value; RaisePropertyChanged(); }
        }
        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set { time = value; RaisePropertyChanged(); }
        }
        private DateTime time1;

        public DateTime Time1
        {
            get { return time1; }
            set { time1 = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 人员编号
        /// </summary>
        private string termerid;
        public string termerId
        {
            get { return termerid; }
            set { termerid = value; RaisePropertyChanged(); }
        }

        private int applydays;
        /// <summary>
        /// 请假天数
        /// </summary>
        public int applyDays
        {
            get { return applydays; }
            set { applydays = value; RaisePropertyChanged(); }
        }
        private string appreasn;
        /// <summary>
        /// 请假理由
        /// </summary>
        public string applyReason
        {
            get { return appreasn; }
            set { appreasn = value; RaisePropertyChanged(); }
        }
        private string applyfileIds;

        public string applyFileIds
        {
            get { return applyfileIds; }
            set { applyfileIds = value; RaisePropertyChanged(); }
        }

        private string destinationselIdL1;

        public string destinationSelIdL1
        {
            get { return destinationselIdL1; }
            set { destinationselIdL1 = value; RaisePropertyChanged(); }
        }
        private string destinationselIdL2;

        public string destinationSelIdL2
        {
            get { return destinationselIdL2; }
            set { destinationselIdL2 = value; RaisePropertyChanged(); }
        }
        private string destinationselIdL3;

        public string destinationSelIdL3
        {
            get { return destinationselIdL3; }
            set { destinationselIdL3 = value; RaisePropertyChanged(); }
        }
        private string destinationselIdL4;

        public string destinationSelIdL4
        {
            get { return destinationselIdL4; }
            set { destinationselIdL4 = value; RaisePropertyChanged(); }
        }
        private string destinationdetail;

        public string destinationDetail
        {
            get { return destinationdetail; }
            set { destinationdetail = value; RaisePropertyChanged(); }
        }
        private string idCard1;

        public string idCard
        {
            get { return idCard1; }
            set { idCard1 = value; RaisePropertyChanged(); }
        }
        #endregion
    }
}
