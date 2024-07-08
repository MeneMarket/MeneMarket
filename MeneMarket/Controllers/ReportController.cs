using MeneMarket.Models.Foundations.Reports;
using MeneMarket.Services.Foundations.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : RESTFulController
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService) =>
            this.reportService = reportService;

        [HttpPost]
        public async ValueTask<ActionResult<Report>> AddReportAsync(Report report) =>
            await this.reportService.AddReportAsync(report);

        [HttpGet]
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult<IQueryable<Report>> GetAllReports()
        {
            var allReports = this.reportService.RetrieveAllReports();

            return Ok(allReports);
        }

        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public async ValueTask<ActionResult<Report>> DeleteReportAsync(Guid id) =>
            await this.reportService.RemoveReportAsync(id);
    }
}