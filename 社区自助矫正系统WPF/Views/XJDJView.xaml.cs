using System;
using System.Collections.Generic;
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
using InterFaceRequestInfo;
using Newtonsoft.Json;
using Prism.Events;
using 社区自助矫正系统WPF.ViewModels;
using 社区自助矫正系统WPF.Extensions;
using 社区自助矫正系统WPF.SplashScreen;

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// XJDJView.xaml 的交互逻辑
    /// </summary>
    public partial class XJDJView : Window
    {
        public XJDJView()
        {
            InitializeComponent();
            
            
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            //if(string.IsNullOrEmpty(this.txt_termerId.Text.Trim()))
            //{
            //    MessageBox.Show("请放置身份证", "提示信息", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //CancelLeave cancelLeave = new CancelLeave()
            //{
            //    termerId = this.txt_termerId.Text.Trim(),
            //    leaveApplyId = this.txt_leaveApplyId.Text.Trim(),
            //    reportBackFileIds = this.txt_reportBackFileIds.Text.Trim()
            //};
            //string josnData = JsonConvert.SerializeObject(cancelLeave);
            //MessageBox.Show(josnData);
        }

        //private void btn_close_Click(object sender, RoutedEventArgs e)
        //{
        //    App.speakStop();
        //    this.Close();
        //}
    }
}
