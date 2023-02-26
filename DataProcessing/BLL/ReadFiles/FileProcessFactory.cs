using DataProcessing.BLL.Log;
using System;
using System.IO;
using System.Text;

namespace DataProcessing
{
    public class FileProcessFactory
    {
        public IFileReader GetFileReader(FileInfo file)
        {
            if (file is null)
                throw new Exception($"file {file.Name} is not exist");

            switch (file.Extension.ToLower())
            {
                case ".txt":
                    return new FileReaderTxt(Encoding.UTF8, file.FullName);
                case ".csv":
                    return new FileReaderCsv(Encoding.UTF8, file.FullName);
                default:
                    throw new Exception($"file type {file.Extension.ToLower()} is not processed");

            }

        }
    }
}
