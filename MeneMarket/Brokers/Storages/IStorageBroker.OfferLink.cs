﻿using MeneMarket.Models.Foundations.OfferLinks;

namespace MeneMarket.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<OfferLink> InsertOfferLinkAsync(OfferLink offerLink);
        IQueryable<OfferLink> SelectAllOfferLinksAsync();
        ValueTask<OfferLink> SelectOfferLinkByIdAsync(Guid id);
        ValueTask<OfferLink> UpdateOfferLinkAsync(OfferLink offerLink);
        ValueTask<OfferLink> DeleteOfferLinkAsync(OfferLink offerLink);
    }
}