using Lingva.BC.DTO;
using System.Collections.Generic;

namespace Lingva.BC.Contracts
{
    public interface IGroupService
    {
        IEnumerable<GroupDTO> GetGroupsList();

        GroupDTO GetGroup(int id);

        GroupDTO AddGroup(GroupDTO group);

        GroupDTO UpdateGroup(int id, GroupDTO group);

        void DeleteGroup(int id);
    }
}
