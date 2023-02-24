using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataProcessing
{
    public interface IFileReader
    {
        Task<List<string>> ReadFileByLineAsync();
    }
}