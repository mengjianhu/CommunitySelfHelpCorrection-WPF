using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainHelper
{
    public class SqjzbyHelper
    {
        /// <summary>
        /// 获取社区矫正人员编号
        /// </summary>
        /// <param name="xzqh">行政区划分6位</param>
        /// <param name="rybh">最后一个人员编号</param>
        /// <returns></returns>
        public static string getRybh(string xzqh, string rybh)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int bhNum = int.Parse(rybh.Substring(rybh.Length - 4));
            bhNum++;
            if (bhNum > 9999)
            {
                bhNum = 1;
                month += 1;
            }
            StringBuilder bmStr = new StringBuilder();
            for (int i = 0; i < (4 - bhNum.ToString().Length); i++)
            {
                bmStr.Append("0");
            }
            bmStr.Append(bhNum);
            string monthStr = month < 10 ? "0" + month : month.ToString();
            return xzqh + year + monthStr + bmStr;
        }
    }
}
