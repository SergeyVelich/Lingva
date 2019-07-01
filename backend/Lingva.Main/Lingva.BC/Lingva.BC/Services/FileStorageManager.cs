using System.IO;
using System.Threading.Tasks;
using Lingva.BC.Contracts;
using Microsoft.AspNetCore.Http;

namespace Lingva.BC.Services
{
    public class FileStorageManager : IFileStorageManager
    {
        public async Task SaveFileAsync(IFormFile file, string path, FileMode fileMode)
        {
            using (var newFileStream = new FileStream(path, fileMode))
            {
                await file.CopyToAsync(newFileStream);
            }
        }
    }
}
