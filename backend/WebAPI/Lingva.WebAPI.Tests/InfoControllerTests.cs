using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.Common.Mapping;
using Lingva.WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Tests
{
    [ExcludeFromCodeCoverage]
    public class InfoControllerTests
    {
        private List<LanguageDto> _languageDtoList;
        private InfoController _infoController;
        private Mock<IInfoService> _infoService;
        private Mock<IDataAdapter> _dataAdapter;
        private Mock<ILogger<InfoController>> _logger;

        [SetUp]
        public void Setup()
        {
            _infoService= new Mock<IInfoService>();
            _dataAdapter = new Mock<IDataAdapter>();          
            _logger = new Mock<ILogger<InfoController>>();

            _languageDtoList = new List<LanguageDto>
            {
                new LanguageDto
                {
                    Id = 1,
                    Name = "en",
                },
                new LanguageDto
                {
                    Id = 2,
                    Name = "ru",
                }
            };
        }

        [Test]
        public async Task Index_Get_NotNull()
        {
            //arrange
            _infoService.Setup(r => r.GetLanguagesListAsync()).Returns(Task.FromResult<IEnumerable<LanguageDto>>(_languageDtoList));
            _infoController = new InfoController(_infoService.Object, _dataAdapter.Object, _logger.Object);

            //act
            var result = await _infoController.GetLanguagesList();

            //assert
            Assert.NotNull(result);
        }
    }
}
