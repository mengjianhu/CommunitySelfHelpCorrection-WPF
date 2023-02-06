using InterfaceReturnEntity;
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
    public class QXJXXViewModel : NavigationViewModel
    {
        public DelegateCommand<string> ExecuteCommand { get; set; }

        public DelegateCommand DisplayPriceCommand { get; set; }
        private ObservableCollection<getLeaveListInfo> getLeaveListInfo;
        public ObservableCollection<getLeaveListInfo> getLeaveListInfos
        {
            get { return getLeaveListInfo; }
            set { getLeaveListInfo = value; RaisePropertyChanged(); }
        }

        public QXJXXViewModel(IContainerProvider provider) : base(provider)
        {
            ExecuteCommand = new DelegateCommand<string>(Excute);
            getLeaveListInfos = new ObservableCollection<getLeaveListInfo>();
            //DisplayPriceCommand = new DelegateCommand(Exce);
        }

        

        private void Excute(string obj)
        {
            switch (obj)
            {
                case "btn_queryByTermerId":
                    btn_queryByTermerId();
                    break;
            }
        }

        private void btn_queryByTermerId()
        {
            for (int i = 0; i < 10; i++)
            {
                getLeaveListInfo getLeaveListInfo11s = new getLeaveListInfo()
                {
                    leaveId = "1",
                    termerId = "1",
                    termerName = "1",
                    orgName = "1",
                    destinationSelText = "1",
                    destination = "1",
                    cumulativeDays = 1,
                    reportBackStatus = 1,
                    reportBackTime = DateTime.Now,
                    reportBackFileIds = "1",
                    applyTime = DateTime.Now,
                    applyFileIds = "1",
                    preApproveStatus = 1,
                    processStatus = 1,
                };
                getLeaveListInfos.Add(getLeaveListInfo11s);
            }
        }
    }
}
