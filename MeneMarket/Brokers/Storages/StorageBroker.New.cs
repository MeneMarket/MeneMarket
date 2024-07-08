using MeneMarket.Models.Foundations.News;
using Microsoft.EntityFrameworkCore;

namespace MeneMarket.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<News> News { get; set; }

        public async ValueTask<News> InsertNewsAsync(News news) =>
            await InsertAsync(news);

        public IQueryable<News> SelectAllNews() =>
            SelectAll<News>();

        public async ValueTask<News> SelectNewsByIdAsync(Guid id) =>
            await SelectAsync<News>(id);

        public async ValueTask<News> DeleteNewsAsync(News news) =>
            await DeleteAsync(news);
    }
}