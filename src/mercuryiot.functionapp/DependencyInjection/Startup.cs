using Mercuryiot.Functions.Repositories;
using Mercuryiot.Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Mercuryiot.Functions.DependencyInjection.Startup))]

namespace Mercuryiot.Functions.DependencyInjection
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            // Add repositories
            builder.Services.AddScoped<IClientRepository, IClientRepository>();

            // Add services
            builder.Services.AddScoped<IClientService, ClientService>();
        }
    }
}