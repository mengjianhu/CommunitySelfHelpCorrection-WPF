using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 社区自助矫正系统WPF.Common.Events;

namespace 社区自助矫正系统WPF.Extensions
{
    public static class DialogExtensions
    {
        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void UpdateLoading(this IEventAggregator aggregator, UpdateModel model)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Publish(model);
        }
        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void Resgiter(this IEventAggregator aggregator, Action<UpdateModel> model)
        {
            aggregator.GetEvent<UpdateLoadingEvent>().Subscribe(model);
        }
    }
}
