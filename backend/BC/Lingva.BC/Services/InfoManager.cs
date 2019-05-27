using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class InfoManager : IInfoManager
    {
        private readonly IRepository _repository;
        private readonly IDataAdapter _dataAdapter;
           
        public InfoManager(IRepository repository, IDataAdapter dataAdapter)
        {
            _repository = repository;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<LanguageDto>> GetLanguagesListAsync()
        {
            IEnumerable<Language> languages = await _repository.GetListAsync<Language>();
            return _dataAdapter.Map<IEnumerable<LanguageDto>>(languages);
        }
    }
}

