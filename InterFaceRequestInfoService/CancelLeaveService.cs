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
 * 文 件 名:  CancelLeaveService
 * 描    述:  
 * 创 建 者：  humengjian
 * 创建时间：  2023-1-5 11:30:17
 * 历史更新记录:
 * =============================================================================================
*修改标记
*修改时间：2023-1-5 11:30:17
*修改人： humengjian
*版本号： V1.0.0.0
*描述：
 * =============================================================================================

****************************************************************************************************************************************************************/
namespace InterFaceRequestInfoService
{
    public class CancelLeaveService
    {

        /// <summary>
        /// 添加执行地变更申请登记
        /// </summary>
        /// <param name="addressChangeSubmit"></param>
        /// <returns></returns>
        public async Task<int> add(CancelLeave cancelLeave)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {

                await db.CancelLeaves.AddAsync(cancelLeave);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 添加一组执行地变更申请登记
        /// </summary>
        /// <param name="addressChangeSubmits"></param>
        /// <returns></returns>
        public async Task<int> addList(List<CancelLeave> cancelLeaves)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.CancelLeaves.AddRange(cancelLeaves);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 查询所有的执行地变更申请登记
        /// </summary>
        /// <returns></returns>
        public async Task<List<CancelLeave>> findAll()
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                return await db.CancelLeaves.ToListAsync<CancelLeave>();
            }
        }
        /// <summary>
        /// 查询所有的执行地变更申请登记(分页查询)
        /// </summary>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public async Task<List<CancelLeave>> findAllByPage(int pageNum, int pageSize = 20)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                return await db.CancelLeaves.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync<CancelLeave>();
            }
        }
        /// <summary>
        /// 删除一组执行地变更申请登记
        /// </summary>
        /// <param name="addressChangeSubmits"></param>
        /// <returns></returns>
        public async Task<int> delList(List<CancelLeave> cancelLeaves)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.CancelLeaves.RemoveRange(cancelLeaves);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 删除执行地变更申请登记
        /// </summary>
        /// <param name="addressChangeSubmit"></param>
        /// <returns></returns>
        public async Task<int> del(CancelLeave cancelLeave)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.CancelLeaves.Remove(cancelLeave);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="addressChangeSubmit"></param>
        /// <returns></returns>
        public async Task<int> update(CancelLeave cancelLeave)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.CancelLeaves.Update(cancelLeave);
                return await db.SaveChangesAsync();
            }
        }
    }
}
