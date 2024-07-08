using MeneMarket.Models.Foundations.Competitions;

namespace MeneMarket.Services.Orchestrations.Competitions
{
    public interface ICompetitionOrchestrationService
    {
        ValueTask<Competition> AddCompetitionAsync(Competition competition);
        IQueryable<Competition> RetrieveAllCompetitions();
        ValueTask<Competition> RetrieveCompetitionByIdAsync(Guid id);
        ValueTask<Competition> ModifyCompetitionAsync(Competition competition);
        ValueTask<Competition> RemoveCompetitionAsync(Guid id);
    }
}