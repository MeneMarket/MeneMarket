using MeneMarket.Models.Foundations.Competitions;

namespace MeneMarket.Services.Foundations.Competitions
{
    public interface ICompetitionService
    {
        ValueTask<Competition> AddCompetitionAsync(Competition competition);
        IQueryable<Competition> RetrieveAllCompetitions();
        ValueTask<Competition> RetrieveCompetitionByIdAsync(Guid id);
        ValueTask<Competition> ModifyCompetitionAsync(Competition competition);
        ValueTask<Competition> RemoveCompetitionAsync(Competition competition);
    }
}