using InterFaceRequestInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/****************************************************************************************************************************************************************
 *
 * 版权所有: 2023  All Rights Reserved.
 * 文 件 名:  DataUploadDBContext
 * 描    述: 数据上传时使用 先存储到数据库，在手动上传
 * 创 建 者：  humengjian
 * 创建时间：  2023-1-4 15:32:39
 * 历史更新记录:
 * =============================================================================================
*修改标记
*修改时间：2023-1-4 15:32:39
*修改人： humengjian
*版本号： V1.0.0.0
*描述：
 * =============================================================================================

****************************************************************************************************************************************************************/
namespace InterFaceRequestInfoService
{
    public class DataUploadDBContext:DbContext
    {
        static readonly string _connStr = @"Data Source=" + System.Environment.CurrentDirectory + "\\"+"db\\" + "sqjz.db";//获取绝对路径下的数据库所在地

        public DataUploadDBContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connStr);
        }
        /// <summary>
        /// 执行地变更申请
        /// </summary>
        public DbSet<AddressChangeSubmit> AddressChangeSubmits { get; set; }
        /// <summary>
        /// 销假登记
        /// </summary>
        public DbSet<CancelLeave> CancelLeaves { get; set; }
        /// <summary>
        /// 请假登记
        /// </summary>
        public DbSet<LeaveSubmit> LeaveSubmits { get; set; }

        public DbSet<UpLoadFeaturesVoice> Voices { get; set; }

    }
}
