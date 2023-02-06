using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperUnit
{
    public class HttpSign
    {
        static string urlSign = "http://localhost:8080/SM2Service_war/sm2/sign";
        static string urlEncrypt = "http://localhost:8080/SM2Service_war/sm2/encrypt";
        static string urlEncryptAndSign = "http://localhost:8080/SM2Service_war/sm/encryptData";
        /// <summary>
        /// 对加密后的数据签名
        /// </summary>
        /// <param name="desctext"></param>
        /// <returns></returns>
        public static string getSign(string sign)
        {
            return HttpHelper.Post(urlSign, JsonConvert.SerializeObject(new MyClass() { text = sign }));
        }
        public static string getEncrypt(string desctext)
        {
            return HttpHelper.Post(urlEncrypt, JsonConvert.SerializeObject(new MyClass() { text = desctext }));
        }
        public static string getEncryptAndSign(string encryptAndSign)
        {
            return HttpHelper.Post(urlEncryptAndSign, JsonConvert.SerializeObject(new MyClass() { text = encryptAndSign }));
        }
    }
    class MyClass
    {
        public string text { get; set; }
    }
}
