using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
namespace 社区自助矫正系统WPF.ViewModels.XXCXModels
{
    public class ZXDBGXXViewModel : NavigationViewModel
    {

        public DelegateCommand<string> ExecuteCommand { get; set; }
        public ZXDBGXXViewModel(IContainerProvider provider) : base(provider)
        {
            ExecuteCommand = new DelegateCommand<string>(Excute);
        }

        private void Excute(string obj)
        {
            switch (obj)
            {
                case "btn_upPage":
                    btn_upPageClick();
                    break;
                case "btn_downPage":
                    btn_downPageClick();
                    break;
                case "btn_queryByTermerId":
                    btn_queryByTermerId();
                    break;
            }
        }
        /// <summary>
        /// 数据查询
        /// </summary>

        private void btn_queryByTermerId()
        {
            if (string.IsNullOrEmpty(termerId))
            {
                MessageBox.Show("请输入人员编号或刷身份证", "提示信息", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
          
            addressInfos = new ObservableCollection<string>();
            for (int i = 0; i < 10; i++)
            {
                addressInfos.Add(i.ToString());
            }
           
        }
        /// <summary>
        /// 上一页
        /// </summary>
        private void btn_upPageClick()
        {
            if (curPage > 1)
            {
                curPage--;
            }
            else
            {
                curPage = 1;
            }

        }
        /// <summary>
        /// 下一页
        /// </summary>
        private void btn_downPageClick()
        {
            curPage++;
        }

        #region 参数
        private int curpage = 0;

        public int curPage
        {
            get { return curpage; }
            set { curpage = value; RaisePropertyChanged(); }
        }


        private string termerid;
        /// <summary>
        /// 矫正人员编号
        /// </summary>
        public string termerId
        {
            get { return termerid; }
            set { termerid = value; }
        }
        private ObservableCollection<string> addressinfos;

        public ObservableCollection<string> addressInfos
        {
            get { return addressinfos; }
            set { addressinfos = value; RaisePropertyChanged(); }
        }

        #endregion
    }
}
