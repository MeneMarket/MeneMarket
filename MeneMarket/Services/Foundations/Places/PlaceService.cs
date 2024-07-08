using MeneMarket.Brokers.Storages;
using MeneMarket.Models.Foundations.Places;

namespace MeneMarket.Services.Foundations.Places
{
    public class PlaceService : IPlaceService
    {
        private readonly IStorageBroker storageBroker;

        public PlaceService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Place> AddPlaceAsync(Place place)
        {
            place.Id = Guid.NewGuid();
            return await this.AddPlaceAsync(place);
        }

        public IQueryable<Place> RetrieveAllPlaces() =>
            this.storageBroker.SelectAllPlaces();

        public async ValueTask<Place> RetrievePlaceByIdAsync(Guid id) =>
            await this.storageBroker.SelectPlaceByIdAsync(id);

        public async ValueTask<Place> ModifyPlaceAsync(Place place) =>
            await this.storageBroker.UpdatePlaceAsync(place);

        public async ValueTask<Place> RemovePlaceByIdAsync(Guid id)
        {
            var place = await this.storageBroker.SelectPlaceByIdAsync(id);

            return await this.storageBroker.DeletePlaceAsync(place);
        }
    }
}