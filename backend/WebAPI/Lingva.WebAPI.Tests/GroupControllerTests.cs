using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.Common.Mapping;
using Lingva.WebAPI.Controllers;
using Lingva.WebAPI.Infrastructure;
using Lingva.WebAPI.Models.Request;
using Lingva.WebAPI.Models.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Tests
{
    [ExcludeFromCodeCoverage]
    public class GroupControllerTests
    {
        private List<GroupDto> _groupDtoList;
        private GroupController _groupController;
        private Mock<IGroupService> _groupService;
        private Mock<IDataAdapter> _dataAdapter;
        private Mock<ILogger<GroupController>> _logger;
        private Mock<QueryOptionsAdapter> _queryOptionsAdapter;
        private Mock<OptionsModel> _optionsModel;

        [SetUp]
        public void Setup()
        {
            _groupService= new Mock<IGroupService>();
            _dataAdapter = new Mock<IDataAdapter>();          
            _logger = new Mock<ILogger<GroupController>>();
            _queryOptionsAdapter = new Mock<QueryOptionsAdapter>();
            _optionsModel = new Mock<OptionsModel>();

            _groupDtoList = new List<GroupDto>
            {
                new GroupDto
                {
                    Id = 1,
                    Name = "Harry Potter",
                    Date = DateTime.Now,
                    Description = "Description",
                    Picture = "Picture",
                    LanguageId = 1
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
        }

        [Test]
        public async Task Index_Get_NotNull()
        {
            //arrange
            _groupService.Setup(r => r.GetListAsync()).Returns(Task.FromResult<IEnumerable<GroupDto>>(_groupDtoList));
            _groupController = new GroupController(_groupService.Object, _dataAdapter.Object, _logger.Object, _queryOptionsAdapter.Object);

            //act
            var result = await _groupController.Index(_optionsModel.Object);

            //assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task Get_Get_NotNull()
        {
            //arrange
            int validId = 1;
            GroupViewModel groupViewModel = new GroupViewModel { Id = validId };
            GroupDto groupDto = new GroupDto { Id = validId };

            _dataAdapter.Setup(d => d.Map<GroupDto>(groupViewModel)).Returns(groupDto);
            _groupService.Setup(r => r.GetByIdAsync(validId)).Returns(Task.FromResult(groupDto));
            _groupController = new GroupController(_groupService.Object, _dataAdapter.Object, _logger.Object, _queryOptionsAdapter.Object);

            //act
            var result = await _groupController.Get(1);

            //assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task Create_Post_NotNull()
        {
            //arrange
            GroupViewModel groupViewModel = new GroupViewModel { Id = 1 };
            GroupCreateViewModel groupCreateViewModel = new GroupCreateViewModel { Id = 1 };
            GroupDto groupDto = new GroupDto { Id = 1 };           

            _groupService.Setup(r => r.AddAsync(It.IsAny<GroupDto>())).Returns(Task.FromResult(groupDto));
            _dataAdapter.Setup(d => d.Map<GroupDto>(groupCreateViewModel)).Returns(groupDto);
            _dataAdapter.Setup(d => d.Map<GroupViewModel>(groupDto)).Returns(groupViewModel);
            _groupController = new GroupController(_groupService.Object, _dataAdapter.Object, _logger.Object, _queryOptionsAdapter.Object);

            //act
            var result = await _groupController.Create(groupCreateViewModel);

            //assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task Update_Post_NotNull()
        {
            //arrange
            GroupViewModel groupViewModel = new GroupViewModel { Id = 1 };
            GroupCreateViewModel groupCreateViewModel = new GroupCreateViewModel { Id = 1 };
            GroupDto groupDto = new GroupDto { Id = 1 };

            _groupService.Setup(r => r.UpdateAsync(It.IsAny<GroupDto>())).Returns(Task.FromResult(groupDto));
            _dataAdapter.Setup(d => d.Map<GroupDto>(groupCreateViewModel)).Returns(groupDto);
            _dataAdapter.Setup(d => d.Map<GroupViewModel>(groupDto)).Returns(groupViewModel);
            _groupController = new GroupController(_groupService.Object, _dataAdapter.Object, _logger.Object, _queryOptionsAdapter.Object);

            //act
            var result = await _groupController.Create(groupCreateViewModel);

            //assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task Delete_NotNull()
        {
            //arrange
            GroupViewModel groupViewModel = new GroupViewModel { Id = 1 };
            GroupCreateViewModel groupCreateViewModel = new GroupCreateViewModel { Id = 1 };
            GroupDto groupDto = new GroupDto { Id = 1 };

            _groupService.Setup(r => r.DeleteAsync(It.IsAny<GroupDto>())).Returns(Task.FromResult<GroupDto>(groupDto));
            _dataAdapter.Setup(d => d.Map<GroupDto>(groupCreateViewModel)).Returns(groupDto);
            _dataAdapter.Setup(d => d.Map<GroupViewModel>(groupDto)).Returns(groupViewModel);
            _groupController = new GroupController(_groupService.Object, _dataAdapter.Object, _logger.Object, _queryOptionsAdapter.Object);

            //act
            var result = await _groupController.Delete(groupCreateViewModel);

            //assert
            Assert.NotNull(result);
        }
    }
}
