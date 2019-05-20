using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using NSubstitute;
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
        private IInfoService _infoService;
        private IDataAdapter _dataAdapter;
        private ILogger<InfoController> _logger;

        [SetUp]
        public void Setup()
        {
            _infoService= Substitute.For<IInfoService>();
            _dataAdapter = Substitute.For<IDataAdapter>();          
            _logger = Substitute.For<ILogger<InfoController>>();

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
            _infoService.GetLanguagesListAsync().Returns(_languageDtoList);
            _infoController = new InfoController(_infoService, _dataAdapter, _logger);

            //act
            var result = await _infoController.GetLanguagesList();

            //assert
            Assert.NotNull(result);
        }
    }
}
