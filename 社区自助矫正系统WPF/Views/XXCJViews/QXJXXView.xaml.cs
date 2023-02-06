using InterfaceReturnEntity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using 社区自助矫正系统WPF.Views.DetailsView;

namespace 社区自助矫正系统WPF.Views.XXCJViews
{
    /// <summary>
    /// QXJXXView.xaml 的交互逻辑
    /// </summary>
    public partial class QXJXXView : UserControl
    {
        public QXJXXView()
        {
            InitializeComponent();
        }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.datagrid.ItemsSource != null)
            {
                getLeaveListInfo LL = this.datagrid.SelectedItem as getLeaveListInfo;
                LeaveListDetailView leaveListDetailView = new LeaveListDetailView(); leaveListDetailView.ShowDialog();
            }
        }
    }
}
