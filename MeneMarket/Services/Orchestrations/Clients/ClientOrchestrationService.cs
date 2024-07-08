using MeneMarket.Models.Foundations.Clients;
using MeneMarket.Models.Foundations.Products;
using MeneMarket.Services.Foundations.Clients;
using MeneMarket.Services.Foundations.OfferLinks;
using MeneMarket.Services.Foundations.Products;

namespace MeneMarket.Services.Orchestrations.Clients
{
    public class ClientOrchestrationService : IClientOrchestrationService
    {
        private readonly IClientService clientService;
        private readonly IOfferLinkService offerLinkService;
        private readonly IProductService productService;

        public ClientOrchestrationService(
            IClientService clientService,
            IProductService productService,
        IOfferLinkService offerLinkService)
        {
            this.clientService = clientService;
            this.productService = productService;
            this.offerLinkService = offerLinkService;
        }

        public async ValueTask<Product> AddClientAsync(Guid id, string ipAddress)
        {
            IQueryable<Client> allClients =
                this.clientService.RetrieveAllClients();

            var selectedOfferLink =
                 await this.offerLinkService.RetrieveOfferLinkByIdAsync(id);

            var existedClient =
                allClients.FirstOrDefault(c =>
                c.IpAddress == ipAddress && c.ProductId == selectedOfferLink.ProductId);

            if (existedClient == null)
            {
                var client = new Client
                {
                    ClientId = Guid.NewGuid(),
                    IpAddress = ipAddress,
                    StatusType = StatusType.Visit,
                    OfferLinkId = id,
                    ProductId = selectedOfferLink.ProductId
                };
                await clientService.AddClientAsync(client);
            }

            var product = await this.productService.RetrieveProductByIdAsync(selectedOfferLink.ProductId);

            return product;
        }

        public IQueryable<Client> RetrieveAllClients() =>
            this.clientService.RetrieveAllClients();

        public async ValueTask<Client> RetrieveClientByIdAsync(Guid id) =>
            await this.clientService.RetrieveClientByIdAsync(id);

        public async ValueTask<Client> ModifyClientAsync(Client client) =>
            await this.clientService.ModifyClientAsync(client);

        public async ValueTask<Client> RemoveClientByIdAsync(Guid id) =>
            await this.clientService.RemoveClientAsync(id);
    }
}