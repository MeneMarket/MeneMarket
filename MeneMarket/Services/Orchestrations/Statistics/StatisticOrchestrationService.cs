using MeneMarket.Models.Foundations.ProductRequests;
using MeneMarket.Models.Orchestrations.Statistics;
using MeneMarket.Services.Foundations.ProductRequests;

namespace MeneMarket.Services.Orchestrations.Statistics
{
    public class StatisticOrchestrationService : IStatisticOrchestrationService
    {
        private readonly IProductRequestService productRequestService;

        public StatisticOrchestrationService(IProductRequestService productRequestService) =>
            this.productRequestService = productRequestService;

        public Statistic RetrieveStatistic()
        {
            var productRequests = this.productRequestService.RetrieveAllProductRequests();
            var statistic = new Statistic();
            var currentDate = DateTime.Now.Date;

            for (int i = 0; i < 7; i++)
            {
                var day = currentDate.AddDays(-i);

                var productRequestsCount = productRequests.Count(request => request.ModifiedDate == day);

                var soldProductsCount = productRequests
                    .Count(request => request.Status == ProductRequestStatusType.Delivered && request.ModifiedDate.Date == day);

                var unsoldProductsCount = productRequests
                    .Count(request => request.Status != ProductRequestStatusType.Delivered
                                     && request.Status != ProductRequestStatusType.Spam
                                     && request.ModifiedDate.Date == day);

                statistic.SoldProductsCount.Add(soldProductsCount);
                statistic.SoldOutProductsCount.Add(unsoldProductsCount);
                statistic.productRequestCount.Add(productRequestsCount);
            }

            return statistic;
        }
    }
}