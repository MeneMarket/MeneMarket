namespace MeneMarket.Services.Orchestrations.News
{
    public interface INewsOrchestrationService
    {
        ValueTask<Models.Foundations.News.News> AddNewsAsync(Models.Foundations.News.News news, string bytes64String);
        IQueryable<Models.Foundations.News.News> RetrieveAllNews();
        ValueTask<Models.Foundations.News.News> RetrieveNewsByIdAsync(Guid id);
        ValueTask<Models.Foundations.News.News> RemoveNewsAsync(Guid id);
    }
}