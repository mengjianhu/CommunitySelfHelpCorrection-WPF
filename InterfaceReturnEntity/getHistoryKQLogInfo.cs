using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    /// <summary>
    /// 处理考勤记录接口返回数据
    /// </summary>
    public class getHistoryKQLogInfo
    {
        public string id { get; set; }

        public string identity { get; set; }

        public string userId { get; set; }
        public string userName { get; set; }
        public string type { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }

        public Nullable<int> authType { get; set; }
    }
}
