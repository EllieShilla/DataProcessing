using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace DataProcessing.BLL
{
    public static class ConfigItems
    {
        public static readonly string fileAPath = System.Configuration.ConfigurationManager.AppSettings["folderPathA"];
        public static readonly string fileBPath = System.Configuration.ConfigurationManager.AppSettings["folderPathB"];
        public static readonly string loggerFilePath = System.Configuration.ConfigurationManager.AppSettings["loggerFile"];
        public static bool CheckConfigFileIsPresent()
        {
            return File.Exists(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }

        public static bool CheckConfigFileName_A()
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains("folderPathA");
        }

        public static bool CheckConfigFileName_B()
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains("folderPathB");
        }
    }
}
