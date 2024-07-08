using MeneMarket.Models.Foundations.ProductRequests;
using MeneMarket.Models.Orchestrations.RequestsWithCounts;

namespace MeneMarket.Services.Orchestrations.ProductRequests
{
    public interface IProductRequestOrchestrationService
    {
        ValueTask<ProductRequest> AddProductRequestAsync(ProductRequest productRequest);
        RequestsWithCount RetrieveAllProductRequests(int pageNumber);
        ValueTask<ProductRequest> RetrieveProductRequestByIdAsync(Guid id);
        ValueTask<ProductRequest> ModifyProductRequestAsync(ProductRequest productRequest);
        ValueTask<ProductRequest> RemoveProductRequestByIdAsync(Guid id);
    }
}