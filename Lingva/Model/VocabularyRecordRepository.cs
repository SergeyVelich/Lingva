//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Lingva.Model
//{
//    public class VocabularyRecordRepository : IRepository<VocabularyRecord>
//    {
//        private readonly DBContext _context;

//        public VocabularyRecordRepository(DBContext context)
//        {
//            _context = context;
//        }

//        // Получить таблицу
//        public IQueryable<VocabularyRecord> GetList()
//        {
//            return _context.Vocabulary;
//        }

//        // Получить элемент таблицы
//        public VocabularyRecord Get(int id)
//        {
//            return _context.Vocabulary.Find(id);
//        }

//        public Task<VocabularyRecord> GetAsync(int id)
//        {
//            return _context.Vocabulary.FindAsync(id);
//        }

//        // Добавление одного поля в БД
//        public void Create(VocabularyRecord entity)
//        {
//            _context.Vocabulary.Add(entity);
//            _context.SaveChanges();
//        }

//        public Task<int> CreateAsync(VocabularyRecord entity)
//        {
//            _context.Vocabulary.Add(entity);
//            return _context.SaveChangesAsync();
//        }

//        // Изменение одного поля в БД
//        public void Update(VocabularyRecord entity)
//        {
//            _context.Entry(entity).State = EntityState.Modified;
//            _context.SaveChanges();
//        }

//        public Task<int> UpdateAsync(VocabularyRecord entity)
//        {
//            _context.Entry(entity).State = EntityState.Modified;
//            return _context.SaveChangesAsync();
//        }

//        // Удаление одного поля в БД
//        public bool Delete(int id)
//        {
//            var entity = _context.Vocabulary.Find(id);
//            if (entity == null)
//            {
//                return false;
//            }

//            _context.Vocabulary.Remove(entity);
//            _context.SaveChanges();

//            return true;
//        }

//        public async bool DeleteAsync(int id)
//        {
//            var entity = await _context.Vocabulary.FindAsync(id);
//            if (entity == null)
//            {
//                return false;
//            }

//            _context.Vocabulary.Remove(entity);
//            await _context.SaveChangesAsync();

//            return true;
//        }
//    }
//}
