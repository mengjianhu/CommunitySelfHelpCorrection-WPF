using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Utilities.Encoders;
namespace HelperUnit
{
    public class SM2Util
    {
        /**
           * 生成SM2秘钥对
           * string[0] 公钥
           * string[1] 私钥
           */
        public static string[] GenerateKeyPair()
        {
            return SM2.GenerateKeyPair();
        }

        /**
         * SM2签名
         * data 签名的数据
         * priKey 私钥
         */
        public static string Sign(string data, string priKey)
        {
            SM2 sm2 = new SM2(priKey, null);
            return sm2.Sign(data);
        }
        public static string SignNoAsn1(string data, string priKey)
        {
            SM2 sm2 = new SM2(priKey, null);
            return sm2.SignNoAsn1(data);
        }

        /**
         * SM2签名
         * sign 源数据
         * pubKey 公钥
         * sign 签名的数据
         */
        public static bool verifySign(string msg, string pubKey, string sign)
        {
            SM2 sm2 = new SM2(null, pubKey);
            return sm2.verifySign(msg, sign);
        }


        /**
         * 加密
         * 返回Base64字符串
         *  公钥加密
         *  plainText 要加密的文本
         *  pubKey 公钥
         */
        
        
        public static string encryptBase64(string plainText, string pubKey)
        {
            //SM2 sm2 = new SM2(null, pubKey);
            //byte[] encryptByte = sm2.encrypt(Encoding.UTF8.GetBytes(plainText));
            //return Base64.ToBase64String(encryptByte);
            SM2 sm2 = new SM2(null, pubKey);
            byte[] encryptByte = sm2.encrypt(Encoding.UTF8.GetBytes(plainText));
            //return Base64.ToBase64String(encryptByte);
            return Hex.ToHexString(encryptByte);
        }

        /**
         * 解密
         *  私钥解密
         *  plainText 要加密的文本
         *  pubKey 公钥
         */
        public static string decryptBase64(string plainText, string priKey)
        {
            //SM2 sm2 = new SM2(priKey, null);
            //byte[] deCode = Base64.Decode(plainText);
            //byte[] decryptText = sm2.deceypt(deCode);
            //return Encoding.UTF8.GetString(decryptText);
            SM2 sm2 = new SM2(priKey, null);
            byte[] deCode = Hex.Decode(plainText);

            byte[] decryptText = sm2.deceypt(deCode);

            return Encoding.UTF8.GetString(decryptText);
        }

    }
}
