using System.Collections.Generic;
using System.Threading.Tasks;
using Mercuryiot.Functions.Models;

namespace Mercuryiot.Functions.Services
{
    public interface IClientService
    {
        Task<Client> GetClient(string id, string region);

        Task<List<Client>> GetClients(string region);

        Task<List<Client>> GetClients();

        Task<Dictionary<string, string>> GetRegions();

        Task<bool> InsertClient(Client client);

        Task<bool> UpdateClient(Client client);
    }
}