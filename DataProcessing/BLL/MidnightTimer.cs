using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.BLL
{
    internal class MidnightTimer
    {
        private int hour = 12;
        private int minute = 14;
        private int seconds = 00;
        public void TimeCheck()
        {
            while (true)
            {
                if ((hour == System.DateTime.Now.Hour) && (minute == System.DateTime.Now.Minute) && (seconds == System.DateTime.Now.Second))
                {
                    Console.WriteLine("Проверка" + DateTime.Now);
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
