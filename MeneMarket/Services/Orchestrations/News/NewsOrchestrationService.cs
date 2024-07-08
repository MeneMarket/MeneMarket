using MeneMarket.Services.Foundations.News;
using MeneMarket.Services.Processings.Files;

namespace MeneMarket.Services.Orchestrations.News
{
    public class NewsOrchestrationService : INewsOrchestrationService
    {
        private readonly INewsService newsService;
        private readonly IFileProcessingService fileProcessingService;

        public NewsOrchestrationService(
            INewsService newsService,
            IFileProcessingService fileProcessingService)
        {
            this.newsService = newsService;
            this.fileProcessingService = fileProcessingService;
        }

        public async ValueTask<Models.Foundations.News.News> AddNewsAsync(
            Models.Foundations.News.News news,
            string bytes64String)
        {
            string fileName = Guid.NewGuid().ToString() + ".WebP";
            byte[] bytes = Convert.FromBase64String(bytes64String);
            var memoryStream = ConvertBytesToMemoryStream(bytes);

            string filePath =
                await this.fileProcessingService.UploadFileAsync(memoryStream, fileName, 5);

            news.ImageFilePath = filePath;
            news.PostedTime = DateTime.Now;

            return await this.newsService.AddNewsAsync(news);
        }

        public async ValueTask<Models.Foundations.News.News> RemoveNewsAsync(Guid id)
        {
            var news = await this.newsService.RetrieveNewsByIdAsync(id);
            List<string> imageFilePaths = new List<string>();
            imageFilePaths.Add(news.ImageFilePath);
            this.fileProcessingService.DeleteImageFile(imageFilePaths);

            return await this.newsService.DeleteNewsByIdAsync(id);
        }

        public IQueryable<Models.Foundations.News.News> RetrieveAllNews() =>
            this.newsService.RetrieveAllNews();

        public async ValueTask<Models.Foundations.News.News> RetrieveNewsByIdAsync(Guid id) =>
            await this.newsService.RetrieveNewsByIdAsync(id);

        private MemoryStream ConvertBytesToMemoryStream(byte[] bytes)
        {
            MemoryStream memoryStream = new MemoryStream(bytes);
            return memoryStream;
        }
    }
}