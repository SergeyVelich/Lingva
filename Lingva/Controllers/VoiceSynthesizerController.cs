using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceSynthesizerController : ControllerBase
    {
        private readonly DBContext _context;

        public VoiceSynthesizerController(DBContext context)
        {
            _context = context;
        }

        // GET: api/voiceSynthesizer/paper
        //[HttpGet("{text}")]
        //public async Task<IActionResult> GetVoice([FromRoute] string text)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IVoiceSynthesizer voiceSynthesizer = new VoiceSynthesizerNET();

        //    //try
        //    //{
        //        voiceSynthesizer.Synthesize(text);
        //        return Ok(text);
        //    //}
        //    //catch
        //    //{
        //    //    return NotFound();
        //    //}
        //}

        [HttpGet()]
        public async Task<IActionResult> GetVoice()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IVoiceSynthesizer voiceSynthesizer = new VoiceSynthesizerGoogle();

            //try
            //{
            voiceSynthesizer.Synthesize("text");
            return Ok("text");
            //}
            //catch
            //{
            //    return NotFound();
            //}
        }
    }
}