using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IFileStorageManager
    {
        Task SaveFileAsync(IFormFile fileStream, string path, FileMode fileMode);
    }
}
