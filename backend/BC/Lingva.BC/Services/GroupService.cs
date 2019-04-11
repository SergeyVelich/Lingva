using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.UnitsOfWork;
using QueryBuilder;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAdapter _dataAdapter;
           
        public GroupService(IUnitOfWork unitOfWork, IDataAdapter dataAdapter)
        {
            _unitOfWork = unitOfWork;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<GroupDTO>> GetListAsync(QueryOptionsDTO optionsDTO)
        {
            Expression<Func<Group, bool>> filters = optionsDTO.GetFiltersExpression<Group>();
            IEnumerable<string> sorters = optionsDTO.GetSortersCollection<Group>();
            ICollection<Expression<Func<Group, bool>>> includers = null;
            //ICollection<Expression<Func<Group, bool>>> includers = optionsDTO.GetIncludersCollection<Group>();//??
            QueryPagenatorDTO pagenator = optionsDTO.Pagenator;
            int skip = pagenator.Skip;
            int take = pagenator.Take;

            IEnumerable<Group> groups = await _unitOfWork.Repository.GetListAsync<Group>(filters, sorters, includers, skip, take);

            return _dataAdapter.Map<IEnumerable<GroupDTO>>(groups);
        }

        public async Task<GroupDTO> GetByIdAsync(int id)
        {
            Group group = await _unitOfWork.Repository.GetByIdAsync<Group>(id);
            return _dataAdapter.Map<GroupDTO>(group);
        }

        public async Task<GroupDTO> AddAsync(GroupDTO groupDTO)
        {
            Group group = _dataAdapter.Map<Group>(groupDTO);
            _unitOfWork.Repository.Create<Group>(group);
            await _unitOfWork.SaveAsync();

            return _dataAdapter.Map<GroupDTO>(group);
        }

        public async Task<GroupDTO> UpdateAsync(int id, GroupDTO updateGroupDTO)
        {
            Group currentGroup = await _unitOfWork.Repository.GetByIdAsync<Group>(id);
            Group updateGroup = _dataAdapter.Map<Group>(updateGroupDTO);
            _dataAdapter.Update<Group>(updateGroup, currentGroup);
            _unitOfWork.Repository.Update<Group>(currentGroup);
            await _unitOfWork.SaveAsync();

            return _dataAdapter.Map<GroupDTO>(currentGroup);
        }

        public async Task DeleteAsync(GroupDTO groupDTO)
        {
            Group group = _dataAdapter.Map<Group>(groupDTO);
            _unitOfWork.Repository.Delete<Group>(group);
            await _unitOfWork.SaveAsync();
        }
    }
}

