using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Authorization
{
    class RegistFileHelper
    {

        public static string RegistInfofile = Environment.CurrentDirectory + "\\nw.lic";


        public static string ReadRegistFile()
        {
            return ReadFile(RegistInfofile);
        }


        public static bool ExistRegistInfofile()
        {
            return File.Exists(RegistInfofile);
        }

        private static string ReadFile(string fileName)
        {
            string info = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    info = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return info;
        }
    }
}

