using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    /// <summary>
    /// 获取文件信息
    /// </summary>
    public class getFileInfo
    {
        /// <summary>
        /// 文件编号
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string objectName { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string originalFileName { get; set; }
        /// <summary>
        /// 文件大小（base64长度）
        /// </summary>
        public int? dataLength { get; set; }

    }
}
