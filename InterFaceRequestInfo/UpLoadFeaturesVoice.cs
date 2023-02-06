using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/****************************************************************************************************************************************************************
 *
 * 版权所有: 2023  All Rights Reserved.
 * 文 件 名:  UpLoadFeatures
 * 描    述:  
 * 创 建 者：  humengjian
 * 创建时间：  2023-1-5 17:15:20
 * 历史更新记录:
 * =============================================================================================
*修改标记
*修改时间：2023-1-5 17:15:20
*修改人： humengjian
*版本号： V1.0.0.0
*描述：
 * =============================================================================================

****************************************************************************************************************************************************************/
namespace InterFaceRequestInfo
{
    /// <summary>
    /// 上传特征信息
    /// </summary>
    [Table("t_UpLoadFeaturesVoice")]
    public class UpLoadFeaturesVoice
    {
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 矫正人员编号
        /// </summary>
        public string termerId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; } = 2.ToString();
        /// <summary>
        /// 序号
        /// </summary>
        public int no { get; set; } = 1;
        /// <summary>
        /// 特征文件编号
        /// </summary>
        public string fileId { get; set; }
        /// <summary>
        /// 声纹文件保存路径
        /// </summary>
        public string filePath { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string idNum { get; set; }
        /// <summary>
        /// 操作 0 未操作  1 上传成功  -1 上传失败
        /// </summary>
        public int oper { get; set; } = 0;
    }
}

