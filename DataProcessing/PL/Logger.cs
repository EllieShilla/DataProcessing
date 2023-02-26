using DataProcessing.BLL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.PL
{
    internal class Logger
    {
        public Logger()
        {
            CreateSerilog();
        }

        public void CreateSerilog()
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.File(ConfigItems.loggerFilePath, rollingInterval: RollingInterval.Day)
               .CreateLogger();
        }
    }
}
