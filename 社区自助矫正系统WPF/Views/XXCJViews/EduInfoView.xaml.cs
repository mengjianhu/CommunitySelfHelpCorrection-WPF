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
using static 社区自助矫正系统WPF.ViewModels.XXCXModels.EduInfoViewModel;

namespace 社区自助矫正系统WPF.Views.XXCJViews
{
    /// <summary>
    /// EduInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class EduInfoView : UserControl
    {
        public EduInfoView()
        {
            InitializeComponent();

           
        }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.datagrid.ItemsSource != null)
            {
                getCurrentConcentrateInfo LL =  this.datagrid.SelectedItem as getCurrentConcentrateInfo;


            }
        }
    }
}
