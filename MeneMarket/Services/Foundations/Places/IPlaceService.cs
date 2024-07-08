using MeneMarket.Models.Foundations.Places;

namespace MeneMarket.Services.Foundations.Places
{
    public interface IPlaceService
    {
        ValueTask<Place> AddPlaceAsync(Place place);
        IQueryable<Place> RetrieveAllPlaces();
        ValueTask<Place> RetrievePlaceByIdAsync(Guid id);
        ValueTask<Place> ModifyPlaceAsync(Place place);
        ValueTask<Place> RemovePlaceByIdAsync(Guid id);
    }
}