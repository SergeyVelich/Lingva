using Lingva.BC.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IInfoService
    {
        Task<IEnumerable<LanguageDTO>> GetLanguagesListAsync();
    }
}
