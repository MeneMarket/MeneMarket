using MeneMarket.Models.Foundations.ProductRequests;

namespace MeneMarket.Models.Orchestrations.RequestsWithCounts
{
    public class RequestsWithCount
    {
        public IQueryable<ProductRequest> AllProductRequests { get; set; }
        public ulong ProductRequestsCunt { get; set; }
    }
}