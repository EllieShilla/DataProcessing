using DataProcessing.BLL.Log;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DataProcessing.BLL
{
    internal class FileManagement
    {
        private FolderAndFile folderAndFile;

        public FileManagement()
        {
            folderAndFile = new FolderAndFile();
        }

        public async Task<List<PaymentData>> ReadFile(string filePath)
        {
            List<PaymentData> _paymentData = new List<PaymentData>();

            var reader = new FileProcessFactory().GetFileReader(new FileInfo(filePath));
            LineParse lineParse = new LineParse();

            foreach (var str in await reader.ReadFileByLineAsync())
            {
                _paymentData.Add(lineParse.ParsingData(str));
            }

            _paymentData.RemoveAll(item => item == null);
            MetaLog.ParsedFilesCount();
            MetaLog.ParsedLinesCount(_paymentData.Count);

            return _paymentData;
        }

        public void WriteFile(List<PaymentData> _paymentData)
        {
            PaymentDataList paymentDataList = new PaymentDataList(_paymentData);
            paymentDataList.CreatePaymentDataList();
            folderAndFile.JsonSerializerAndSave(paymentDataList.ReturnPaymentDataList());
        }
    }
}
