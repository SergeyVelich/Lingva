using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IDataAdapter _dataAdapter;
           
        public UserService(IRepository repository, IDataAdapter dataAdapter)
        {
            _repository = repository;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<UserDto>> GetListAsync(IQueryOptions queryOptions)
        {
            IEnumerable<User> users = await _repository.GetListAsync<User>(queryOptions);

            return _dataAdapter.Map<IEnumerable<UserDto>>(users);
        }
    }
}

