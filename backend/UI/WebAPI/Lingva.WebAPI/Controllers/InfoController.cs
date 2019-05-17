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
        private readonly IInfoService _infoService;
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<InfoController> _logger;

        public InfoController(IInfoService infoService, IDataAdapter dataAdapter, ILogger<InfoController> logger)
        {
            _infoService = infoService;
            _dataAdapter = dataAdapter;
            _logger = logger;
        }

        // GET: api/info/languages
        [HttpGet("languages")]
        public async Task<IActionResult> GetLanguagesList()
        {
            IEnumerable<LanguageDto> languages = await _infoService.GetLanguagesListAsync();

            return Ok(_dataAdapter.Map<IEnumerable<LanguageViewModel>>(languages));
        }
    }
}