using InterfaceReturnEntity;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 社区自助矫正系统WPF.ViewModels.XXCXModels
{
    public class AttendanceRecordViewModel : NavigationViewModel
    {
        public DelegateCommand<string> ExecuteCommand { get; set; }
        public List<kqlx> comlist { get; set; }

        public ObservableCollection<getHistoryKQLogInfo> historyKQLogInfos { get; set; }

        private ObservableCollection<getHistoryKQLogInfo> historyKQLogInfo22;

        public ObservableCollection<getHistoryKQLogInfo> historyKQLogInfo2
        {
            get { return historyKQLogInfo22; }
            set { historyKQLogInfo22 = value;RaisePropertyChanged(); }
        }

      
        public string selectIndexVal { get; set; }
        public kqlx selectItemVal { get; set; }
        public AttendanceRecordViewModel(IContainerProvider provider) : base(provider)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            createComData();
            historyKQLogInfos = new ObservableCollection<getHistoryKQLogInfo>();
            //historyKQLogInfo2 = new ObservableCollection<getHistoryKQLogInfo>();

        }
        private void createComData()
        {
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
            comlist.Add(new kqlx()
            {
                name = "个别教育",
                id = 2.ToString()
            });
            comlist.Add(new kqlx()
            {
                name = "个别劳动",
                id = 3.ToString()
            });
            selectItemVal = comlist[1];
            selectIndexVal = comlist[0].id;
        }
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "query":
                    query();
                    break;
                case "btn_upPage":
                    btn_upPageClick();
                    break;
                case "btn_downPage":
                    btn_downPageClick();
                    break;
            }
        }
        private void query()
        {
            select();
        }
        /// <summary>
        /// 上一页
        /// </summary>
        private void btn_upPageClick()
        {
            if (pageNumber > 1)
            {
                pageNumber--;
                select();
            }
            else
            {
                pageNumber = 1;
                select();
            }

        }
        /// <summary>
        /// 下一页
        /// </summary>
        private void btn_downPageClick()
        {
            pageNumber++;
            select();
        }

        public void select()
        {
            if (selectIndexVal == 0.ToString())
            {
                if (string.IsNullOrEmpty(concentrateId))
                {
                    MessageBox.Show("集中教育必须输入集中编号");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(identity))
                {
                    MessageBox.Show("请输入身份证号");
                    return;
                }
            }
            getKaoqinRequestEntity getKaoqin = new getKaoqinRequestEntity()
            {
                type = selectIndexVal,
                identity = identity,
                concentrateId = concentrateId,
                pageNumber = pageNumber,
                pageSize = pageSize
            };
            MessageBox.Show(JsonConvert.SerializeObject(getKaoqin));
            for (int i = 0; i < 20; i++)
            {
                getHistoryKQLogInfo getHistoryKQLogInfos = new getHistoryKQLogInfo()
                {
                    id="1",
                    userId="122",
                    userName="张三"+i,
                    startTime=DateTime.Now,
                    endTime= DateTime.Now,
                    authType=1,
                    identity="hao",
                    type="0"
                };
                
                historyKQLogInfos.Add(getHistoryKQLogInfos);
                historyKQLogInfo2 = historyKQLogInfos;

            }
            

        }
        /// <summary>
        /// 查询
        /// </summary>


        #region 参数
        private string type1;
        /// <summary>
        /// 考勤类型
        /// </summary>
        public string type
        {
            get { return type1; }
            set { type1 = value; RaisePropertyChanged(); }
        }

        private string identity1;
        /// <summary>
        /// 身份证号
        /// </summary>
        public string identity
        {
            get { return identity1; }
            set { identity1 = value; RaisePropertyChanged(); }
        }
        private string concentrateId1;
        /// <summary>
        /// 集中编号
        /// </summary>
        public string concentrateId
        {
            get { return concentrateId1; }
            set { concentrateId1 = value; RaisePropertyChanged(); }
        }

        private int pageNumber1 = 1;
        /// <summary>
        /// 页码
        /// </summary>
        public int pageNumber
        {
            get { return pageNumber1; }
            set { pageNumber1 = value; RaisePropertyChanged(); }
        }
        private int pageSize1 = 10;
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize
        {
            get { return pageSize1; }
            set { pageSize1 = value; RaisePropertyChanged(); }
        }

        #endregion
        public class kqlx
        {
            public string id { get; set; }
            public string name { get; set; }
        }


    }
}
