using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class InfoService : IInfoService
    {
        private readonly IRepository _repository;
        private readonly IDataAdapter _dataAdapter;
           
        public InfoService(IRepository repository, IDataAdapter dataAdapter)
        {
            _repository = repository;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<LanguageDTO>> GetLanguagesListAsync()
        {
            IEnumerable<Language> languages = await _repository.GetListAsync<Language>();
            return _dataAdapter.Map<IEnumerable<LanguageDTO>>(languages);
        }
    }
}

