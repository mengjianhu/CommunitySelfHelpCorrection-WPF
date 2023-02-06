using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    public class UpLoadFileResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        ///消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 文件返回编码
        /// </summary>
        public List<string> rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool ok { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool fail { get; set; }
    }
}
