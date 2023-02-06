using HelperUnit;
using InterFaceRequestInfo;
using Microsoft.Win32;
using Newtonsoft.Json;
using Panuon.WPF.UI;
using Prism.Commands;
using Prism.Ioc;
using RainHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 社区自助矫正系统WPF.ViewModels
{
    public class AddressChangeViewModel : NavigationViewModel
    {
        InterfaceHelper interfaceHelper = new InterfaceHelper();//接口相关
        UpFileBase64 upFile = new UpFileBase64();
        public DelegateCommand<object> ExecuteCommand { get; set; }
        public DelegateCommand ExecuteCommandFiles { get; set; }
        public DelegateCommand<object> ExecuteCommandClose { get; set; }
        public List<CheckBool> comSfqczsw { get; set; }
        /// <summary>
        /// 附件编号
        /// </summary>
        private string fileID = "";//上传附件所得到的附件编号
        /// <summary>
        /// token 值
        /// </summary>
        public string token { get; set; }

        public AddressChangeViewModel(IContainerProvider provider) : base(provider)
        {
            ExecuteCommand = new DelegateCommand<object>(Exercute);
            ExecuteCommandClose = new DelegateCommand<object>(CloseForm);
            ExecuteCommandFiles = new DelegateCommand(ChooseFiles);
            createCheckBooksVals();
        }
        string path = "";
        /// <summary>
        /// 选择附件
        /// </summary>
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
                string name = System.IO.Path.GetFileName(path);//获取文件名称名称
                this.applyFileIds = name;
            }
        }

        //private void ExportEndCommand(string provinceCode, string cityCode, string areaCode, string townCode, string provinceName, string cityName, string areaName, string townName)
        //{
        //    MessageBox.Show("123");
        //}


        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="obj"></param>
        private void CloseForm(object obj)
        {
            try
            {

            }
            catch (Exception)
            {

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
        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="obj"></param>
        private void Exercute(object obj)
        {
            if (string.IsNullOrEmpty(termerId))
            {
                MessageBoxX.Show("请输入人员编号或者刷身份证", "提示信息", MessageBoxButton.OK, MessageBoxIcon.Info, DefaultButton.YesOK, 5);

                return;
            }
            if (string.IsNullOrEmpty(applyReason))
            {
                MessageBox.Show("请输入申请理由");
                return;
            }
            if (string.IsNullOrEmpty(applyFileIds))
            {
                MessageBox.Show("请选择上传的附件");
                return;
            }
            string detaisAdderss = (obj as Window).Title;
            if (!string.IsNullOrEmpty(detaisAdderss))
            {
                string[] detaisAdderssList = detaisAdderss.Split(",");
                if (detaisAdderssList.Length != 4)
                {
                    MessageBox.Show("请选择目的地信息");
                    return;
                }

                pro = detaisAdderssList[0];
                cit = detaisAdderssList[1];
                area = detaisAdderssList[2];
                town = detaisAdderssList[3];
                if (string.IsNullOrEmpty(pro) || string.IsNullOrEmpty(cit) || string.IsNullOrEmpty(area) || string.IsNullOrEmpty(town))
                {
                    MessageBox.Show("请选择目的地信息");
                    return;
                }
            }
            if (string.IsNullOrEmpty(destinationDetail))
            {
                MessageBox.Show("请输入详细的地址信息。。。。");
                return;
            }


            MessageBoxResult result = MessageBox.Show("确定要保存吗？", "温馨提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var handler = PendingBox.Show("正在上传文件信息", "提示信息");
                try
                {
                    string fileBase64 = Base64Helper.FileToBase64Str(path);
                    string fileName = applyFileIds;
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
                try
                {
                    AddressChangeSubmit addressChangeSubmit = new AddressChangeSubmit()
                    {
                        termerId = termerId,
                        applyReason = applyReason,
                        applyFileIds = fileID,
                        destinationOutProvince = selectIndexVal,
                        destinationSelIdL1 = pro,
                        destinationSelIdL2 = cit,
                        destinationSelIdL3 = area,
                        destinationSelIdL4 = town,
                        destinationDetail = destinationDetail
                    };
                    string josnRes = JsonConvert.SerializeObject(addressChangeSubmit);
                    var handler1 = PendingBox.Show("正在上传数据。。。", "提示信息");
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
                        MessageBox.Show("执行地变更信息上传异常：" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("执行地变更信息上传异常：" + ex.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public CheckBool selectItemVall { get; set; }
        public string selectIndexVal { get; set; }
        public void createCheckBooksVals()
        {
            comSfqczsw = new List<CheckBool>();
            comSfqczsw.Add(new CheckBool()
            {
                name = "否",
                id = 0.ToString()
            });
            comSfqczsw.Add(new CheckBool()
            {
                name = "是",
                id = 1.ToString()
            });
            selectItemVall = comSfqczsw[0];
            selectIndexVal = comSfqczsw[0].id;
        }
        public class CheckBool
        {
            public string name { get; set; }
            public string id { get; set; }
        }
        #region 参数



        private string termerId1;
        public string termerId
        {
            get { return termerId1; }
            set { termerId1 = value; RaisePropertyChanged(); }
        }

        private string applyReason1;

        public string applyReason
        {
            get { return applyReason1; }
            set { applyReason1 = value; RaisePropertyChanged(); }
        }

        private string applyFileIds1;

        public string applyFileIds
        {
            get { return applyFileIds1; }
            set { applyFileIds1 = value; RaisePropertyChanged(); }
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
