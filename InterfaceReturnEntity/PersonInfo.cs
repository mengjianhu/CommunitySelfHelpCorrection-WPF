using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceReturnEntity
{
    /// <summary>
    /// 矫正对象信息
    /// </summary>
    public class PersonInfo: BindableBase
    {
        /// <summary>
        /// 人员编号
        /// </summary>
        public string termerId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 矫正状态
        /// </summary>
        public string termerStatus { get; set; }
        /// <summary>
        /// 头像文件
        /// </summary>
        public string avatarFile { get; set; }
        /// <summary>
        /// 新增时间 2022-03-15 19:06:28
        /// </summary>
        public DateTime? createTime { get; set; }
    }
}
