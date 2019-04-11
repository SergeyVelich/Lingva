using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.UnitsOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class InfoService : IInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAdapter _dataAdapter;
           
        public InfoService(IUnitOfWork unitOfWork, IDataAdapter dataAdapter)
        {
            _unitOfWork = unitOfWork;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<LanguageDTO>> GetLanguagesListAsync()
        {
            IEnumerable<Language> languages = await _unitOfWork.Repository.GetListAsync<Language>();
            return _dataAdapter.Map<IEnumerable<LanguageDTO>>(languages);
        }
    }
}

