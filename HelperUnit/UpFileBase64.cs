using InterfaceReturnEntity;
using Newtonsoft.Json;
using RainHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelperUnit
{
    public class UpFileBase64
    {
        InterfaceHelper interfaceHelper = new InterfaceHelper();
        public string getFileID(string base64, string fileName, string token)
        {
            try
            {
                //MessageBox.Show(base64.Substring(1,10));
                //MessageBox.Show(fileName);
                //MessageBox.Show(token);
                baseFileUploadInfo baseFile = new baseFileUploadInfo();
                baseFile.base64 = base64;
                baseFile.fileName = fileName;
                string ss = JsonConvert.SerializeObject(baseFile);
                string res = interfaceHelper.baseFileUpload(ss, token);
                UpLoadFileResult result = JsonConvert.DeserializeObject<UpLoadFileResult>(res);
                // MessageBox.Show(res);
                return result.rows[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传文件信息获取文件编号异常：" + ex.Message);
                return null;
            }
            
        }
    }
   
}
