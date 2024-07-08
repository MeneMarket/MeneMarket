using MeneMarket.Models.Foundations.Places;
using Microsoft.EntityFrameworkCore;

namespace MeneMarket.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Place> Places { get; set; }

        public async ValueTask<Place> InsertPlaceAsync(Place place) =>
            await InsertAsync(place);

        public IQueryable<Place> SelectAllPlaces() =>
            SelectAll<Place>();

        public async ValueTask<Place> SelectPlaceByIdAsync(Guid id) =>
            await SelectAsync<Place>(id);

        public async ValueTask<Place> UpdatePlaceAsync(Place place) =>
            await UpdateAsync(place);

        public async ValueTask<Place> DeletePlaceAsync(Place place) =>
            await DeleteAsync(place);
    }
}
