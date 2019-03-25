using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.UnitsOfWork.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWorkGroup _unitOfWork;
        private readonly IDataAdapter _dataAdapter;
           
        public GroupService(IUnitOfWorkGroup unitOfWork, IDataAdapter dataAdapter)
        {
            _unitOfWork = unitOfWork;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<GroupDTO>> GetListAsync()
        {
            IEnumerable<Group> groups = await _unitOfWork.Groups.GetListAsync();
            return _dataAdapter.Map<IEnumerable<GroupDTO>>(groups);
        }

        public async Task<GroupDTO> GetByIdAsync(int id)
        {
            Group group = await _unitOfWork.Groups.GetByIdAsync(id);
            return _dataAdapter.Map<GroupDTO>(group);
        }

        public async Task<GroupDTO> AddAsync(GroupDTO groupDTO)
        {
            Group group = _dataAdapter.Map<Group>(groupDTO);
            _unitOfWork.Groups.Create(group);
            await _unitOfWork.SaveAsync();

            return _dataAdapter.Map<GroupDTO>(group);
        }

        public async Task<GroupDTO> UpdateAsync(int id, GroupDTO updateGroupDTO)
        {
            Group currentGroup = await _unitOfWork.Groups.GetByIdAsync(id);
            Group updateGroup = _dataAdapter.Map<Group>(updateGroupDTO);
            _dataAdapter.Update<Group>(updateGroup, currentGroup);
            _unitOfWork.Groups.Update(currentGroup);
            await _unitOfWork.SaveAsync();

            return _dataAdapter.Map<GroupDTO>(currentGroup);
        }

        public async Task DeleteAsync(int id)
        {
            Group group = await _unitOfWork.Groups.GetByIdAsync(id);

            if (group == null)
            {
                return;
            }

            _unitOfWork.Groups.Delete(group);
            await _unitOfWork.SaveAsync();
        }
    }
}

