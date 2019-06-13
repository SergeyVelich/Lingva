using Lingva.BC.Dto;
using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IGroupManager
    {
        Task<IEnumerable<GroupDto>> GetListAsync();
        Task<IEnumerable<GroupDto>> GetListAsync(IQueryOptions queryOptions);
        Task<int> CountAsync(IQueryOptions queryOptions);

        Task<GroupDto> GetByIdAsync(int id);

        Task<GroupDto> AddAsync(GroupDto groupDto);

        Task<GroupDto> UpdateAsync(GroupDto groupDto);

        Task DeleteAsync(int id);
    }
}
