using System.Threading.Tasks;

namespace SenderService.Email.Contracts
{
    public interface ITemplateSource
    {
        Task<string> GetAsync(int id);
    }
}
