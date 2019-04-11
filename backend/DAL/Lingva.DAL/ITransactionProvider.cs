using System.Data;

namespace Lingva.DAL
{
    public interface ITransactionProvider
    {
        IDbTransaction BeginTransaction();

        void EndTransaction();
    }
}
