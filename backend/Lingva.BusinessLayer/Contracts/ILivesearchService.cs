using System.Collections;

namespace Lingva.BC.Contracts
{
    public interface ILivesearchService
    {
        IEnumerable Find(string substring, int quantity);
    }
}
