using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lingva.Model;

namespace Lingva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private readonly DBContext _context;

        public VocabularyController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Vocabulary
        [HttpGet]
        public IEnumerable<VocabularyRecord> GetVocabulary()
        {
            return _context.Vocabulary;
        }

        // GET: api/Vocabulary/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVocabularyRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var translation = await _context.Vocabulary.FindAsync(id);

            if (translation == null)
            {
                return NotFound();
            }

            return Ok(translation);
        }

        // PUT: api/Vocabulary/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVocabularyRecord([FromRoute] int id, [FromBody] VocabularyRecord vocabularyRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vocabularyRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(vocabularyRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VocabularyRecordExists(id))
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

        // POST: api/Vocabulary
        [HttpPost]
        public async Task<IActionResult> PostVocabularyRecord([FromBody] VocabularyRecord vocabularyRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vocabulary.Add(vocabularyRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTranslation", new { id = vocabularyRecord.Id }, vocabularyRecord);
        }

        // DELETE: api/Vocabulary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVocabularyRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vocabularyRecord = await _context.Vocabulary.FindAsync(id);
            if (vocabularyRecord == null)
            {
                return NotFound();
            }

            _context.Vocabulary.Remove(vocabularyRecord);
            await _context.SaveChangesAsync();

            return Ok(vocabularyRecord);
        }

        private bool VocabularyRecordExists(int id)
        {
            return _context.Vocabulary.Any(e => e.Id == id);
        }
    }
}