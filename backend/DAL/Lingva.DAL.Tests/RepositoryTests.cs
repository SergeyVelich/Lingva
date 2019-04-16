using Lingva.DAL.EF.Context;
using Lingva.DAL.EF.Repositories;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Lingva.DAL.EF.Tests
{
    [ExcludeFromCodeCoverage]
    public class RepositoryTests
    {
        private DictionaryContext _context;
        private Group _group;
        private List<Group> _groupList;

        public RepositoryTests()
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
                    Id = 12,
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

            IQueryable<Group> dbSet = Substitute.For<IQueryable<Group>>();
            dbSet.AsNoTracking();
            dbSet.ToListAsync().Returns(_groupList);
            DictionaryContext _context = Substitute.For<DictionaryContext>();
            _context.Set<Group>().Returns(dbSet);
        }

        //[Fact]
        //public async Task GetListAsync_ShouldNot_Return_NotNull()
        //{
        //    //arrange
        //    IRepository _repository = new Repository(_context);

        //    //act
        //    var result = await _repository.GetListAsync<Group>();

        //    //assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task GetListAsync_Should_Return_SetValue()
        //{
        //    //arrange
        //    IRepository _repository = new Repository(_context);

        //    //act
        //    var result = await _repository.GetListAsync<Group>();

        //    //assert
        //    Assert.True(result.Count() == _groupList.Count());
        //}

        //[Fact]
        //public async Task GetListAsyncWithOptions_ShouldNot_Return_NotNull()
        //{
        //    //arrange
        //    IRepository _repository = new Repository(_context);


        //    //_repoMock.Setup(r => r.GetListAsync<Group>()).Returns(Task.FromResult<IEnumerable<Group>>(_groupList));
        //    //_groupService = new GroupService(_repoMock.Object, _data.Object);
        //    //_data.Setup(d => d.Map<IEnumerable<GroupDto>>(_groupList)).Returns(_groupListDto);

        //    var queryOptions = Substitute.For<IQueryOptions>();
        //    //queryOptions.GetFiltersExpression<Group>().Returns();
        //    //queryOptions.GetFiltersExpression<Group>().Returns<Group>(null);
        //    //queryOptions.GetSortersCollection<Group>().Returns<Group>(null);
        //    //queryOptions.GetIncludersCollection<Group>().Returns<Group>(null);
        //    //queryOptions.Pagenator.Returns<Group>(null);

        //    //act
        //    var result = await _repository.GetListAsync<Group>(queryOptions);

        //    //assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task GetBy_Id_Should_Return_Correct_Object()
        //{
        //    //arrange
        //    IRepository _repository = new Repository(_context);

        //    var products = await _repository.GetListAsync<Group>();
        //    var testProduct = await _repository.GetByIdAsync<Group>(products.First().Id);

        //    Assert.NotNull(testProduct);
        //}

        //[Fact]
        //public async Task GetBy_WrongId_Should_ReturnNull()
        //{
        //    //arrange
        //    IRepository _repository = new Repository(_context);

        //    int badId = -1;
        //    var testProduct = await _repository.GetByIdAsync<Group>(badId);

        //    Assert.Null(testProduct);
        //}

        //[Fact]
        //public async Task Add_Should_Return_Correct_Object()
        //{
        //    //arrange
        //    IRepository _repository = new Repository(_context);

        //    var testPerson = await _repository.CreateAsync(_group);
        //    await _repository.DeleteAsync(testPerson);

        //    Assert.True(_group.CreateDate == testPerson.CreateDate);
        //}

        ////[Fact]
        ////public async Task Remove()
        ////{
        ////    //arrange
        ////    IRepository _repository = new Repository(_context);

        ////    var tempPerson = await _repository.CreateAsync(_group);
        ////    await _repository.DeleteAsync(tempPerson);
        ////    var testPerson = await _repository.GetByIdAsync<Group>(tempPerson.Id);

        ////    Assert.Null(testPerson);
        ////}

        //[Fact]
        //public async Task AddAsync_WasExecute()
        //{
        //    //arrange
        //    IRepository _repository = new Repository(_context);
        //    Group group = new Group { Id = 1 };

        //    //_repoMock.Setup(r => r.CreateAsync<Group>(null));
        //    //_data.Setup(d => d.Map<Group>(null)).Returns(group);
        //    //_groupService = new GroupService(_repoMock.Object, _data.Object);

        //    //act
        //    await _repository.CreateAsync<Group>(null);

        //    //assert
        //    //_context.Verify(mock => mock.CreateAsync(group), Times.Once());
        //    //_context.DidNotReceive()..Execute();
        //}

        //[Fact]
        //public async Task UpdateAsync_WasExecute()
        //{
        //    //arrange
        //    IRepository _repository = new Repository(_context);
        //    Group group = new Group { Id = 1 };

        //    //_repoMock.Setup(r => r.GetByIdAsync<Group>(group.Id)).Returns(Task.FromResult(group));
        //    //_repoMock.Setup(r => r.UpdateAsync(group)).Returns(Task.FromResult(group));
        //    //_groupService = new GroupService(_repoMock.Object, _data.Object);

        //    //act
        //    await _repository.UpdateAsync(group);

        //    //assert
        //    //_context.Verify(mock => mock.UpdateAsync(group), Times.Once());
        //}

        //[Fact]
        //public async Task DeleteAsync_WasExecute()
        //{
        //    //arrange
        //    IRepository _repository = new Repository(_context);
        //    Group group = new Group { Id = 1 };

        //    //_repoMock.Setup(r => r.DeleteAsync<Group>(null));
        //    //_data.Setup(d => d.Map<Group>(null)).Returns(group);
        //    //_groupService = new GroupService(_repoMock.Object, _data.Object);

        //    //act
        //    await _repository.DeleteAsync(group);

        //    //assert
        //    //_context.Verify(mock => mock.DeleteAsync(group), Times.Once());
        //}
    }
}
