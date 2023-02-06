using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperUnit
{
    class JsonData
    {
        /// <summary>
        /// 加密之后的数据
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
}
