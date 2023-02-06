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
 * 文 件 名:  VoiceInfoSubmitService
 * 描    述:  
 * 创 建 者：  humengjian
 * 创建时间：  2023-1-6 11:01:12
 * 历史更新记录:
 * =============================================================================================
*修改标记
*修改时间：2023-1-6 11:01:12
*修改人： humengjian
*版本号： V1.0.0.0
*描述：
 * =============================================================================================

****************************************************************************************************************************************************************/
namespace InterFaceRequestInfoService
{
    public class VoiceInfoSubmitService
    {

        /// <summary>
        /// 添加声纹数据
        /// </summary>
        /// <param name="addressChangeSubmit"></param>
        /// <returns></returns>
        public async Task<int> add(UpLoadFeaturesVoice upLoadFeaturesVoice)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                bool istrue = db.Database.EnsureCreated();
                await db.Voices.AddAsync(upLoadFeaturesVoice);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 添加一组添加声纹数据
        /// </summary>
        /// <param name="addressChangeSubmits"></param>
        /// <returns></returns>
        public async Task<int> addList(List<UpLoadFeaturesVoice> upLoadFeaturesVoices)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.Voices.AddRange(upLoadFeaturesVoices);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 查询所有的添加声纹数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<UpLoadFeaturesVoice>> findAll()
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                return await db.Voices.ToListAsync<UpLoadFeaturesVoice>();
            }
        }
        public async Task<List<UpLoadFeaturesVoice>> findAllByIdNum(string idNum)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                return await db.Voices.Where(a => a.idNum == idNum).ToListAsync();
            }
        }
        /// <summary>
        /// 查询所有的添加声纹数据 根据 oper
        /// </summary>
        /// <returns></returns>
        public async Task<List<UpLoadFeaturesVoice>> findAllByOper(int oper)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                return await db.Voices.Where(a=>a.oper == oper).ToListAsync<UpLoadFeaturesVoice>();
            }
        }
        /// <summary>
        /// 查询所有的  声纹数据(分页查询)
        /// </summary>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public async Task<List<UpLoadFeaturesVoice>> findAllByPage(int pageNum, int pageSize = 20)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                return await db.Voices.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync<UpLoadFeaturesVoice>();
            }
        }
        /// <summary>
        /// 删除一组声纹数据
        /// </summary>
        /// <param name="addressChangeSubmits"></param>
        /// <returns></returns>
        public async Task<int> delList(List<UpLoadFeaturesVoice> upLoadFeaturesVoices)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.Voices.RemoveRange(upLoadFeaturesVoices);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 删除声纹数据
        /// </summary>
        /// <param name="addressChangeSubmit"></param>
        /// <returns></returns>
        public async Task<int> del(UpLoadFeaturesVoice upLoadFeaturesVoice)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.Voices.Remove(upLoadFeaturesVoice);
                return await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 删除一组声纹数据
        /// </summary>
        /// <param name="upLoadFeaturesVoice"></param>
        /// <returns></returns>
        public async Task<int> delRange(List<UpLoadFeaturesVoice> upLoadFeaturesVoices)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.Voices.RemoveRange(upLoadFeaturesVoices);
                return await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="addressChangeSubmit"></param>
        /// <returns></returns>
        public async Task<int> update(UpLoadFeaturesVoice upLoadFeaturesVoice)
        {
            using (DataUploadDBContext db = new DataUploadDBContext())
            {
                db.Voices.Update(upLoadFeaturesVoice);
                return await db.SaveChangesAsync();
            }

        }
    }

}
