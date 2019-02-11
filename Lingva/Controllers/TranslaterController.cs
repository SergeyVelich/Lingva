using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lingva.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lingva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslaterController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IOptions<StorageOptions> _storageOptions;

        public TranslaterController(DBContext context, IOptions<StorageOptions> storageOptions)
        {
            _context = context;
            _storageOptions = storageOptions;
        }

        // GET: api/Translater/paper/en/ru
        [HttpGet("{text}/{originalLanguageStr}/{translationLanguageStr}")]
        public async Task<IActionResult> GetTranslation([FromRoute] string text, [FromRoute] string originalLanguageStr, [FromRoute] string translationLanguageStr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ITranslater translater = new TranslaterYandex(_storageOptions.Value.ServicesYandexKey);
            string strTranslation;

            Language originalLanguage = await _context.Languages.FindAsync(originalLanguageStr);
            if (originalLanguage == null)
            {
                return NotFound();
            }

            Language translationLanguage = await _context.Languages.FindAsync(translationLanguageStr);
            if (translationLanguage == null)
            {
                return NotFound();
            }

            try
            {
                strTranslation = await Task.Run(() => translater.Translate(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return Ok(text);
            }
        }

    }
}