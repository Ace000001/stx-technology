using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TouchSystems.Data.Helper
{
    public  class Utilities
    {
        public static void WriteTextLog(string action, string strMessage, DateTime time)
        {
            string wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "System", "Log");

            string path = wwwroot;//AppDomain.CurrentDomain.BaseDirectory + @"Systemd\Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = Path.Combine(path, time.ToString("yyyy-MM-dd") + ".System.txt");
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time.ToString() + "\r\n");
            str.Append("Action:  " + action + "\r\n");
            str.Append("Message: " + strMessage + "\r\n");
            str.Append("-----------------------------------------------------------\r\n\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }


        public static void WriteTextLog(string strMessage)
        {

            string wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "System", "Log");


            DateTime time = DateTime.Today;
            string path = wwwroot; //+ @"System2\Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = Path.Combine(path, time.ToString("yyyy-MM-dd") + ".System.txt");
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time.ToString() + "\r\n");
            str.Append("Message: " + strMessage + "\r\n");
            str.Append("-----------------------------------------------------------\r\n\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }

        public static void CreateDirectory(string foldername)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Drive\" + foldername + @"\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

    }
}
