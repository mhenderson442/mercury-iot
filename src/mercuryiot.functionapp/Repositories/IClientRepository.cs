using System.Threading.Tasks;
using Mercuryiot.Functions.Models;

namespace Mercuryiot.Functions.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetClient(string customerKey);

        Task<bool> InsertClient(Client client);
    }
}