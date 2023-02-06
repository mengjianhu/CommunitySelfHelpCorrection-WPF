using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 社区自助矫正系统WPF.ViewModels
{
    public class JZXXCCViewModel : NavigationViewModel
    {
        public DelegateCommand<string> ExecuteCommand { get; set; }
        public JZXXCCViewModel(IContainerProvider provider) : base(provider)
        {
            ExecuteCommand = new DelegateCommand<string>(Excute);
        }

        private void Excute(string obj)
        {
            switch (obj)
            {
                case "queryJzddxxByIdCard":
                    queryJzddxxByIdCard();
                    break;
            }
        }
        /// <summary>
        /// 根据身份证号查询数据
        /// </summary>
        private void queryJzddxxByIdCard()
        {
            if (string.IsNullOrEmpty(idCard))
            {
                MessageBox.Show("请输入身份证号或刷身份证", "提示信息", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (idCard.Length != 18)
            {
                MessageBox.Show("请输入正确的身份证号", "提示信息", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            name = "李四";
            DateTime dateTime = DateTime.Now;
            createTime = dateTime;
        }
        #region 参数
        private string idcard;
        public string idCard
        {
            get { return idcard; }
            set { idcard = value; RaisePropertyChanged(); }
        }
        private string termerid;
        /// <summary>
        /// 人员编号
        /// </summary>
        public string termerId
        {
            get { return termerid; }
            set { termerid = value; RaisePropertyChanged(); }
        }
        private string name1;
        /// <summary>
        /// 姓名
        /// </summary>
        public string name
        {
            get { return name1; }
            set { name1 = value; RaisePropertyChanged(); }
        }
        private string termerstatus;
        /// <summary>
        /// 矫正状态
        /// </summary>
        public string termerStatus
        {
            get { return termerstatus; }
            set { termerstatus = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 头像文件
        /// </summary>
        private string avatarfile;

        public string avatarFile
        {
            get { return avatarfile; }
            set { avatarfile = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 新增时间 2022-03-15 19:06:28
        /// </summary>
        private DateTime? createtime;

        public DateTime? createTime
        {
            get { return createtime; }
            set { createtime = value; RaisePropertyChanged(); }
        }
        #endregion
        /// <summary>
        /// 导航进入事件
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //MessageBox.Show("OnNavigatedToFromJzxx1");
        }
        /// <summary>
        /// 导航离开事件
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
           // MessageBox.Show("OnNavigatedFromJzxx2");
        }
    }

}