using Lingva.BC.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDTO>> GetListAsync(QueryOptionsDTO optionsDTO);

        Task<GroupDTO> GetByIdAsync(int id);

        Task<GroupDTO> AddAsync(GroupDTO groupDTO);

        Task<GroupDTO> UpdateAsync(int id, GroupDTO groupDTO);

        Task DeleteAsync(GroupDTO groupDTO);
    }
}
