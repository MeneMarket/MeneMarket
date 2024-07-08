using MeneMarket.Models.Foundations.Products;
using MeneMarket.Models.Orchestrations.CombinedProducts;
using MeneMarket.Models.Orchestrations.ProductWithCount;

namespace MeneMarket.Services.Orchestrations.Products
{
    public interface IProductOrchestrationService
    {
        ValueTask<Product> AddProductAsync(Product product, List<string> bytes);
        CombinedProducts RetrieveFilteredProducts(string userRole);
        ValueTask<Product> ArchiveAndUnArchiveProduct(Guid id);
        ProductsWithCount RetrieveAllProducts(string userRole, int pageNumber);
        ValueTask<Product> RetrieveProductByIdAsync(Guid id);
        ValueTask<Product> ModifyProductAsync(Product product, List<string> bytes64String);
        ValueTask<Product> RemoveProductAsync(Guid id);
    }
}