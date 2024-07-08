using MeneMarket.Models.Orchestrations.Statistics;
using MeneMarket.Services.Orchestrations.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : RESTFulController
    {
        private readonly IStatisticOrchestrationService statisticOrchestrationService;

        public StatisticsController(IStatisticOrchestrationService statisticOrchestrationService) =>
            this.statisticOrchestrationService = statisticOrchestrationService;

        [HttpGet]
        [Authorize(Roles = "User,Admin,Owner")]
        public Statistic GetStatistics() =>
            this.statisticOrchestrationService.RetrieveStatistic();
    }
}