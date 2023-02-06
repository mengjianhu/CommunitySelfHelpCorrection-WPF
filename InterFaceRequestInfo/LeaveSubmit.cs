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
    /// 请假申请
    /// </summary>
    [Table("t_LeaveSubmit")]
    public class LeaveSubmit
    {
        [Key]
        public int id { get; set; }
        public string termerId { get; set; }
        public int applyDays { get; set; }
        public DateTime applyStartTime { get; set; }
        public DateTime applyEndTime { get; set; }
        public string applyReason { get; set; }
        public string applyFileIds { get; set; }
        public string destinationSelIdL1 { get; set; }
        public string destinationSelIdL2 { get; set; }
        public string destinationSelIdL3 { get; set; }
        public string destinationSelIdL4 { get; set; }
        public string destinationDetail { get; set; }

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
