using Lingva.BC.DTO;
using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDTO>> GetListAsync(QueryOptions queryOptions);

        Task<GroupDTO> GetByIdAsync(int id);

        Task<GroupDTO> AddAsync(GroupDTO groupDTO);

        Task<GroupDTO> UpdateAsync(int id, GroupDTO groupDTO);

        Task DeleteAsync(GroupDTO groupDTO);
    }
}
