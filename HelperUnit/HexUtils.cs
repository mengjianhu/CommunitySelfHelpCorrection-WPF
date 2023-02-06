using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperUnit
{
    class HexUtils
    {
        private const string pattern = "^[a-f0-9]+$";

        public static bool isHex(string value)
        {
            if (null == value)
            {
                return false;
            }
            return System.Text.RegularExpressions.Regex.IsMatch(value, pattern);
        }

        public static byte[] decode(String str)
        {
            if (null == str)
            {
                return null;
            }
            if (isHex(str))
            {
                return hexStrToByte(str);
            }
            return Org.BouncyCastle.Utilities.Encoders.Base64.Decode(str);
        }

        public static byte[] hexStrToByte(String hexStr)
        {
            if ((null == hexStr) || (hexStr.Length == 0))
            {
                return null;
            }
            char[] hexData = hexStr.ToCharArray();
            int len = hexData.Length;
            if ((len & 0x1) != 0)
            {
                throw new SystemException("Odd number of characters.");
            }
            byte[] out1 = new byte[len >> 1];

            int i = 0;
            for (int j = 0; j < len; i++)
            {
                int f = toDigit(hexData[j], j) << 4;
                j++;
                f |= toDigit(hexData[j], j);
                j++;
                out1[i] = ((byte)(f & 0xFF));
            }
            return out1;
        }

        private static int toDigit(char ch, int index)
        {
            int digit = Convert.ToInt32(ch.ToString(), 16);
            //int digit = Character.digit(ch, 16);
            if (digit == -1)
            {
                throw new SystemException("Illegal hexadecimal character " + ch + " at index " + index);
            }
            return digit;
        }
    }
}
