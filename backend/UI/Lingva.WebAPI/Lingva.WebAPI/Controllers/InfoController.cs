using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IInfoManager _infoManager;
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<InfoController> _logger;

        public InfoController(IInfoManager infoManager, IDataAdapter dataAdapter, ILogger<InfoController> logger)
        {
            _infoManager = infoManager;
            _dataAdapter = dataAdapter;
            _logger = logger;
        }

        // GET: api/info/languages
        [HttpGet("languages")]
        public async Task<IActionResult> GetLanguagesList()
        {
            IEnumerable<LanguageDto> languages = await _infoManager.GetLanguagesListAsync();

            return Ok(_dataAdapter.Map<IEnumerable<LanguageViewModel>>(languages));
        }
    }
}