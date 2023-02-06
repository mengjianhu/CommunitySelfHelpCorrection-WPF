using InterFaceRequestInfo;
using InterFaceRequestInfoService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace 社区自助矫正系统WPF.Views
{
    /// <summary>
    /// TestSqliteView.xaml 的交互逻辑
    /// </summary>
    public partial class TestSqliteView : Window
    {
        public TestSqliteView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async Task<int> Testadd()
        {
            AddressChangeSubmitService addressChangeSubmitService = new AddressChangeSubmitService();
            AddressChangeSubmit addressChangeSubmit = new AddressChangeSubmit()
            {
                termerId = "1234656",
                applyFileIds = "123",
                applyReason = "1",
                destinationDetail = "1",
                destinationOutProvince = "1",
                destinationSelIdL1 = "1",
                destinationSelIdL2 = "1",
                destinationSelIdL3 = "1",
                destinationSelIdL4 = "1",
                idNum = "342222200012030414",

            };
            return await addressChangeSubmitService.add(addressChangeSubmit);
        }

        private async void add(object sender, RoutedEventArgs e)
        {
            await Testadd();
        }

        public async Task<List<AddressChangeSubmit>> findAll()
        {
            AddressChangeSubmitService addressChangeSubmitService = new AddressChangeSubmitService();
            return await addressChangeSubmitService.findAll();
        }


        private async void find(object sender, RoutedEventArgs e)
        {
            List<AddressChangeSubmit> ss = await findAll();
        }

        private async void update(object sender, RoutedEventArgs e)
        {
            AddressChangeSubmitService addressChangeSubmitService = new AddressChangeSubmitService();
            List<AddressChangeSubmit> ss = await findAll();
          
            ss[0].oper = 1;
            await addressChangeSubmitService.update(ss[0]);
        }
    }
}
