using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterFaceRequestInfo
{
    /// <summary>
    /// 销假登记
    /// </summary>
    [Table("t_CancelLeave")]
    public class CancelLeave
    {
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string termerId { get; set; }
        /// <summary>
        /// 请假编号
        /// </summary>
        public string leaveApplyId { get; set; }
        /// <summary>
        /// 文件编号
        /// </summary>
        public string reportBackFileIds { get; set; }
        /// <summary>
        /// 身份证号 （离线状态下使用）必填
        /// </summary>
        public string idNum { get; set; }
        /// <summary>
        /// 操作 0 未操作  1 上传成功  -1 上传失败
        /// </summary>
        public int oper { get; set; } = 0;
    }
}
