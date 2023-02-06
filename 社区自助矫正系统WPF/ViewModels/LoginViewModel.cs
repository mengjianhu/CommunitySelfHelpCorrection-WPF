using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 社区自助矫正系统WPF.ViewModels
{
    public class LoginViewModel:BindableBase
    {
        public LoginViewModel()
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login": Login(); break;
                case "Close": Close(); break;
            }
        }

        private void Close()
        {
            throw new NotImplementedException();
        }

        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) ||
              string.IsNullOrWhiteSpace(Password))
            {
                return;
            }
            MessageBox.Show(Account + Password);
        }

        public DelegateCommand<string> ExecuteCommand { get; set; }
        private string  account;

        public string  Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

    }
}
