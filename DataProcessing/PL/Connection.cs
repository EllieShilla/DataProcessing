using DataProcessing.BLL;
using DataProcessing.BLL.ReadFiles;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataProcessing.PL
{
    internal class Connection
    {
        private Thread timeCheck;
        private Thread fileTrack;
        private MidnightTimer midnightTimer;
        private string command = "";
        private Logger logger;
        private FirstStart firstStart;
        public Connection()
        {
            Initialize();
        }

        private void Initialize()
        {
            logger = new Logger();
            firstStart = new FirstStart();

            midnightTimer = new MidnightTimer();
            timeCheck = new Thread(() => midnightTimer.TimeCheck());
            fileTrack = new Thread(() => new FileTracker());
            startTimerAndTrackerAsync();
        }

        private void startTimerAndTrackerAsync()
        {
            if (ConfigItems.CheckConfigFileIsPresent() && ConfigItems.CheckConfigFileName_A() && ConfigItems.CheckConfigFileName_B())
            {
                new Action(async () => await firstStart.ProcesTheFileOnTheFirstStartup())();

                timeCheck.Start();
                fileTrack.Start();
                WaitForInstructions();
            }
            else
            {
                Console.WriteLine("Problem with App.config");
                Console.ReadLine();
            }
        }

        private void stopTimerAndTracker()
        {
            timeCheck.Abort();
            fileTrack.Abort();
        }

        public void WaitForInstructions()
        {
            while (!command.ToLower().Equals("stop"))
            {
                Console.Write("Type command(reload or stop): ");
                command = Console.ReadLine();

                switch (command)
                {
                    case "reload":
                        {
                            stopTimerAndTracker();
                            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                            Environment.Exit(0);
                        }
                        break;
                    case "stop":
                        {
                            stopTimerAndTracker();
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
