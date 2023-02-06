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

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// AddressChangeView.xaml 的交互逻辑
    /// </summary>
    public partial class AddressChangeView : Window
    {
        public AddressChangeView()
        {
           // WindowState = WindowState.Maximized;
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
    }
}
