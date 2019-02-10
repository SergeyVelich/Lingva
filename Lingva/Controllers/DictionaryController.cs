using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lingva.Model;

namespace Lingva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly DBContext _context;

        public DictionaryController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Dictionary
        [HttpGet]
        public IEnumerable<DictionaryRecord> GetDictionary()
        {
            return _context.Dictionary;
        }

        // GET: api/Dictionary/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDictionaryRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var translation = await _context.Dictionary.FindAsync(id);

            if (translation == null)
            {
                return NotFound();
            }

            return Ok(translation);
        }

        // PUT: api/Dictionary/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDictionaryRecord([FromRoute] int id, [FromBody] DictionaryRecord dictionaryRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dictionaryRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(dictionaryRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DictionaryRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dictionary
        [HttpPost]
        public async Task<IActionResult> PostDictionaryRecord([FromBody] DictionaryRecord dictionaryRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dictionary.Add(dictionaryRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTranslation", new { id = dictionaryRecord.Id }, dictionaryRecord);
        }

        // DELETE: api/Dictionary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDictionaryRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dictionaryRecord = await _context.Dictionary.FindAsync(id);
            if (dictionaryRecord == null)
            {
                return NotFound();
            }

            _context.Dictionary.Remove(dictionaryRecord);
            await _context.SaveChangesAsync();

            return Ok(dictionaryRecord);
        }

        private bool DictionaryRecordExists(int id)
        {
            return _context.Dictionary.Any(e => e.Id == id);
        }
    }
}