using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataProcessing.BLL
{
    internal class FolderAndFile
    {
        public string CreateSubFolder()
        {
            string subFolderName = $@"{ConfigItems.fileBPath}\{DateTime.Today.ToString("MM-dd-yyyy")}";

            if (!Directory.Exists(subFolderName))
            {
                Directory.CreateDirectory(subFolderName);
            }

            return subFolderName;
        }

        public string CreateFileInSubfolder()
        {
            string subFolderName = CreateSubFolder();
            string[] files = new DirectoryInfo(subFolderName).GetFiles().Select(o => o.Name).ToArray();
            int countFile = files.Count(i => i.Contains("output"));

            if (countFile == 0)
                return string.Concat($"{subFolderName}/", "output1.json");
            else
                return string.Concat($"{subFolderName}/", $"output{countFile += 1}.json");
        }

        public void JsonSerializerAndSave(List<PaymentData> paymentDataList)
        {
            var updatedJsonString = JsonConvert.SerializeObject(paymentDataList);
            File.WriteAllText(CreateFileInSubfolder(), updatedJsonString);
        }

        public string CreateMetaLogFileName()
        {
            return string.Concat($"{CreateSubFolder()}/", "metaLog.log");
        }
    }
}
