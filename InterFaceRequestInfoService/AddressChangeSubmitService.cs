using InterFaceRequestInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/****************************************************************************************************************************************************************
 *
 * 版权所有: 2023  All Rights Reserved.
 * 文 件 名:  AddressChangeSubmitService
 * 描    述:  
 * 创 建 者：  humengjian
 * 创建时间：  2023-1-4 15:56:09
 * 历史更新记录:
 * =============================================================================================
*修改标记
*修改时间：2023-1-4 15:56:09
*修改人： humengjian
*版本号： V1.0.0.0
*描述：
 * =============================================================================================

****************************************************************************************************************************************************************/
namespace InterFaceRequestInfoService
{
    public class AddressChangeSubmitService
    {
        /// <summary>
        /// 添加执行地变更申请登记
        /// </summary>
        /// <param name="addressChangeSubmit"></param>
        /// <returns></returns>
        public async Task<int> add(AddressChangeSubmit addressChangeSubmit)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                bool istrue = db.Database.EnsureCreated();
                await db.AddressChangeSubmits.AddAsync(addressChangeSubmit);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 添加一组执行地变更申请登记
        /// </summary>
        /// <param name="addressChangeSubmits"></param>
        /// <returns></returns>
        public async Task<int> addList(List<AddressChangeSubmit> addressChangeSubmits)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.AddressChangeSubmits.AddRange(addressChangeSubmits);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 查询所有的执行地变更申请登记
        /// </summary>
        /// <returns></returns>
        public async Task<List<AddressChangeSubmit>> findAll()
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                return await db.AddressChangeSubmits.ToListAsync<AddressChangeSubmit>();
            }
        }
        /// <summary>
        /// 查询所有的执行地变更申请登记(分页查询)
        /// </summary>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public async Task<List<AddressChangeSubmit>> findAllByPage(int pageNum, int pageSize = 20)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                return await db.AddressChangeSubmits.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync<AddressChangeSubmit>();
            }
        }
        /// <summary>
        /// 删除一组执行地变更申请登记
        /// </summary>
        /// <param name="addressChangeSubmits"></param>
        /// <returns></returns>
        public async Task<int> delList(List<AddressChangeSubmit> addressChangeSubmits)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.AddressChangeSubmits.RemoveRange(addressChangeSubmits);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 删除执行地变更申请登记
        /// </summary>
        /// <param name="addressChangeSubmit"></param>
        /// <returns></returns>
        public async Task<int> del(AddressChangeSubmit addressChangeSubmit)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.AddressChangeSubmits.Remove(addressChangeSubmit);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="addressChangeSubmit"></param>
        /// <returns></returns>
        public async Task<int> update(AddressChangeSubmit addressChangeSubmit)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.AddressChangeSubmits.Update(addressChangeSubmit);
                return await db.SaveChangesAsync();
            }

        }
    }
}
