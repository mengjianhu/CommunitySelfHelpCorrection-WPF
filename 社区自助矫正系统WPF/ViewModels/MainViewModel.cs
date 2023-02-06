using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using 社区自助矫正系统WPF.Common.Models;

namespace 社区自助矫正系统WPF.ViewModels
{
    public class MainViewModel : BindableBase
    {

        
        public MainViewModel()
        {
            //App.speak("欢迎使用社区自助矫正系统");

            MainMenus = new ObservableCollection<MainMenu>();

            //CreateMainMenu();
        }
        private ObservableCollection<MainMenu> mainMenu;

        public ObservableCollection<MainMenu> MainMenus
        {
            get { return mainMenu; }
            set { mainMenu = value; RaisePropertyChanged(); }
        }
    
    }
}
