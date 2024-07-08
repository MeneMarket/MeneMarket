using MeneMarket.Models.Foundations.Clients;
using MeneMarket.Models.Foundations.Products;

namespace MeneMarket.Services.Orchestrations.Clients
{
    public interface IClientOrchestrationService
    {
        ValueTask<Product> AddClientAsync(Guid id, string ipAddress);
        IQueryable<Client> RetrieveAllClients();
        ValueTask<Client> RetrieveClientByIdAsync(Guid id);
        ValueTask<Client> ModifyClientAsync(Client client);
        ValueTask<Client> RemoveClientByIdAsync(Guid id);
    }
}