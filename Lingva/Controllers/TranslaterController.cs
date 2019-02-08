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
        // GET: api/Translater/paper/1/0
        [HttpGet("{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslation([FromRoute] string text, [FromRoute] int originalLanguage, [FromRoute] int translationLanguage)
        {
            ITranslater translater = new TranslaterYandex();
            string strTranslation;

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