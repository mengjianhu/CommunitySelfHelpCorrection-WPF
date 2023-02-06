using Prism.Events;
using Prism.Regions;
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
using 社区自助矫正系统WPF.Extensions;
using 社区自助矫正系统WPF.ViewModels;

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// XXCXView.xaml 的交互逻辑
    /// </summary>
    public partial class XXCXView : Window
    {
        private readonly IRegionManager regionManager;
        public XXCXView(IRegionManager regionManager, IEventAggregator aggregator)
        {
            InitializeComponent();
            //aggregator.Resgiter(arg =>
            //{
            //    DialogHost.DataContext = new ProgressBar();
            //});
            WindowState = WindowState.Maximized;
            this.regionManager = regionManager;
            
            //RegionManager.SetRegionName(cmr, PrismManager.XXCXViewRegionName);
            Closed += XXCXView_Closed;
        }

        private void XXCXView_Closed(object sender, EventArgs e)
        {
            regionManager.Regions.Remove(PrismManager.XXCXViewRegionName);
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
