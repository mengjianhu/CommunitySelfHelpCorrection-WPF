using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Authorization
{
    public class EncryptionHelper
    {
        private string publicKey { get; set; }
        string md5Begin = "nwwlke";
        string md5End = "rain";
        public EncryptionHelper()
        {
            this.publicKey = "ndy98801";
        }
        /// <summary>
        /// 验证是否授权
        /// </summary>
        public void isLicense()
        {
            try
            {
                if (!RegistFileHelper.ExistRegistInfofile())//没有授权文件
                {
                    FileWriterCpuID();
                    MessageBox.Show("设备缺少文件！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.GetCurrentProcess().Kill();
                }
                string txt = RegistFileHelper.ReadRegistFile();
                String res = DecryptString(txt);
                string mac = res;
                //判断是永久加密还是普通时间加密
                if (res.Contains("@"))//时间有效期加密
                {
                    //获取mac地址及有效期
                    string[] strs = res.Split('@');
                    mac = strs[0];
                    DateTime endDateTime = DateTime.Parse(strs[1]);
                    if (DateTime.Now > endDateTime)//超过有效期  退出系统
                    {
                        //MessageBox.Show("设备授权时间已过！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("设备缺少异常文件！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        FileWriterCpuID();
                        Process.GetCurrentProcess().Kill();
                    }
                }

                if (mac != HardwareInfo.GetCPUSerialNumber())//授权核验失败
                {
                    FileWriterCpuID();
                    //MessageBox.Show("设备未授权！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("设备缺少文件！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.GetCurrentProcess().Kill();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("异常信息：设备未授权！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();
            }
        }
        private void FileWriterCpuID()
        {
            string cpuid = HardwareInfo.GetCPUSerialNumber();
            string filename = Environment.CurrentDirectory + "\\" + "onlineFF.txt";
            File.WriteAllText(filename, cpuid);
        }
        private string DecryptString(string str)
        {
            return Decrypt(str, publicKey);
        }
        private string GetMD5String(string str)
        {
            str = string.Concat(md5Begin, str, md5End);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.Unicode.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string md5String = string.Empty;
            foreach (var b in targetData)
                md5String += b.ToString("x2");
            return md5String;
        }
        private string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }
}
