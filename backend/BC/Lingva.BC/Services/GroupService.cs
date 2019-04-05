using Lingva.BC.Common.Enums;
using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.UnitsOfWork.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
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

        public async Task<IEnumerable<GroupDTO>> GetListAsync(QueryOptionsDTO optionsDTO)
        {
            Expression<Func<Group, bool>> filters = GetFiltersExpression(optionsDTO.Filters);
            IEnumerable<string> sorters = GetSortersExpression(optionsDTO.Sorters);
            int skip = optionsDTO.Pagenator.Skip;
            int take = optionsDTO.Pagenator.Take;

            IEnumerable<Group> groups = await _unitOfWork.Groups.GetListAsync(filters, sorters, skip, take);

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

        public async Task DeleteAsync(GroupDTO groupDTO)
        {
            Group group = _dataAdapter.Map<Group>(groupDTO);
            _unitOfWork.Groups.Delete(group);
            await _unitOfWork.SaveAsync();
        }








        private Expression<Func<Group, bool>> GetFiltersExpression(ICollection<QueryFilterDTO> filterModel)
        {
            if(filterModel == null)
            {
                return null;
            }
            if (filterModel.Count == 0)
            {
                return null;
            }
           
            Expression expression = null;
            var parameter = Expression.Parameter(typeof(Group), "x");

            foreach (var filter in filterModel)
            {
                var property = Expression.Property(parameter, filter.PropertyName);
                var propertyInfo = typeof(Group).GetProperty(filter.PropertyName);
                var typeForValue = propertyInfo.PropertyType;
                var constant = Expression.Constant(Convert.ChangeType(filter.PropertyValue, typeForValue));

                Expression subExpression = null;

                switch (filter.Operation)
                {
                    case FilterOperation.Equal:
                        subExpression = Expression.Equal(property, constant);
                        break;
                    case FilterOperation.NotEqual:
                        subExpression = Expression.NotEqual(property, constant);
                        break;
                    case FilterOperation.Less:
                        subExpression = Expression.LessThan(property, constant);
                        break;
                    case FilterOperation.More:
                        subExpression = Expression.GreaterThan(property, constant);
                        break;
                    case FilterOperation.Contains:
                        MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        subExpression = Expression.Call(property, method, constant);
                        //subExpression = Expression.Lambda<Func<Group, bool>>(containsMethodExp, parameter);
                        break;
                    case FilterOperation.NotContains:
                        method = typeof(string).GetMethod("NotContains", new[] { typeof(string) });
                        subExpression = Expression.Call(property, method, constant);
                        //subExpression = Expression.Lambda<Func<Group, bool>>(containsMethodExp, parameter);
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }

                //switch (filter.SqlCondition)
                //{
                //    case SqlCondition.And:
                        expression = expression == null ? subExpression : Expression.AndAlso(expression, subExpression);
                    //    break;
                    //case SqlCondition.Or:
                    //    expression = expression == null ? subExpression : Expression.OrElse(expression, subExpression);
                    //    break;
                    //default: throw new ArgumentOutOfRangeException();
                //}
            }
            var exp = Expression.Lambda<Func<Group, bool>>(expression ?? throw new InvalidOperationException(), parameter);

            return exp;
        }

        private ICollection<string> GetSortersExpression(ICollection<QuerySorterDTO> sorterModel)
        {
            if (sorterModel == null)
            {
                return null;
            }
            if (sorterModel.Count == 0)
            {
                return null;
            }

            ICollection<string> sorters = new List<string>();//??
            foreach (var sorter in sorterModel)
            {
                sorters.Add(sorter.PropertyName + " " + sorter.SortOrder.ToString());
            }

            return sorters;
        }
    }
}

