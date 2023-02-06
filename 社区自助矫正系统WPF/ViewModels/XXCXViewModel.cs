using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using 社区自助矫正系统WPF.Common;
using 社区自助矫正系统WPF.Extensions;
using 社区自助矫正系统WPF.Views;

namespace 社区自助矫正系统WPF.ViewModels
{
    public class XXCXViewModel : BindableBase,IConfigureService
    {
        /// <summary>
        /// 导航
        /// </summary>
        public DelegateCommand<XXCXMenuBar> NavigateCommand { get; private set; }
        /// <summary>
        /// 上一步
        /// </summary>
        public DelegateCommand GoBackCommand { get; private set; }
        /// <summary>
        /// 下一步
        /// </summary>
        public DelegateCommand GoForwardCommand { get; private set; }
        public DelegateCommand<object> NavigateCommandClose { get; set; }
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;

        public XXCXViewModel(IRegionManager regionManager)
        {
           
             XXCXMenuBars = new ObservableCollection<XXCXMenuBar>();
            //CreateMenuBar();
            this.regionManager = regionManager;
         
            NavigateCommand = new DelegateCommand<XXCXMenuBar>(Navigate);
            NavigateCommandClose = new DelegateCommand<object>(closeForm);
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {

                    journal.GoBack();
                }
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                {

                    journal.GoForward();
                }
            });
            Configure();


        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="obj"></param>
        private void closeForm(object obj)
        {
            (obj as Window).Close();
        }
        string ss = "";
        private void Navigate(XXCXMenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;

            regionManager.Regions[PrismManager.XXCXViewRegionName].RequestNavigate(obj.NameSpace, back =>
             {
                 navJouName = obj.Title;
                 journal = back.Context.NavigationService.Journal;
                 // indexMenu.Add(menuBars.IndexOf(obj));
             });
        }

        private ObservableCollection<XXCXMenuBar> menuBars;
        /// <summary>
        /// 查询菜单集合
        /// </summary>
        public ObservableCollection<XXCXMenuBar> XXCXMenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        private int _selectMenu = -1;

        public int selectMenu
        {
            get { return _selectMenu; }
            set { _selectMenu = value; RaisePropertyChanged(); }
        }

        private string _navJouName;

        public string navJouName
        {
            get { return _navJouName; }
            set { _navJouName = value; RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            XXCXMenuBars.Add(new XXCXMenuBar() { Icon = "Home", Title = "矫正信息查询", NameSpace = "JZXXCCView" });
            XXCXMenuBars.Add(new XXCXMenuBar() { Icon = "NotebookOutline", Title = "劳动教育信息", NameSpace = "EduInfoView" });
            XXCXMenuBars.Add(new XXCXMenuBar() { Icon = "NotebookPlus", Title = "考勤记录信息", NameSpace = "AttendanceRecordView" });
            XXCXMenuBars.Add(new XXCXMenuBar() { Icon = "Cog", Title = "请销假信息", NameSpace = "QXJXXView" });
            XXCXMenuBars.Add(new XXCXMenuBar() { Icon = "Cog", Title = "执行地变更信息", NameSpace = "ZXDBGXXView" });
        }

        public void Configure()
        {
              CreateMenuBar();
            //regionManager.Regions[PrismManager.XXCXViewRegionName].RequestNavigate("JZXXCCView");

        }
    }
}
