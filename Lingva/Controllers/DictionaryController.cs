using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lingva.Model;
using Microsoft.Extensions.Options;

namespace Lingva.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IOptions<StorageOptions> _storageOptions;

        public DictionaryController(DBContext context, IOptions<StorageOptions> storageOptions)
        {
            _context = context;
            _storageOptions = storageOptions;
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
        public async Task<IActionResult> PutDictionaryRecord([FromRoute] int id, [FromBody] Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DictionaryRecord dictionaryRecord = GetDictionaryRecord(id, record);

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
        public async Task<IActionResult> PostDictionaryRecord([FromBody] Record record)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DictionaryRecord dictionaryRecord = CreateDictionaryRecord(record);

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

        public struct Record
        {
            public int Owner { get; set; }
            public string OriginalPhrase { get; set; }
            public string TranslationText { get; set; }
            public string TranslationLanguage { get; set; }
            public string Context { get; set; }
            public string Picture { get; set; }
        }

        private DictionaryRecord CreateDictionaryRecord(Record record)
        {
            DictionaryRecord dictionaryRecord = new DictionaryRecord();

            FillDictionaryRecord(dictionaryRecord, record);

            return dictionaryRecord;
        }

        private DictionaryRecord GetDictionaryRecord(int id, Record record)
        {
            DictionaryRecord dictionaryRecord = _context.Dictionary.Find(id);
            if (dictionaryRecord == null)
            {
                return null;
            }

            FillDictionaryRecord(dictionaryRecord, record);

            return dictionaryRecord;
        }

        private void FillDictionaryRecord(DictionaryRecord dictionaryRecord, Record record)
        {
            User owner = _context.Users
                          .Where(c => c.Id == record.Owner)
                          .FirstOrDefault();

            Phrase phrase = _context.Phrases
                            .Where(c => c.Name == record.OriginalPhrase)
                            .FirstOrDefault();

            Language language = _context.Languages
                                .Where(c => c.Name == record.TranslationLanguage)
                                .FirstOrDefault();

            dictionaryRecord.Owner = owner;
            dictionaryRecord.OriginalPhrase = phrase;
            dictionaryRecord.TranslationText = record.TranslationText;
            dictionaryRecord.TranslationLanguage = language;
            dictionaryRecord.Context = record.Context;
            dictionaryRecord.Picture = record.Picture;
        }
    }
}