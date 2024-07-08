using MeneMarket.Models.Foundations.Clients;
using MeneMarket.Models.Foundations.DonationBoxes;
using MeneMarket.Models.Foundations.ProductRequests;
using MeneMarket.Models.Foundations.ProductRequests.Exception;
using MeneMarket.Models.Orchestrations.RequestsWithCounts;
using MeneMarket.Services.Foundations.Clients;
using MeneMarket.Services.Foundations.ProductRequests;
using MeneMarket.Services.Foundations.Products;
using MeneMarket.Services.Foundations.ProductTypes;
using MeneMarket.Services.Orchestrations.DonationBoxes;

namespace MeneMarket.Services.Orchestrations.ProductRequests
{
    public class ProductRequestOrchestrationService : IProductRequestOrchestrationService
    {
        private readonly IProductRequestService productRequestService;
        private readonly IProductTypeService productTypeService;
        private readonly IClientService clientService;
        private readonly IDonationBoxOrchestrationService donationBoxOrchestrationService;
        private readonly IProductService productService;

        public ProductRequestOrchestrationService(
            IProductRequestService productRequestService,
            IClientService clientService,
            IProductTypeService productTypeService,
            IProductService productService,
        IDonationBoxOrchestrationService donationBoxOrchestrationService)
        {
            this.productRequestService = productRequestService;
            this.clientService = clientService;
            this.donationBoxOrchestrationService = donationBoxOrchestrationService;
            this.productTypeService = productTypeService;
            this.productService = productService;
        }

        public async ValueTask<ProductRequest> AddProductRequestAsync(
            ProductRequest productRequest)
        {
            var productType =
                await this.productTypeService.RetrieveProductTypeByIdAsync(productRequest.ProductTypeId);

            if (productType.Count == 0)
                throw new NullProductTypeCount();


            IQueryable<Client> allClients =
                this.clientService.RetrieveAllClients();

            var client = allClients.FirstOrDefault(c =>
                c.IpAddress == productRequest.IpAddress);

            if (client == null)
            {
                return await this.productRequestService.AddProductRequestAsync(productRequest);
            }
            else
            {
                if (client != null && client.ProductId == productRequest.ProductId)
                {
                    client.StatusType = StatusType.Accepted;
                    await this.clientService.ModifyClientAsync(client);
                }

                productRequest.Status =
                    ProductRequestStatusType.Accepted;

                return await this.productRequestService.AddProductRequestAsync(productRequest);
            }
        }

        public RequestsWithCount RetrieveAllProductRequests(int pageNumber)
        {
            int pageSize = 5;
            int usersToSkip = (pageNumber - 1) * pageSize;
            IQueryable<ProductRequest> allRequests = this.productRequestService.RetrieveAllProductRequests();
            IQueryable<ProductRequest> productReuqests = this.productRequestService.RetrieveAllProductRequests()
                                            .Skip(usersToSkip)
                                            .Take(pageSize);

            return new RequestsWithCount
            {
                AllProductRequests = productReuqests,
                ProductRequestsCunt = (ulong)allRequests.Count()
            };
        }

        public async ValueTask<ProductRequest> RetrieveProductRequestByIdAsync(Guid id) =>
            await this.productRequestService.RetrieveProductRequestByIdAsync(id);

        public async ValueTask<ProductRequest> ModifyProductRequestAsync(
            ProductRequest productRequest)
        {
            bool outbalance = true;

            IQueryable<Client> allClients =
                this.clientService.RetrieveAllClients();

            var client = allClients.FirstOrDefault(c =>
                c.IpAddress == productRequest.IpAddress);

            if (client != null)
            {
                var productType = await this.productTypeService.RetrieveProductTypeByIdAsync(productRequest.ProductTypeId);
                var product = await this.productService.RetrieveProductByIdAsync(productRequest.ProductId);
                var selectedProductRequest = await this.RetrieveProductRequestByIdAsync(productRequest.Id);

                IQueryable<DonationBox> allDonationBoxes =
                     this.donationBoxOrchestrationService.RetrieveAllDonationBoxes();

                var selectedDonationBox = allDonationBoxes.FirstOrDefault(d =>
                    d.DonationBoxId == d.DonationBoxId);

                if (selectedProductRequest.Status != productRequest.Status &&
                    productRequest.Status == ProductRequestStatusType.Delivered &&
                    client.ProductId == productRequest.ProductId)
                {
                    client.StatusType.Equals(productRequest.Status.ToString());
                    await this.clientService.ModifyClientAsync(client);

                    await this.donationBoxOrchestrationService.ModifyDonationBoxAsync(
                        selectedDonationBox, client.OfferLinkId, outbalance = false);

                    product.NumberSold--;
                    productType.Count--;
                    await this.productService.ModifyProductAsync(product);
                    await this.productTypeService.ModifyProductTypeAsync(productType);

                    return await this.productRequestService.ModifyProductRequestAsync(productRequest);
                }
                else if (selectedProductRequest.Status != productRequest.Status &&
                    productRequest.Status == ProductRequestStatusType.CameBack &&
                    client.ProductId == productRequest.ProductId)
                {
                    client.StatusType.Equals(productRequest.Status.ToString());
                    await this.clientService.ModifyClientAsync(client);

                    await this.donationBoxOrchestrationService.ModifyDonationBoxAsync(
                        selectedDonationBox, client.OfferLinkId, outbalance);

                    product.NumberSold--;
                    productType.Count--;
                    await this.productService.ModifyProductAsync(product);
                    await this.productTypeService.ModifyProductTypeAsync(productType);

                    return await this.productRequestService.ModifyProductRequestAsync(productRequest);
                }
            }

            return await this.productRequestService.ModifyProductRequestAsync(productRequest);
        }

        public async ValueTask<ProductRequest> RemoveProductRequestByIdAsync(Guid id) =>
            await this.productRequestService.RemoveProductRequestAsync(id);
    }
}