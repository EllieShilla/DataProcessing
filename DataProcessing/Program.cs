using DataProcessing.BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataProcessing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MidnightTimer midnightTimer = new MidnightTimer();

            Thread timeCheck = new Thread(() => midnightTimer.TimeCheck());
            Thread fileTrack = new Thread(() => new FileTracker());

            timeCheck.Start();
            fileTrack.Start();
        }

    }

}
