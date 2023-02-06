using InterfaceReturnEntity;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using 社区自助矫正系统WPF.Common;

namespace 社区自助矫正系统WPF.ViewModels.XXCXModels
{
    public class EduInfoViewModel : NavigationViewModel
    {

        public DelegateCommand<string> ExcuteCommand { get; set; }
        private ObservableCollection<getCurrentConcentrateInfo> menuBars;
        public List<kqlx> comlist { get; set; }
        
        /// <summary>
        /// 查询菜单集合
        /// </summary>
        public ObservableCollection<getCurrentConcentrateInfo> EducationInfo
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        private string selectvalindex;

        public string selectValIndex
        {
            get { return selectvalindex; }
            set { selectvalindex = value; RaisePropertyChanged(); }
        }
        public kqlx kk { get; set; }
        public EduInfoViewModel(IContainerProvider provider) : base(provider)
        {
            ExcuteCommand = new DelegateCommand<string>(Excute);
            comlist = new List<kqlx>();
            comlist.Add(new kqlx()
            {
                name = "集中教育",
                id = 0.ToString()
            });
            comlist.Add(new kqlx()
            {
                name = "公益活动",
                id = 1.ToString()
            });
            kk = comlist[0];//选中的项
            selectValIndex = comlist[0].id;//索引
        }

        private void Excute(string obj)
        {
            switch (obj)
            {
                case "queryEndcationInfo": getQueryEndcationInfo(); break;
            }

        }
        /// <summary>
        /// 获取劳动教育信息
        /// </summary>
        private void getQueryEndcationInfo()
        {
            MessageBox.Show(selectValIndex);
            EducationInfo = new ObservableCollection<getCurrentConcentrateInfo>();
            for (int i = 0; i < 100; i++)
            {
                getCurrentConcentrateInfo getCurrentConcentrateInfo1 = new getCurrentConcentrateInfo()
                {
                    id = "a12feee",
                    time = DateTime.Now,
                    content = "测试一测试一测试一测试一测试一测试一测试一测试一er jies",
                    address = "地点一",
                    type = "123",
                    startTime = DateTime.Now,
                    endTime = DateTime.Now

                };
                getCurrentConcentrateInfo getCurrentConcentrateInfo2 = new getCurrentConcentrateInfo()
                {
                    id = "b3645",
                    time = DateTime.Now,
                    content = "测试er",
                    address = "地点er",
                    type = "123",
                    startTime = DateTime.Now,
                    endTime = DateTime.Now
                };
                EducationInfo.Add(getCurrentConcentrateInfo2);
                EducationInfo.Add(getCurrentConcentrateInfo1);
            }
           
          
        }
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //MessageBox.Show("OnNavigatedFrom2");
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
           // MessageBox.Show("OnNavigatedTo1");
        }

        public class kqlx
        {
            public string id{ get; set; }
            public string name { get; set; }
        }
    }
}
