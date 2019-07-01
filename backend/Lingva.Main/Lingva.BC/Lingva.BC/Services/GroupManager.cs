using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class GroupManager : IGroupManager
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IDataAdapter _dataAdapter;
           
        public GroupManager(IGroupRepository groupRepository, IDataAdapter dataAdapter)
        {
            _groupRepository = groupRepository;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<GroupDto>> GetListAsync()
        {
            IEnumerable<Group> groups = await _groupRepository.GetListAsync<Group>();

            return _dataAdapter.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<IEnumerable<GroupDto>> GetListAsync(IQueryOptions queryOptions)
        {
            IEnumerable<Group> groups = await _groupRepository.GetListAsync<Group>(queryOptions);

            return _dataAdapter.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<int> CountAsync(IQueryOptions queryOptions)
        {
            return await _groupRepository.CountAsync<Group>(queryOptions);
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            Group group = await _groupRepository.GetByIdAsync<Group>(id);
            return _dataAdapter.Map<GroupDto>(group);
        }

        public async Task<GroupDto> AddAsync(GroupDto groupDto)
        {
            Group group = _dataAdapter.Map<Group>(groupDto);
            await _groupRepository.CreateAsync(group);

            return _dataAdapter.Map<GroupDto>(group);
        }

        public async Task<GroupDto> UpdateAsync(GroupDto groupDto)
        {
            Group currentGroup = await _groupRepository.GetByIdAsync<Group>(groupDto.Id);
            Group updateGroup = _dataAdapter.Map<Group>(groupDto);
            _dataAdapter.Update(updateGroup, currentGroup);
            await _groupRepository.UpdateAsync(currentGroup);

            return _dataAdapter.Map<GroupDto>(currentGroup);
        }

        public async Task DeleteAsync(int id)
        {
            await _groupRepository.DeleteAsync<Group>(id);
        }
    }
}

