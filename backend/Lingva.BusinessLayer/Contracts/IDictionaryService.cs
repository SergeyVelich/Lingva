using Lingva.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace Lingva.BusinessLayer.Contracts
{
    public interface IDictionaryService
    {
        IEnumerable<DictionaryRecord> GetDictionary();

        DictionaryRecord GetDictionaryRecord(int id);

        void AddDictionaryRecord(DictionaryRecord dictionaryRecord);

        void UpdateDictionaryRecord(int id, DictionaryRecord dictionaryRecord);

        void DeleteDictionaryRecord(int id);
    }
}
