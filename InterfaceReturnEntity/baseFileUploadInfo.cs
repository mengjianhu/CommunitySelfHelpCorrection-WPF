using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class baseFileUploadInfo
    {
        /// <summary>
        /// base64 文 件
        /// </summary>
        public string base64 { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string fileName { get; set; }
    }
}
