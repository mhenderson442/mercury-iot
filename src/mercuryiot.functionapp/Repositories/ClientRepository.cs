using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Client> GetClient(string id, string region)
        {
            var client = await _clientContext.Clients.FirstOrDefaultAsync(x => x.id == id && x.Region == region);
            return client;
        }

        public async Task<List<Client>> GetClients(string region)
        {
            var clients = await _clientContext.Clients.Where(x => x.Region == region).ToListAsync();
            return clients;
        }

        public async Task<bool> InsertClient(Client client)
        {
            _ = await _clientContext.AddAsync(client);
            return await _clientContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateClient(Client client)
        {
            _ = _clientContext.Update(client);
            return await _clientContext.SaveChangesAsync() == 1;
        }
    }
}