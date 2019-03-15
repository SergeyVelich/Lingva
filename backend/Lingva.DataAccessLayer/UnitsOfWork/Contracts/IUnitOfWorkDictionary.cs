using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.UnitsOfWork.Contracts
{
    public interface IUnitOfWorkDictionary : IUnitOfWork
    {
        IRepositoryDictionaryRecord DictionaryRecords { get; }
        IRepositoryWord Words { get; }
    }
}
