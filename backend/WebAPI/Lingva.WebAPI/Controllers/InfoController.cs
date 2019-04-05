using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.WebAPI.Models.Response.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [Authorize]
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
            IEnumerable<LanguageDTO> languages = await _infoService.GetLanguagesListAsync();

            return Ok(_dataAdapter.Map<IEnumerable<LanguageViewModel>>(languages));
        }
    }
}