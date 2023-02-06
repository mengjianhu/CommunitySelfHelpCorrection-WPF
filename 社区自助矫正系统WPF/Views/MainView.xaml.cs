using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
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
using InterfaceReturnEntity;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using 社区自助矫正系统WPF.Extensions;
using 社区自助矫正系统WPF.Views.VideoShow;

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        private readonly IContainerExtension _container;
        private readonly IRegionManager _regionManager;
        public MainView(IContainerExtension container, IRegionManager regionManager)
        {

            InitializeComponent();
           // this.WindowState = WindowState.Maximized;
            _container = container;
            _regionManager = regionManager;

        }

        /// <summary>
        /// 信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_xxcx_MouseDown(object sender, MouseButtonEventArgs e)
        {

            var provider = ContainerLocator.Container.Resolve<IContainerProvider>();
            var regionManager = ContainerLocator.Container.Resolve<IRegionManager>();
            var win = provider.Resolve<XXCXView>();

            if (win is Window view)
            {
                RegionManager.SetRegionManager(view, regionManager);
                view.ShowDialog();
            }
        }

        private void 关闭_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btn_xjsqdj_MouseDown(object sender, MouseButtonEventArgs e)
        {

            XJDJView xJDJView = new XJDJView();
            xJDJView.Show();
        }

        private void btn_facecj_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FaceCjView faceCjView = new FaceCjView();
            faceCjView.ShowDialog();
        }

        private void btn_fingercj_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //FingerCjView finger = new FingerCjView();
                //finger.ShowDialog();
                FingerCjZkFinger0View finger = new FingerCjZkFinger0View();
                finger.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void btn_voiceCj_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                VoiceCJView voiceCJView = new VoiceCJView();
                voiceCJView.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_leave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LeaveApplicationView leaveApplicationView = new LeaveApplicationView();
            leaveApplicationView.Show();
        }

        private void btn_AddressChange_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddressChangeView addressChangeView = new AddressChangeView();
            addressChangeView.Show();
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_signin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignInView signInView = new SignInView();
            signInView.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_xxcj_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // XXCJReportView xXCJReportView = new XXCJReportView();
            LoginView xXCJReportView = new LoginView();
            xXCJReportView.ShowDialog();
        }
        /// <summary>
        /// 测试摄像头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_testVideoCamera(object sender, MouseButtonEventArgs e)
        {
            OpenCVSharpVideo openCVSharpVideo = new OpenCVSharpVideo();
            openCVSharpVideo.ShowDialog();
        }

        private void btn_testsql(object sender, MouseButtonEventArgs e)
        {
            TestSqliteView testSqliteView = new TestSqliteView();
            testSqliteView.ShowDialog();
        }
    }
}
