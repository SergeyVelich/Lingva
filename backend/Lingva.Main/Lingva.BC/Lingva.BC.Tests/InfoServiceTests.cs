using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.BC.Services;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Lingva.BC.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class InfoManagerTests
    {
        private readonly List<Language> _languageList;
        private readonly List<LanguageDto> _languageListDto;
        private IInfoManager _infoManager;
        private readonly Mock<IGroupRepository> _repoMock;
        private readonly Mock<IDataAdapter> _data;

        public InfoManagerTests()
        {
            _languageList = new List<Language>
            {
                new Language
                {
                    Id = 1,
                    Name = "en",
                },
                new Language
                {
                    Id = 2,
                    Name = "ru",
                },
            };

            _languageListDto = new List<LanguageDto>
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

            _repoMock = new Mock<IGroupRepository>();
            _data = new Mock<IDataAdapter>();
            _infoManager = new InfoManager(_repoMock.Object, _data.Object);
        }

        [Fact]
        public async Task GetLanguagesListAsync_ShouldNot_Return_NotNull()
        {
            //arrange
            _repoMock.Setup(r => r.GetListAsync<Language>()).Returns(Task.FromResult<IEnumerable<Language>>(_languageList));
            _infoManager = new InfoManager(_repoMock.Object, _data.Object);

            //act
            var result = await _infoManager.GetLanguagesListAsync();

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetLanguagesListAsync_Should_Return_SetValue()
        {
            //arrange
            _repoMock.Setup(r => r.GetListAsync<Language>()).Returns(Task.FromResult<IEnumerable<Language>>(_languageList));
            _infoManager = new InfoManager(_repoMock.Object, _data.Object);
            _data.Setup(d => d.Map<IEnumerable<LanguageDto>>(_languageList)).Returns(_languageListDto);

            //act
            var result = await _infoManager.GetLanguagesListAsync();

            //assert
            Assert.True(result.Count() == _languageList.Count());
        }
    }
}
