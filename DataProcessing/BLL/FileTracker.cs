using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    public class FileTracker
    {
        private readonly string _fileName = System.Configuration.ConfigurationManager.AppSettings["folderPathA"];
        private readonly FileSystemWatcher _watcher;
        public FileTracker()
        {
            _watcher = new FileSystemWatcher(_fileName);
            _watcher.EnableRaisingEvents = true;
            _watcher.Created += Watcher_Created;
            _watcher.Error += Watcher_Error;
            Console.ReadLine();
        }

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"Error: {e.GetException().Message}");
        }

        private async void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.Name} {e.ChangeType}");

            try
            {
                var reader = new FileProcessFactory().GetFileReader(new FileInfo(e.FullPath));
                TransformData transformData = new TransformData();

                foreach (var str in await reader.ReadFileByLineAsync())
                {
                    transformData.Transform(str);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can not access a file: {e.FullPath} {ex.Message}");
            }
        }
    }
}
