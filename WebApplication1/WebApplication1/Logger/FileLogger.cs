using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Logger
{
    public class FileLogger
    {
        public void LogException(Exception e)
        {
            File.WriteAllLines("D://Error//" + DateTime.Now.ToString("dd-MM-yyyy mm hh ss") + ".log", 
                new string[] 
                {
                    "Message" + e.Message,"Stacktrace:" + e.StackTrace
                });
        }
    }
}