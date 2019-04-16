using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository _repository;
        private readonly IDataAdapter _dataAdapter;
           
        public GroupService(IRepository repository, IDataAdapter dataAdapter)
        {
            _repository = repository;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<GroupDto>> GetListAsync()
        {
            IEnumerable<Group> groups = await _repository.GetListAsync<Group>();

            return _dataAdapter.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<IEnumerable<GroupDto>> GetListAsync(IQueryOptions queryOptions)
        {
            IEnumerable<Group> groups = await _repository.GetListAsync<Group>(queryOptions);

            return _dataAdapter.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            Group group = await _repository.GetByIdAsync<Group>(id);
            return _dataAdapter.Map<GroupDto>(group);
        }

        public async Task<GroupDto> AddAsync(GroupDto groupDto)
        {
            Group group = _dataAdapter.Map<Group>(groupDto);
            await _repository.CreateAsync(group);

            return _dataAdapter.Map<GroupDto>(group);
        }

        public async Task<GroupDto> UpdateAsync(GroupDto groupDto)
        {
            Group currentGroup = await _repository.GetByIdAsync<Group>(groupDto.Id);
            Group updateGroup = _dataAdapter.Map<Group>(groupDto);
            _dataAdapter.Update(updateGroup, currentGroup);
            await _repository.UpdateAsync(currentGroup);

            return _dataAdapter.Map<GroupDto>(currentGroup);
        }

        public async Task DeleteAsync(GroupDto groupDto)
        {
            Group group = _dataAdapter.Map<Group>(groupDto);
            await _repository.DeleteAsync(group);
        }
    }
}

