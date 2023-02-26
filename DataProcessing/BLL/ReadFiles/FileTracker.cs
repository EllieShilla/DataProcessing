using DataProcessing.BLL;
using DataProcessing.BLL.Log;
using Serilog;
using System;
using System.IO;

namespace DataProcessing
{
    public class FileTracker
    {
        private readonly FileSystemWatcher _watcher;
        private FileManagement fileManagement;
        public FileTracker()
        {
            fileManagement = new FileManagement();
            _watcher = new FileSystemWatcher(ConfigItems.fileAPath);
            _watcher.EnableRaisingEvents = true;
            _watcher.Created += Watcher_Created;
            _watcher.Error += Watcher_Error;
        }

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            MetaLog.FoundErrors();
            Log.Error($"Exception: {e.GetException().Message} \nPlace of occurrence: {e.GetException().TargetSite}");
        }

        private async void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                var dataList = await fileManagement.ReadFile(e.FullPath);
                if (dataList.Count > 0)
                    fileManagement.WriteFile(dataList);
            }
            catch (Exception ex)
            {
                MetaLog.FoundErrors();
                MetaLog.AddInvalidFilesItem(e.FullPath);
                Log.Error($"Exception: {ex.Message} \nPlace of occurrence: {ex.TargetSite}");
            }
        }
    }
}
