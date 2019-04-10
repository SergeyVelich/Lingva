using System.Data;

namespace Lingva.DAL.Dapper
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
        IDbTransaction GetTransaction { get; }
    }
}
