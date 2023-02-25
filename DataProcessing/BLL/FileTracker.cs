using DataProcessing.BLL;
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
        List<PaymentData> _paymentData;
        private PaymentDataList paymentDataList;
        private FolderAndFile folderAndFile;
        public FileTracker()
        {
            _paymentData = new List<PaymentData>();
            folderAndFile = new FolderAndFile();
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
                await readAndWriteFile(e.FullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Can not access a file: {e.FullPath} {ex.Message}");
            }
        }

        private async Task readAndWriteFile(string filePath)
        {
            var reader = new FileProcessFactory().GetFileReader(new FileInfo(filePath));
            TransformData transformData = new TransformData();

            foreach (var str in await reader.ReadFileByLineAsync())
            {
                _paymentData.Add(transformData.ParsingData(str));
            }

            _paymentData.RemoveAll(item => item == null);
            paymentDataList = new PaymentDataList(_paymentData);
            paymentDataList.CreatePaymentDataList();
            folderAndFile.JsonSerializerAndSave(paymentDataList.ReturnPaymentDataList());
            _paymentData.Clear();
        }
    }
}
