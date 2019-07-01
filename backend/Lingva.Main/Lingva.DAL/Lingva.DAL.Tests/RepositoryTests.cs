using Lingva.DAL.EF.Context;
using Lingva.DAL.EF.Repositories;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Tests
{
    [ExcludeFromCodeCoverage]
    public class RepositoryTests
    {
        private IGroupRepository _groupRepository;
        private List<Group> _groupList;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DictionaryContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Lingva;Trusted_Connection=True;MultipleActiveResultSets=true;");
            var _dbContext = new DictionaryContext(optionsBuilder.Options);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _groupList = new List<Group>()
            {
                new Group
                {
                    Name = "Harry Potter",
                    Date = DateTime.Now,
                    Description = "Description",
                    LanguageId = 1,
                },
                new Group
                {
                    Name = "Harry Potter",
                    Date = DateTime.Now,
                    Description = "Description",
                    LanguageId = 1,
                }
            };
            _dbContext.Set<Group>().AddRange(_groupList);
            _dbContext.SaveChanges();

            _groupRepository = new GroupRepository(_dbContext);
        }

        [Test]
        public async Task GetListAsync_ShouldNot_ReturnNull()
        {
            var orders = await _groupRepository.GetListAsync<Group>();

            Assert.NotNull(orders);
        }

        [Test]
        public async Task GetByIdAsync_ValidId_ReturnGroup()
        {
            var groups = await _groupRepository.GetListAsync<Group>();
            int validId = groups.First().Id;
            var result = await _groupRepository.GetByIdAsync<Group>(validId);

            Assert.NotNull(result);
        }

        [Test]
        public async Task GetByIdAsync_InvalidId_ReturnNull()
        {
            int invalidId = -1;
            var result = await _groupRepository.GetByIdAsync<Group>(invalidId);

            Assert.Null(result);
        }

        [Test]
        public async Task AddAsync_Group_ReturnGroup()
        {
            var result = await _groupRepository.CreateAsync(_groupList[0]);

            Assert.True(result.Id != 0);
        }

        [Test]
        public async Task UpdateAsync_Group_ReturnGroup()
        {
            var groupFromDb = await _groupRepository.GetByIdAsync<Group>(1);
            int languageId = groupFromDb.LanguageId;
            groupFromDb.LanguageId ++;
            var result = await _groupRepository.UpdateAsync(groupFromDb);

            Assert.True(result.LanguageId != languageId);
        }

        [Test]
        public async Task DeleteAsync_Group_ReturnGroup()
        {
            var groupFromDb = await _groupRepository.GetByIdAsync<Group>(2);
            await _groupRepository.DeleteAsync<Group>(groupFromDb.Id);
            var result = await _groupRepository.GetByIdAsync<Group>(groupFromDb.Id);

            Assert.Null(result);
        }

        //[Fact]
        //public async Task GetListAsync_ShouldNot_Return_NotNull()
        //{
        //    //arrange
        //    IGroupRepository _repository = new Repository(_context);

        //    //act
        //    var result = await _repository.GetListAsync<Group>();

        //    //assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task GetListAsync_Should_Return_SetValue()
        //{
        //    //arrange
        //    IGroupRepository _repository = new Repository(_context);

        //    //act
        //    var result = await _repository.GetListAsync<Group>();

        //    //assert
        //    Assert.True(result.Count() == _groupList.Count());
        //}

        //[Fact]
        //public async Task GetListAsyncWithOptions_ShouldNot_Return_NotNull()
        //{
        //    //arrange
        //    IGroupRepository _repository = new Repository(_context);


        //    //_repoMock.Setup(r => r.GetListAsync<Group>()).Returns(Task.FromResult<IEnumerable<Group>>(_groupList));
        //    //_groupManager = new GroupService(_repoMock.Object, _data.Object);
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
        //    IGroupRepository _repository = new Repository(_context);

        //    var products = await _repository.GetListAsync<Group>();
        //    var testProduct = await _repository.GetByIdAsync<Group>(products.First().Id);

        //    Assert.NotNull(testProduct);
        //}

        //[Fact]
        //public async Task GetBy_WrongId_Should_ReturnNull()
        //{
        //    //arrange
        //    IGroupRepository _repository = new Repository(_context);

        //    int badId = -1;
        //    var testProduct = await _repository.GetByIdAsync<Group>(badId);

        //    Assert.Null(testProduct);
        //}

        //[Fact]
        //public async Task Add_Should_Return_Correct_Object()
        //{
        //    //arrange
        //    IGroupRepository _repository = new Repository(_context);

        //    var testPerson = await _repository.CreateAsync(_group);
        //    await _repository.DeleteAsync(testPerson);

        //    Assert.True(_group.CreateDate == testPerson.CreateDate);
        //}

        ////[Fact]
        ////public async Task Remove()
        ////{
        ////    //arrange
        ////    IGroupRepository _repository = new Repository(_context);

        ////    var tempPerson = await _repository.CreateAsync(_group);
        ////    await _repository.DeleteAsync(tempPerson);
        ////    var testPerson = await _repository.GetByIdAsync<Group>(tempPerson.Id);

        ////    Assert.Null(testPerson);
        ////}

        //[Fact]
        //public async Task AddAsync_WasExecute()
        //{
        //    //arrange
        //    IGroupRepository _repository = new Repository(_context);
        //    Group group = new Group { Id = 1 };

        //    //_repoMock.Setup(r => r.CreateAsync<Group>(null));
        //    //_data.Setup(d => d.Map<Group>(null)).Returns(group);
        //    //_groupManager = new GroupService(_repoMock.Object, _data.Object);

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
        //    IGroupRepository _repository = new Repository(_context);
        //    Group group = new Group { Id = 1 };

        //    //_repoMock.Setup(r => r.GetByIdAsync<Group>(group.Id)).Returns(Task.FromResult(group));
        //    //_repoMock.Setup(r => r.UpdateAsync(group)).Returns(Task.FromResult(group));
        //    //_groupManager = new GroupService(_repoMock.Object, _data.Object);

        //    //act
        //    await _repository.UpdateAsync(group);

        //    //assert
        //    //_context.Verify(mock => mock.UpdateAsync(group), Times.Once());
        //}

        //[Fact]
        //public async Task DeleteAsync_WasExecute()
        //{
        //    //arrange
        //    IGroupRepository _repository = new Repository(_context);
        //    Group group = new Group { Id = 1 };

        //    //_repoMock.Setup(r => r.DeleteAsync<Group>(null));
        //    //_data.Setup(d => d.Map<Group>(null)).Returns(group);
        //    //_groupManager = new GroupService(_repoMock.Object, _data.Object);

        //    //act
        //    await _repository.DeleteAsync(group);

        //    //assert
        //    //_context.Verify(mock => mock.DeleteAsync(group), Times.Once());
        //}
    }
}
