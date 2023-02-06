using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    /// <summary>
    /// 获取集中劳动/教育接口
    /// </summary>
    public class getCurrentConcentrateInfo
    {
        /// <summary>
        /// 集中编号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime  time { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? startTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endTime { get; set; }
    }
}
