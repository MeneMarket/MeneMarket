using MeneMarket.Models.Foundations.DonationBoxes;

namespace MeneMarket.Services.Processings.DonationBoxes
{
    public interface IDonationBoxProcessingService
    {
        ValueTask<DonationBox> AddDonationBoxAsync(DonationBox donationBox);
        IQueryable<DonationBox> RetrieveAllDonationBoxs();
        ValueTask<DonationBox> RetrieveDonationBoxByIdAsync(Guid id);
        ValueTask<DonationBox> ModifyDonationBoxAsync(DonationBox donationBox);
        ValueTask<DonationBox> RemoveDonationBoxByIdAsync(Guid id);
    }
}