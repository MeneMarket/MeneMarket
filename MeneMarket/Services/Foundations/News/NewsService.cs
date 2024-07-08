using MeneMarket.Brokers.Storages;

namespace MeneMarket.Services.Foundations.News
{
    public class NewsService : INewsService
    {
        private readonly IStorageBroker storageBroker;

        public NewsService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Models.Foundations.News.News> AddNewsAsync(Models.Foundations.News.News News)
        {
            News.PostedTime = DateTime.Now;
            return await this.storageBroker.InsertNewsAsync(News);
        }

        public IQueryable<Models.Foundations.News.News> RetrieveAllNews() =>
            this.storageBroker.SelectAllNews();

        public async ValueTask<Models.Foundations.News.News> RetrieveNewsByIdAsync(Guid id) =>
            await this.storageBroker.SelectNewsByIdAsync(id);

        public async ValueTask<Models.Foundations.News.News> DeleteNewsByIdAsync(Guid id)
        {
            var news = await this.storageBroker.SelectNewsByIdAsync(id);

            return await this.storageBroker.DeleteNewsAsync(news);
        }
    }
}