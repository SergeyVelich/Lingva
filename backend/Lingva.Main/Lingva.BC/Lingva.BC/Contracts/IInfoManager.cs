using Lingva.BC.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IInfoManager
    {
        Task<IEnumerable<LanguageDto>> GetLanguagesListAsync();
    }
}
