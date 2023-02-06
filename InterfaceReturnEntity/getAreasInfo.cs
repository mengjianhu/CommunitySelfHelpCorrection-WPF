using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    public class getAreasInfo
    {
        /// <summary>
        /// 编号 （根据编号获取下一级数据）
        /// </summary>
        public string areaId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string areaName { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string areaLevel { get; set; }
        /// <summary>
        /// 是否直辖市
        /// </summary>
        public string zxs { get; set; }
        /// <summary>
        /// 是否直管县
        /// </summary>
        public string zgx { get; set; }
        /// <summary>
        /// 下级个数
        /// </summary>
        public int childCount { get; set; }
        /// <summary>
        /// 上级编号
        /// </summary>
        public string parentId { get; set; }
        public string id { get; set; }
    }
}
