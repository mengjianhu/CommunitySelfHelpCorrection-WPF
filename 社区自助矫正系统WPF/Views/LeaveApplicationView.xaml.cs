using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using 社区自助矫正系统WPF.ViewModels;

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// LeaveApplicationView.xaml 的交互逻辑
    /// </summary>
    public partial class LeaveApplicationView : Window
    {

        public LeaveApplicationView()
        {
           // WindowState = WindowState.Maximized;
            this.DataContext = new LeaveApplicationViewModel();
           
            InitializeComponent(); 
            mypcat1.PCAData_enent += Mypcat1_PCAData_enent;
            mypcat1.init();
        }
        string jzdbgProvice = "";
        string jzdbgCity = "";
        string jzdbgArea = "";
        string jzdbgTown = "";
        private void Mypcat1_PCAData_enent(string provinceCode, string cityCode, string areaCode, string townCode, string provinceName, string cityName, string areaName, string townName)
        {
            jzdbgProvice = provinceCode;
            jzdbgCity = cityCode;
            jzdbgArea = areaCode;
            jzdbgTown = townCode;
            this.textDetail.Text = provinceName + cityName + areaName + townName;
            Title = provinceCode + "," + cityCode + "," + areaCode + "," + townCode;
        }

        public void CombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            CombinedCalendar.SelectedDate = ((LeaveApplicationViewModel)DataContext).applyStartTime;
            CombinedClock.Time = ((LeaveApplicationViewModel)DataContext).Time;

        }

        public void CombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1") &&
                CombinedCalendar.SelectedDate is DateTime selectedDate)
            {
                DateTime dateTimenew =(DateTime)CombinedCalendar.SelectedDate.Value;
                int year = dateTimenew.Year;
                int month = dateTimenew.Month;
                int data = dateTimenew.Day;
                DateTime combined = CombinedClock.Time;
                int hour = combined.Hour;
                int minute = combined.Minute;
                int second = combined.Second;
                DateTime newDateTime = new DateTime(year, month, data, hour, minute, second);
                ((LeaveApplicationViewModel)DataContext).Time = newDateTime;
                ((LeaveApplicationViewModel)DataContext).applyStartTime = newDateTime;
            }
        }
        public void CombinedDialogOpenedEventHandler1(object sender, DialogOpenedEventArgs eventArgs)
        {
            CombinedCalendar.SelectedDate = ((LeaveApplicationViewModel)DataContext).applyEndTime;
            CombinedClock.Time = ((LeaveApplicationViewModel)DataContext).Time1;
        }
        public void CombinedDialogClosingEventHandler1(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1") &&
                CombinedCalendar1.SelectedDate is DateTime selectedDate)
            {
                DateTime dateTimenew = (DateTime)CombinedCalendar1.SelectedDate.Value;
                int year = dateTimenew.Year;
                int month = dateTimenew.Month;
                int data = dateTimenew.Day;
                DateTime combined = CombinedClock1.Time;
                int hour = combined.Hour;
                int minute = combined.Minute;
                int second = combined.Second;
                DateTime newDateTime = new DateTime(year, month, data, hour, minute, second);
                ((LeaveApplicationViewModel)DataContext).Time1 = newDateTime;
                ((LeaveApplicationViewModel)DataContext).applyEndTime = newDateTime;
            }
        }
    }
}


