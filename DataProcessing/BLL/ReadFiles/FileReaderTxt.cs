using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    internal class FileReaderTxt : IFileReader
    {
        readonly Encoding _encoder;
        readonly string _filePath;
        private List<string> strContent;

        public FileReaderTxt(Encoding encoder, string filePath)
        {
            _encoder = encoder;
            _filePath = filePath;
            strContent = new List<string>();
        }
        public async Task<List<string>> ReadFileByLineAsync()
        {

            if (new FileInfo(_filePath).Exists)
            {
                using (var streamReader = new StreamReader(_filePath, _encoder))
                {
                    while (!streamReader.EndOfStream)
                    {
                        strContent.Add(await streamReader.ReadLineAsync());
                    }
                }

                return strContent;
            }
            else
            {
                throw new Exception("File does not exist");
            }
        }
    }
}
