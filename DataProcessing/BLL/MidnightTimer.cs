using DataProcessing.BLL.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.BLL
{
    internal class MidnightTimer
    {
        private int hour = 15;
        private int minute = 11;
        private int seconds = 00;
        private FolderAndFile folderAndFile;

        public MidnightTimer()
        {
            folderAndFile = new FolderAndFile();
        }

        public void TimeCheck()
        {
            while (true)
            {
                if ((hour == System.DateTime.Now.Hour) && (minute == System.DateTime.Now.Minute) && (seconds == System.DateTime.Now.Second))
                {
                    MetaLog.CreateMetaLogFile(folderAndFile.CreateMetaLogFileName());
                }
                System.Threading.Thread.Sleep(999);
            }
        }
    }
}
