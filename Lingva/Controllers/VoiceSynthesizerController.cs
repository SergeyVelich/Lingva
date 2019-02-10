using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lingva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceSynthesizerController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IOptions<StorageOptions> _storageOptions;

        public VoiceSynthesizerController(DBContext context, IOptions<StorageOptions> storageOptions)
        {
            _context = context;
            _storageOptions = storageOptions;
        }

        // GET: api/voiceSynthesizer/paper
        [HttpGet("{text}")]
        public async Task<IActionResult> SreakText([FromRoute] string text)
        {
            text = "Android is a mobile operating system developed by Google, " +
                   "based on the Linux kernel and designed primarily " +
                   "for touchscreen mobile devices such as smartphones and tablets";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IVoiceSynthesizer voiceSynthesizer = new VoiceSynthesizerGoogle(_storageOptions.Value.ServicesYandexKey);

            try
            {
                await Task.Run(() => voiceSynthesizer.Synthesize(text));
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}