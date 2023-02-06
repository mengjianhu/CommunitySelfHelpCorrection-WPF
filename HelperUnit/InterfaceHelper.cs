using InterfaceReturnEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperUnit
{
    public class InterfaceHelper
    {
        public string url = "http://28.0.0.49:10000";

        ///// <summary>
        ///// 应用编号
        ///// </summary>
        //string appId = "c2132c63-c075-11ec-ad5e-2cea7f489107";
        ///// <summary>
        ///// 应用key
        ///// </summary>
        //string appKey = "c83defca-c075-11ec-ad5e-2cea7f489107";
        /// <summary>
        /// 设备编号
        /// </summary>
        //string aioCode = "ZZD-L-YWX-02-2022W2020000";
        /// <summary>
        /// 服务端公钥
        /// </summary>
        //string publicKey = "047cf9563086b289d8cd9405d357a3cbd637b25700fcfe7b807c74af843844f0edf567b703e3cefe2558edc2277b813bddaaa543eeeeb1ec9ad2869d31d2fb57b3";
        /// <summary>
        /// 客户端私钥
        /// </summary>
        string priviateKey = "4efded1c7c32f839c15a0eb36dbe565437520aff1cfe3eae7d8de409ae90bf51";
        /// <summary>
        /// token 值
        /// </summary>
        public string token { get; set; }
       
        public string GetSJToken(string url = "http://28.0.0.49:10000/terminal/api/v2/comm/getToken")
        {
            TokeData tokeData = new TokeData();//应用编号，应用key
            string res = JsonConvert.SerializeObject(tokeData);//josn
            string data = HttpSign.getEncryptAndSign(res);
            string ss = HttpHelper.Post(url, data);
            JsonData jsonData1 = JsonConvert.DeserializeObject<JsonData>(ss);
            string backData = jsonData1.data;//获取服务器返回的数据
            string decData = SM2Util.decryptBase64(backData, priviateKey);//解密服务器数据
            Result<GetTokens> result = JsonConvert.DeserializeObject<Result<GetTokens>>(decData);//josn字符串转为对              
            return result.rows[0].token;
        }
        class GetTokens
        {
            public string token { get; set; }
        }
        public class TokeData
        {
            public string appId { get; set; } = "c2132c63-c075-11ec-ad5e-2cea7f489107";
            public string appKey { get; set; } = "c83defca-c075-11ec-ad5e-2cea7f489107";
        }
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="fileIds">文件编号</param>
        /// <param name="token">token授权</param>
        /// <returns></returns>
        public string getFileInfos(string fileIds, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/comm/getFileInfos")
        {
            string data = HttpSign.getEncryptAndSign(fileIds);
            string ss = HttpHelper.PostToken(url, data, token);//服务器响应参数
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            //Result<FileInfo> result = JsonConvert.DeserializeObject<Result<FileInfo>>(decData);//把josn转为对象
            //string resData = HttpHelper.PostToken(url, data, token);
            //string decResult = SM2Util.decryptBase64(resData, priviateKey);
            //JsonData jsonData1 = JsonConvert.DeserializeObject<JsonData>(decResult);
            return decData;
        }
        /// <summary>
        /// 文件信息
        /// </summary>
        class FileInfo
        {
            /// <summary>
            /// 文件编号
            /// </summary>
            public string fileId { get; set; }
            /// <summary>
            /// 文件路径
            /// </summary>
            public string objectName { get; set; }
            /// <summary>
            /// 原始文件名
            /// </summary>
            public string originalFileName { get; set; }
            /// <summary>
            /// 文件大小
            /// </summary>
            public int dataLength { get; set; }
        }
        public class Result<T>
        {
            /// <summary>
            /// 错误码 0正确 非0失败
            /// </summary>
            public int code { get; set; }
            /// <summary>
            /// 错误描述
            /// </summary>
            public string msg { get; set; }
            /// <summary>
            /// 内容
            /// </summary>
            public List<T> rows { get; set; }
            public bool ok { get; set; }
            public bool fail { get; set; }

        }
        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dicTypes"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string getDicList(string dicTypes, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/comm/getDicList")
        {
            string data = HttpSign.getEncryptAndSign(dicTypes);
            string ss = HttpHelper.PostToken(url, data, token);//服务器返回的加密数据
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            //string resData = HttpHelper.PostToken(url, data, token);
            //string decResult = SM2Util.decryptBase64(resData, priviateKey);
            //JsonData jsonData1 = JsonConvert.DeserializeObject<JsonData>(decResult);
            return decData;
        }
        /// <summary>
        /// 上传文件信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="uploadData"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string baseFileUpload(string uploadData, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/comm/baseFileUpload")
        {
            string data = HttpSign.getEncryptAndSign(uploadData);
            string ss = HttpHelper.PostToken(url, data, token);//服务器返回的加密数据
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;

        }
        class FILEiDS
        {
            public string fileId { get; set; }
        }
        /// <summary>
        /// 获取行政区域划分
        /// </summary>
        /// <param name="url"></param>
        /// <param name="areas"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string getAreas(string areas, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/comm/getAreas")
        {
            string data = HttpSign.getEncryptAndSign(areas);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }
        /// <summary>
        /// 获取机构信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="getOrgans">机构信息</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string getOrgans(string getOrgans, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/getOrgans")
        {
            string data = HttpSign.getEncryptAndSign(getOrgans);
            string res = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(res);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }
        /// <summary>
        /// 获取矫正对象的信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="idCard"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string getPersonInfo(string idCard, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/getPersonInfo")
        {
            string data = HttpSign.getEncryptAndSign(idCard);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据

            return decData;
        }
        /// <summary>
        /// 上传信息采集数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userInfo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string UploadPersonInfo(string userInfo, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/report")
        {
            string data = HttpSign.getEncryptAndSign(userInfo);

            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }


        /// <summary>
        /// 获取特征信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="terrmerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string getFeatures(string terrmerId, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/getFeatures")
        {
            string data = HttpSign.getEncryptAndSign(terrmerId);

            string ss = HttpHelper.PostToken(url, data, token);

            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }

        /// <summary>
        /// 矫正人员信息
        /// </summary>
        public class PersonInfo
        {
            /// <summary>
            /// 人员编号
            /// </summary>
            public string termerId { get; set; }
            public string name { get; set; }
            /// <summary>
            /// 矫正状态 字典：PERSONNEL_STATES
            /// </summary>
            public string termerStatus { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string avatarFile { get; set; }
            /// <summary>
            /// 新增时间
            /// </summary>
            public DateTime createTime { get; set; }
        }

        /// <summary>
        /// 获取集中劳动/教育接口
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string getCurrentConcentrate(string type, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/getCurrentConcentrate")
        {
            string data = HttpSign.getEncryptAndSign(type);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }
        /// <summary>
        /// 历史考勤记录
        /// </summary>
        public string getHistoryLog(string history, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/getHistoryLog")
        {
            string data = HttpSign.getEncryptAndSign(history);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }

        public string getSign(string url, string signInfo, string token)
        {
            return HttpHelper.PostToken(url, signInfo, token);
        }


        /// <summary>
        /// 请销假列表
        /// </summary>
        /// <param name="url"></param>
        /// <param name="history"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string getLeaveList(string history, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/getLeaveList")
        {
            string data = HttpSign.getEncryptAndSign(history);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }

        /// <summary>
        /// 获取执行地变更列表
        /// </summary>
        /// <param name="url"></param>
        /// <param name="addressChange"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string getAddressChangeList(string addressChange, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/getAddressChangeList")
        {
            string data = HttpSign.getEncryptAndSign(addressChange);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }
        /// <summary>
        /// 上传特征信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dataUpload"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string uploadFeatures(string dataUpload, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/uploadFeatures")
        {
            string data = HttpSign.getEncryptAndSign(dataUpload);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }
        /// <summary>
        /// 上传签到信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dataUpload"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string UploadSignIn(string dataUpload, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/sign")
        {
            string data = HttpSign.getEncryptAndSign(dataUpload);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }
        /// <summary>
        /// 上传请假信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dataUpload"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string UploadSubmitLeave(string dataUpload, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/submitLeave")
        {
            string data = HttpSign.getEncryptAndSign(dataUpload);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }
        /// <summary>
        /// 上传销假信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dataUpload"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string UploadSubmitCancelLeave(string dataUpload, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/submitCancelLeave")
        {
            string data = HttpSign.getEncryptAndSign(dataUpload);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }
        /// <summary>
        /// 执行地变更申请
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dataUpload"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string UploadSubmitAddressChange(string dataUpload, string token, string url = "http://28.0.0.49:10000/terminal/api/v2/terminal/submitAddressChange")
        {
            string data = HttpSign.getEncryptAndSign(dataUpload);
            string ss = HttpHelper.PostToken(url, data, token);
            JsonData backData = JsonConvert.DeserializeObject<JsonData>(ss);
            string decData = SM2Util.decryptBase64(backData.data, priviateKey);//解密后的数据
            return decData;
        }
    }

}
