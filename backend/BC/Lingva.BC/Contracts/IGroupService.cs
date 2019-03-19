using Lingva.BC.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDTO>> GetGroupsListAsync();

        Task<GroupDTO> GetGroupAsync(int id);

        Task<GroupDTO> AddGroupAsync(GroupDTO group);

        Task<GroupDTO> UpdateGroupAsync(int id, GroupDTO group);

        Task DeleteGroupAsync(int id);
    }
}
