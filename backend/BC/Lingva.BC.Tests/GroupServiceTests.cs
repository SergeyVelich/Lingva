using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.BC.Services;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Moq;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Lingva.BC.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class GroupServiceTests
    {
        private readonly List<Group> _groupList;
        private readonly List<GroupDto> _groupListDto;
        private IGroupService _groupService;
        private readonly Mock<IRepository> _repoMock;
        private readonly Mock<IDataAdapter> _dataAdapter;

        public GroupServiceTests()
        {
            _groupList = new List<Group>
            {
                new Group
                {
                    Id = 1,
                    Name = "Harry Potter",
                    Date = DateTime.Now,
                    Description = "Description",
                    Picture = "Picture",
                    LanguageId = 1,
                    Language = new Language
                    {
                        Id = 1,
                        Name = "en",
                    },
                },
                new Group
                {
                    Id = 2,
                    Name = "Librium",
                    Date = DateTime.Now,
                    Description = "Description",
                    Picture = "Picture",
                    LanguageId = 2,
                    Language = new Language
                    {
                        Id = 2,
                        Name = "ru",
                    },
                }
            };

            _groupListDto = new List<GroupDto>
            {
                new GroupDto
                {
                    Id = 1,
                    Name = "Harry Potter",
                    Date = DateTime.Now,
                    Description = "Description",
                    Picture = "Picture",
                    LanguageId = 1,
                },
                new GroupDto
                {
                    Id = 12,
                    Name = "Librium",
                    Date = DateTime.Now,
                    Description = "Description",
                    Picture = "Picture",
                    LanguageId = 2
                }
            };           

            _repoMock = new Mock<IRepository>();
            _dataAdapter = new Mock<IDataAdapter>();
            _groupService = new GroupService(_repoMock.Object, _dataAdapter.Object);
        }

        [Fact]
        public async Task GetListAsync_ShouldNot_Return_NotNull()
        {
            //arrange
            _repoMock.Setup(r => r.GetListAsync<Group>()).Returns(Task.FromResult<IEnumerable<Group>>(_groupList));
            _groupService = new GroupService(_repoMock.Object, _dataAdapter.Object);

            //act
            var result = await _groupService.GetListAsync();

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetListAsync_Should_Return_SetValue()
        {
            //arrange
            _repoMock.Setup(r => r.GetListAsync<Group>()).Returns(Task.FromResult<IEnumerable<Group>>(_groupList));
            _groupService = new GroupService(_repoMock.Object, _dataAdapter.Object);
            _dataAdapter.Setup(d => d.Map<IEnumerable<GroupDto>>(_groupList)).Returns(_groupListDto);

            //act
            var result = await _groupService.GetListAsync();

            //assert
            Assert.True(result.Count() == _groupList.Count());
        }

        [Fact]
        public async Task GetListAsyncWithOptions_ShouldNot_Return_NotNull()
        {
            //arrange
            _repoMock.Setup(r => r.GetListAsync<Group>()).Returns(Task.FromResult<IEnumerable<Group>>(_groupList));
            _groupService = new GroupService(_repoMock.Object, _dataAdapter.Object);
            _dataAdapter.Setup(d => d.Map<IEnumerable<GroupDto>>(_groupList)).Returns(_groupListDto);

            Mock<IQueryOptions> queryOptions = new Mock<IQueryOptions>();
            queryOptions.Setup(q => q.GetFiltersExpression<Group>()).Returns<Group>(null);
            queryOptions.Setup(q => q.GetSortersCollection<Group>()).Returns<Group>(null);
            queryOptions.Setup(q => q.GetIncludersCollection<Group>()).Returns<Group>(null);
            queryOptions.Setup(q => q.Pagenator).Returns<Group>(null);

            //act
            var result = await _groupService.GetListAsync(queryOptions.Object);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByValidId_ShouldNot_Return_Null()
        {
            //arrange
            int validId = 1;
            Group group = new Group { Id = validId };
            GroupDto groupDto = new GroupDto { Id = validId };

            _dataAdapter.Setup(d => d.Map<GroupDto>(group)).Returns(groupDto);
            _repoMock.Setup(r => r.GetByIdAsync<Group>(validId)).Returns(Task.FromResult(group));
            _groupService = new GroupService(_repoMock.Object, _dataAdapter.Object);

            //act
            var order = await _groupService.GetByIdAsync(validId);

            //assert
            Assert.NotNull(order);
        }

        [Fact]
        public async Task GetByInvalidId_Should_Return_Null()
        {
            //arrange
            int invalidId = -1;

            _repoMock.Setup(r => r.GetByIdAsync<Group>(invalidId)).Returns(Task.FromResult<Group>(null));
            _groupService = new GroupService(_repoMock.Object, _dataAdapter.Object);

            //act
            var order = await _groupService.GetByIdAsync(invalidId);

            //assert
            Assert.Null(order);
        }

        [Fact]
        public async Task AddAsync_WasExecute()
        {
            //arrange
            Group group = new Group { Id = 1 };

            _repoMock.Setup(r => r.CreateAsync<Group>(null));
            _dataAdapter.Setup(d => d.Map<Group>(null)).Returns(group);
            _groupService = new GroupService(_repoMock.Object, _dataAdapter.Object);

            //act
            await _groupService.AddAsync(null);

            //assert
            _repoMock.Verify(mock => mock.CreateAsync(group), Times.Once());
        }

        [Fact]
        public async Task UpdateAsync_WasExecute()
        {
            //arrange
            Group group = new Group { Id = 1 };
            GroupDto groupDto = new GroupDto { Id = 1 };

            _repoMock.Setup(r => r.GetByIdAsync<Group>(group.Id)).Returns(Task.FromResult(group));
            _repoMock.Setup(r => r.UpdateAsync(group)).Returns(Task.FromResult(group));
            _groupService = new GroupService(_repoMock.Object, _dataAdapter.Object);

            //act
            await _groupService.UpdateAsync(groupDto);

            //assert
            _repoMock.Verify(mock => mock.UpdateAsync(group), Times.Once());
        }

        [Fact]
        public async Task DeleteAsync_WasExecute()
        {
            //arrange
            Group group = new Group { Id = 1 };

            _repoMock.Setup(r => r.DeleteAsync<Group>(group.Id));
            _dataAdapter.Setup(d => d.Map<Group>(null)).Returns(group);
            _groupService = new GroupService(_repoMock.Object, _dataAdapter.Object);

            //act
            await _groupService.DeleteAsync(group.Id);

            //assert
            _repoMock.Verify(mock => mock.DeleteAsync<Group>(group.Id), Times.Once());
        }
    }
}
