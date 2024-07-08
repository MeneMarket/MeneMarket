using MeneMarket.Models.Foundations.Products;

namespace MeneMarket.Models.Orchestrations.ProductWithCount
{
    public class ProductsWithCount
    {
        public IQueryable<Product> allProducts {  get; set; }
        public ulong ProductsCount { get; set; }
    }
}