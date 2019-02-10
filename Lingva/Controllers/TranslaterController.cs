using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lingva.Model;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslaterController : ControllerBase
    {
        private readonly DBContext _context;

        public TranslaterController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Translater/paper/en/ru
        [HttpGet("{text}/{originalLanguageStr}/{translationLanguageStr}")]
        public async Task<IActionResult> GetTranslation([FromRoute] string text, [FromRoute] string originalLanguageStr, [FromRoute] string translationLanguageStr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ITranslater translater = new TranslaterYandex();
            string strTranslation;

            var originalLanguage = await _context.Languages.FindAsync(originalLanguageStr);
            if (originalLanguage == null)
            {
                return NotFound();
            }

            var translationLanguage = await _context.Languages.FindAsync(translationLanguageStr);
            if (translationLanguage == null)
            {
                return NotFound();
            }

            try
            {
                strTranslation = translater.Translate(text, originalLanguage, translationLanguage);
                return Ok(strTranslation);
            }
            catch
            {
                return Ok(text);
            }
        }

    }
}