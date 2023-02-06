using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    /// <summary>
    /// 获取机构信息
    /// </summary>
    public class getOrgansInfo
    {
        public string flag { get; set; }
        public string parentId { get; set; }
        public string isValid { get; set; }
        public string organ { get; set; }
        public string parentOrg { get; set; }
        public string childCount { get; set; }
        public string id { get; set; }
    }
}
