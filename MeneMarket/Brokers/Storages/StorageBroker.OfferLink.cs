using MeneMarket.Models.Foundations.OfferLinks;
using Microsoft.EntityFrameworkCore;

namespace MeneMarket.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<OfferLink> OfferLinks { get; set; }

        public async ValueTask<OfferLink> InsertOfferLinkAsync(OfferLink offerLink) =>
            await InsertAsync(offerLink);

        public IQueryable<OfferLink> SelectAllOfferLinksAsync()
        {
            return this.OfferLinks
                .Include(p => p.Clients)
                .AsQueryable();
        }

        public async ValueTask<OfferLink> SelectOfferLinkByIdAsync(Guid id) =>
            await SelectAsync<OfferLink>(id);

        public async ValueTask<OfferLink> UpdateOfferLinkAsync(OfferLink offerLink) =>
            await UpdateAsync(offerLink);

        public async ValueTask<OfferLink> DeleteOfferLinkAsync(OfferLink offerLink) =>
            await DeleteAsync(offerLink);
    }
}
