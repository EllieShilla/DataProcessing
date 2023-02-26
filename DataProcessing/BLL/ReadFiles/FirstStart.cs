using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataProcessing.BLL.ReadFiles
{
    internal class FirstStart
    {
        private FileManagement fileManagement;

        public FirstStart()
        {
            fileManagement = new FileManagement();
        }

        public string[] CheckFolder()
        {
            string[] files = new DirectoryInfo(ConfigItems.fileAPath).GetFiles()
                                                                     .Where(i => i.Extension.Equals(".txt") || i.Extension.Equals(".csv"))
                                                                     .Select(o => o.FullName)
                                                                     .ToArray();

            return files;
        }

        public async Task ProcesTheFileOnTheFirstStartup()
        {

            foreach (var file in CheckFolder())
            {
                var dataList = await fileManagement.ReadFile(file);
                if (dataList.Count > 0)
                    fileManagement.WriteFile(dataList);

            }
        }
    }
}
