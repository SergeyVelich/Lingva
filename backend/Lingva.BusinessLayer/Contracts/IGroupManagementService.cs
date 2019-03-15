using Lingva.DAL.Entities;
using System.Collections.Generic;

namespace Lingva.BC.Contracts
{
    public interface IGroupManagementService
    {
        IEnumerable<Group> GetGroupsList();

        Group GetGroup(int id);

        void AddGroup(Group group);

        void UpdateGroup(int id, Group group);

        void DeleteGroup(int id);
    }
}
