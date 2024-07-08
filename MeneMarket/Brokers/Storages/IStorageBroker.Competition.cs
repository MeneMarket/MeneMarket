using MeneMarket.Models.Foundations.Competitions;

namespace MeneMarket.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Competition> InsertCompetitionAsync(Competition competition);
        IQueryable<Competition> SelectAllCompetitions();
        ValueTask<Competition> SelectCompetitionByIdAsync(Guid id);
        ValueTask<Competition> UpdateCompetitionAsync(Competition competition);
        ValueTask<Competition> DeleteCompetitionAsync(Competition competition);
    }
}