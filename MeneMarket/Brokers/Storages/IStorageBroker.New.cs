﻿using MeneMarket.Models.Foundations.News;

namespace MeneMarket.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<News> InsertNewsAsync(News news);
        IQueryable<News> SelectAllNews();
        ValueTask<News> SelectNewsByIdAsync(Guid id);
        ValueTask<News> DeleteNewsAsync(News news);
    }
}