using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    /// <summary>
    /// 获取考勤信息传参
    /// </summary>
    public class getKaoqinRequestEntity
    {
        /// <summary>
        /// 考勤类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string identity { get; set; }
        /// <summary>
        /// 集中编号
        /// </summary>
        public string concentrateId { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int pageNumber { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public  Nullable< int> pageSize { get; set; }
    }
}
