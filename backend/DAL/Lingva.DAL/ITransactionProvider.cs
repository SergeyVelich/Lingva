namespace Lingva.DAL
{
    public interface ITransactionProvider
    {
        void StartTransaction();
        void CommitTransaction();
        void AbortTransaction();
        void EndTransaction();
    }
}
