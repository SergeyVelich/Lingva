//using Lingva.Additional.Mapping.DataAdapter;
//using Lingva.ASP.Infrastructure.Adapters;
//using Lingva.ASP.Infrastructure.Models;
//using Lingva.BC.Contracts;
//using Lingva.BC.Dto;
//using Lingva.WebAPI.Controllers;
//using Lingva.WebAPI.Models.Entities;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Logging;
//using NSubstitute;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Threading.Tasks;

//namespace Lingva.WebAPI.Tests
//{
//    [ExcludeFromCodeCoverage]
//    public class GroupControllerTests
//    {
//        private List<GroupDto> _groupDtoList;
//        private GroupController _groupController;
//        private IGroupManager _groupManager;
//        private IDataAdapter _dataAdapter;
//        private ILogger<GroupController> _logger;
//        private QueryOptionsAdapter _queryOptionsAdapter;
//        private GroupsListOptionsModel _optionsModel;
//        private IFileStorageManager _fileStorageManager;
//        private IHostingEnvironment _appEnvironment;

//        [SetUp]
//        public void Setup()
//        {
//            _appEnvironment = Substitute.For<IHostingEnvironment>();
//            _groupManager = Substitute.For<IGroupManager>();
//            _fileStorageManager = Substitute.For<IFileStorageManager>();
//            _dataAdapter = Substitute.For<IDataAdapter>();          
//            _logger = Substitute.For<ILogger<GroupController>>();
//            _queryOptionsAdapter = Substitute.For<QueryOptionsAdapter>();
//            _optionsModel = Substitute.For<GroupsListOptionsModel>();

//            _groupDtoList = new List<GroupDto>
//            {
//                new GroupDto
//                {
//                    Id = 1,
//                    Name = "Harry Potter",
//                    Date = DateTime.Now,
//                    Description = "Description",
//                    ImagePath = "Picture",
//                    LanguageId = 1
//                },
//                new GroupDto
//                {
//                    Id = 12,
//                    Name = "Librium",
//                    Date = DateTime.Now,
//                    Description = "Description",
//                    ImagePath = "Picture",
//                    LanguageId = 2
//                }
//            };
//        }

//        [Test]
//        public async Task Index_Get_NotNull()
//        {
//            //arrange
//            _groupManager.GetListAsync().Returns(_groupDtoList);
//            _groupController = new GroupController(_appEnvironment, _groupManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _groupController.Index(_optionsModel);

//            //assert
//            Assert.NotNull(result);
//        }

//        [Test]
//        public async Task Get_Get_NotNull()
//        {
//            //arrange
//            int validId = 1;
//            GroupViewModel groupViewModel = new GroupViewModel { Id = validId };
//            GroupDto groupDto = new GroupDto { Id = validId };

//            _dataAdapter.Map<GroupDto>(groupViewModel).Returns(groupDto);
//            _groupManager.GetByIdAsync(validId).Returns(groupDto);
//            _groupController = new GroupController(_appEnvironment, _groupManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _groupController.Get(1);

//            //assert
//            Assert.NotNull(result);
//        }

//        [Test]
//        public async Task Create_Post_NotNull()
//        {
//            //arrange
//            GroupViewModel groupViewModel = new GroupViewModel { Id = 1 };
//            GroupDto groupDto = new GroupDto { Id = 1 };

//            _groupManager.AddAsync(Arg.Any<GroupDto>()).Returns(Task.FromResult(groupDto));
//            _dataAdapter.Map<GroupDto>(groupViewModel).Returns(groupDto);
//            _dataAdapter.Map<GroupViewModel>(groupDto).Returns(groupViewModel);
//            _groupController = new GroupController(_appEnvironment, _groupManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _groupController.Create(groupViewModel);

//            //assert
//            Assert.NotNull(result);
//        }

//        [Test]
//        public async Task Update_Post_NotNull()
//        {
//            //arrange
//            GroupViewModel groupViewModel = new GroupViewModel { Id = 1 };
//            GroupDto groupDto = new GroupDto { Id = 1 };

//            _groupManager.UpdateAsync(Arg.Any<GroupDto>()).Returns(groupDto);
//            _dataAdapter.Map<GroupDto>(groupViewModel).Returns(groupDto);
//            _dataAdapter.Map<GroupViewModel>(groupDto).Returns(groupViewModel);
//            _groupController = new GroupController(_appEnvironment, _groupManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _groupController.Create(groupViewModel);

//            //assert
//            Assert.NotNull(result);
//        }

//        [Test]
//        public async Task Delete_NotNull()
//        {
//            //arrange
//            GroupViewModel groupViewModel = new GroupViewModel { Id = 1 };
//            GroupDto groupDto = new GroupDto { Id = 1 };

//            _groupManager.DeleteAsync(Arg.Any<int>()).Returns(Task.FromResult(groupDto));
//            _dataAdapter.Map<GroupDto>(groupViewModel).Returns(groupDto);
//            _dataAdapter.Map<GroupViewModel>(groupDto).Returns(groupViewModel);
//            _groupController = new GroupController(_appEnvironment, _groupManager, _fileStorageManager, _dataAdapter, _queryOptionsAdapter, _logger);

//            //act
//            var result = await _groupController.Delete(groupViewModel.Id);

//            //assert
//            Assert.NotNull(result);
//        }
//    }
//}
