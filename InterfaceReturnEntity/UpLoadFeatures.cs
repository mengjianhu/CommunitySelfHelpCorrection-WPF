using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    /// <summary>
    /// 上传特征信息
    /// </summary>
    public class UpLoadFeatures
    {
        /// <summary>
        /// 矫正人员编号
        /// </summary>
        public string termerId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int no { get; set; }
        /// <summary>
        /// 特征文件编号
        /// </summary>
        public string fileId { get; set; }
    }
}
