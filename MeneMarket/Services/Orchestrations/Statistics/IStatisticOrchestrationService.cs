using MeneMarket.Models.Orchestrations.Statistics;

namespace MeneMarket.Services.Orchestrations.Statistics
{
    public interface IStatisticOrchestrationService
    {
        public Statistic RetrieveStatistic();
    }
}