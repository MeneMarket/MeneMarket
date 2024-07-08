using MeneMarket.Models.Foundations.Places;

namespace MeneMarket.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Place> InsertPlaceAsync(Place place);
        IQueryable<Place> SelectAllPlaces();
        ValueTask<Place> SelectPlaceByIdAsync(Guid id);
        ValueTask<Place> UpdatePlaceAsync(Place place);
        ValueTask<Place> DeletePlaceAsync(Place place);
    }
}