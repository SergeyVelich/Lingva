using Lingva.BC.Dto;
using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetListAsync(IQueryOptions queryOptions);
    }
}
