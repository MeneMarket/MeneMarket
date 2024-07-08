using MeneMarket.Models.Orchestrations.NewsWithImages;
using MeneMarket.Services.Orchestrations.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : RESTFulController
    {
        private readonly INewsOrchestrationService newsOrchestrationService;

        public NewsController(INewsOrchestrationService newsOrchestrationService) =>
            this.newsOrchestrationService = newsOrchestrationService;

        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        public async ValueTask<ActionResult<Models.Foundations.News.News>> PostNewsAsync(NewsWithImage newsWithImage)
        {
            try
            {
                var addedNews = await this.newsOrchestrationService.AddNewsAsync(newsWithImage.News, newsWithImage.bytes);
                return addedNews;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex}");

                return BadRequest($"An error occurred while processing the request.{ex}");
            }
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin,Owner")]
        public ActionResult<IQueryable<Models.Foundations.News.News>> GetAllNews()
        {
            var AllNews = this.newsOrchestrationService.RetrieveAllNews();

            return Ok(AllNews);
        }

        [HttpGet]
        [Route("ById")]
        [Authorize(Roles = "User,Admin,Owner")]
        public async ValueTask<ActionResult<Models.Foundations.News.News>> GetNewsByIdAsync(Guid id) =>
            await this.newsOrchestrationService.RetrieveNewsByIdAsync(id);

        [HttpDelete]
        [Authorize(Roles = "Admin,Owner")]
        public async ValueTask<ActionResult<Models.Foundations.News.News>> DeleteNewsAsync(Guid id) =>
            await this.newsOrchestrationService.RemoveNewsAsync(id);
    }
}