using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.UnitsOfWork.Contracts;
using System.Collections.Generic;

namespace Lingva.BC.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWorkGroupManagement _unitOfWork;
        private readonly IDataAdapter _dataAdapter;
           
        public GroupService(IUnitOfWorkGroupManagement unitOfWork, IDataAdapter dataAdapter)
        {
            _unitOfWork = unitOfWork;
            _dataAdapter = dataAdapter;
        }

        public IEnumerable<GroupDTO> GetGroupsList()
        {
            IEnumerable<Group> groups = _unitOfWork.Groups.GetList();
            return _dataAdapter.Map<IEnumerable<GroupDTO>>(groups);
        }

        public GroupDTO GetGroup(int id)
        {
            Group group = _unitOfWork.Groups.Get(id);
            return _dataAdapter.Map<GroupDTO>(group);
        }

        public GroupDTO AddGroup(GroupDTO groupDTO)
        {
            var group = _dataAdapter.Map<Group>(groupDTO);
            var result = _unitOfWork.Groups.Create(group);
            _unitOfWork.Save();

            return _dataAdapter.Map<GroupDTO>(result);
        }

        public GroupDTO UpdateGroup(int id, GroupDTO updateGroupDTO)
        {
            Group currentGroup = _unitOfWork.Groups.Get(id);
            Group updateGroup = _dataAdapter.Map<Group>(updateGroupDTO);
            _dataAdapter.Update<Group>(updateGroup, currentGroup);
            _unitOfWork.Groups.Update(currentGroup);
            _unitOfWork.Save();

            return _dataAdapter.Map<GroupDTO>(currentGroup);
        }

        public void DeleteGroup(int id)
        {
            Group group = _unitOfWork.Groups.Get(id);

            if (group == null)
            {
                return;
            }

            _unitOfWork.Groups.Delete(group);
            _unitOfWork.Save();
        }
    }
}

