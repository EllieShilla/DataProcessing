using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    public class FileProcessFactory
    {
        public IFileReader GetFileReader(FileInfo file)
        {
            if (file is null) throw new Exception($"file {file.Name} is not exist");

            switch (file.Extension.ToLower())
            {
                case ".txt":
                    return new FileReaderTxt(Encoding.UTF8, file.FullName);
                case ".csv":
                    return new FileReaderCsv(Encoding.UTF8, file.FullName);
                default:
                    throw new NotImplementedException();
                    break;
            }

        }
    }
}
