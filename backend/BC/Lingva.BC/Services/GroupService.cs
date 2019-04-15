using Lingva.BC.Contracts;
using Lingva.BC.DTO;
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

        public async Task<IEnumerable<GroupDTO>> GetListAsync()
        {
            IEnumerable<Group> groups = await _repository.GetListAsync<Group>();

            return _dataAdapter.Map<IEnumerable<GroupDTO>>(groups);
        }

        public async Task<IEnumerable<GroupDTO>> GetListAsync(QueryOptions queryOptions)
        {
            IEnumerable<Group> groups = await _repository.GetListAsync<Group>(queryOptions);

            return _dataAdapter.Map<IEnumerable<GroupDTO>>(groups);
        }

        public async Task<GroupDTO> GetByIdAsync(int id)
        {
            Group group = await _repository.GetByIdAsync<Group>(id);
            return _dataAdapter.Map<GroupDTO>(group);
        }

        public async Task<GroupDTO> AddAsync(GroupDTO groupDTO)
        {
            Group group = _dataAdapter.Map<Group>(groupDTO);
            await _repository.CreateAsync<Group>(group);

            return _dataAdapter.Map<GroupDTO>(group);
        }

        public async Task<GroupDTO> UpdateAsync(int id, GroupDTO updateGroupDTO)
        {
            Group currentGroup = await _repository.GetByIdAsync<Group>(id);
            Group updateGroup = _dataAdapter.Map<Group>(updateGroupDTO);
            _dataAdapter.Update<Group>(updateGroup, currentGroup);
            await _repository.UpdateAsync<Group>(currentGroup);

            return _dataAdapter.Map<GroupDTO>(currentGroup);
        }

        public async Task DeleteAsync(GroupDTO groupDTO)
        {
            Group group = _dataAdapter.Map<Group>(groupDTO);
            await _repository.DeleteAsync<Group>(group);
        }
    }
}

