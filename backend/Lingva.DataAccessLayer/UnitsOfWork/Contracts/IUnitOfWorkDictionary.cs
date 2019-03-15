using Lingva.DataAccessLayer.Repositories.Contracts;

namespace Lingva.DataAccessLayer.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkDictionary : IUnitOfWork
    {
        IRepositoryDictionaryRecord DictionaryRecords { get; }
        IRepositoryWord Words { get; }
    }
}
