using System.Threading.Tasks;
using Mercuryiot.Functions.DataAccess;
using Mercuryiot.Functions.Models;
using Microsoft.EntityFrameworkCore;

namespace Mercuryiot.Functions.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _clientContext;

        public ClientRepository(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }

        public async Task<Client> GetClient(string customerKey)
        {
            var client = await _clientContext.Clients.FirstOrDefaultAsync(x => x.Key == customerKey);
            return client;
        }

        public async Task<bool> InsertClient(Client client)
        {
            var entityEntry = await _clientContext.AddAsync<Client>(client);

            if (entityEntry.Entity is Client)
            {
                await _clientContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}