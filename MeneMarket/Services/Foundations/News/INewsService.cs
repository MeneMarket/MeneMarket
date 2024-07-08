namespace MeneMarket.Services.Foundations.News
{
    public interface INewsService
    {
        ValueTask<Models.Foundations.News.News> AddNewsAsync(Models.Foundations.News.News News);
        IQueryable<Models.Foundations.News.News> RetrieveAllNews();
        ValueTask<Models.Foundations.News.News> RetrieveNewsByIdAsync(Guid id);
        ValueTask<Models.Foundations.News.News> DeleteNewsByIdAsync(Guid id);
    }
}