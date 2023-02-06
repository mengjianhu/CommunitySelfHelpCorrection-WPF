using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 社区自助矫正系统WPF.Extensions;

namespace 社区自助矫正系统WPF.ViewModels
{
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IContainerProvider containerProvider;
        public readonly IEventAggregator aggregator;

        public NavigationViewModel(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
            aggregator = containerProvider.Resolve<IEventAggregator>();
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 从本页面转到其它页面的时候， 切换导航时
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //MessageBox.Show("OnNavigatedFrom");
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            //MessageBox.Show("OnNavigatedTo");
        }
        //public void UpdateLoading(bool IsOpen)
        //{
        //    aggregator.UpdateLoading(new Common.Events.UpdateModel()
        //    {
        //        IsOpen = IsOpen
        //    });
        //}
    } 
}