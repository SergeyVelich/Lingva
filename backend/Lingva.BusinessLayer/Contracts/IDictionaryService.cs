using Lingva.DAL.Entities;
using System.Collections.Generic;

namespace Lingva.BC.Contracts
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
