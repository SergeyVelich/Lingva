using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.BC.Services;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Lingva.BC.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class GroupServiceTests
    {
        private List<Group> _groupList;
        private IGroupService _groupService;
        private Mock<IRepository> _repoMock;
        private Mock<IDataAdapter> _data;

        [SetUp]
        public void Setup()
        {
            _groupList = new List<Group>
            {
                  new Group
                {
                    Id = 1,
                    Person = new Person
                    {
                        Id = 1,
                        FirstName = "Nick",
                        LastName = "Some",
                        Email = "nick@gmail.com",
                        Phone = "123"
                    },
                    Product = new Product
                    {
                        Id = 4,
                        Name = "Test Name",
                        Description = "some text",
                        Price = 1000.6,
                    }
                }
            };

            _repoMock = new Mock<IRepository>();
            _data = new Mock<IDataAdapter>();
            _groupService = new GroupService(_repoMock.Object, _data.Object);
        }

        [Test]
        public async Task GetAllOrders_Should_Return_SetValue()
        {
            //arrange
            _repoMock.Setup(r => r.GetListAsync()).Returns(Task.FromResult<IList<Group>>(_groupList));
            _groupService = new GroupService(_repoMock.Object, _data.Object);

            //act
            var orderList = await _groupService.GetListAsync();

            //assert
            Assert.True(orderList.Count == _groupList.Count);
        }

        [Test]
        public async Task GetAllOrders_ShouldNot_Return_Null()
        {
            //arrange
            _orderRepoMock.Setup(r => r.GetAllOrders()).Returns(Task.FromResult<IList<Group>>(_groupList));
            _groupService = new GroupService(_repoMock.Object, _data.Object, _orderRepoMock.Object);

            //act
            var orderList = await _groupService.GetAllOrders();

            //assert
            Assert.NotNull(orderList);
        }

        [Test]
        public async Task GetOrderByValidId_ShouldNot_Return_Null()
        {
            //arrange
            int validId = 1;
            var orderItem = new Group { Id = 1 };
            var orderDto = new GroupDTO { Id = 1 };

            _data.Setup(d => d.Map<GroupDTO>(orderItem)).Returns(orderDto);
            _orderRepoMock.Setup(r => r.GetOrderById(validId)).Returns(Task.FromResult(orderItem));
            _groupService = new GroupService(_repoMock.Object, _data.Object);

            //act
            var order = await _groupService.GetOrderById(validId);

            //assert
            Assert.NotNull(order);
        }

        [Test]
        public async Task GetOrderByInvalidId_Should_Return_Null()
        {
            //arrange
            int invalidId = -1;

            _orderRepoMock.Setup(r => r.GetOrderById(invalidId)).Returns(Task.FromResult<Group>(null));
            _groupService = new GroupService(_repoMock.Object, _data.Object, _orderRepoMock.Object);

            //act
            var order = await _groupService.GetOrderById(invalidId);

            //assert
            Assert.Null(order);
        }

        [Test]
        public async Task RemoveOrder_WasExecute()
        {
            //arrange
            var orderItem = new Group { Id = 1 };

            _orderRepoMock.Setup(r => r.RemoveOrder(null));
            _data.Setup(d => d.Map<Group>(null)).Returns(orderItem);
            _groupService = new GroupService(_repoMock.Object, _data.Object);

            //act
            await _groupService.RemoveOrder(null);

            //assert
            _orderRepoMock.Verify(mock => mock.RemoveOrder(orderItem), Times.Once());
        }

        [Test]
        public async Task AddOrder_WasExecute()
        {
            //arrange
            var orderItem = new Group { Id = 1 };

            _orderRepoMock.Setup(r => r.AddOrder(null));
            _data.Setup(d => d.Map<Group>(null)).Returns(orderItem);
            _groupService = new GroupService(_repoMock.Object, _data.Object, _orderRepoMock.Object);

            //act
            await _groupService.AddOrder(null);

            //assert
            _orderRepoMock.Verify(mock => mock.AddOrder(orderItem), Times.Once());
        }
    }
}
