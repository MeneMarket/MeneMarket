using MeneMarket.Brokers.Storages;
using MeneMarket.Models.Foundations.Competitions;

namespace MeneMarket.Services.Foundations.Competitions
{
    public class CompetitionService : ICompetitionService
    {
        private readonly IStorageBroker storageBroker;

        public CompetitionService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Competition> AddCompetitionAsync(Competition competition) =>
            await this.storageBroker.InsertCompetitionAsync(competition);

        public async ValueTask<Competition> ModifyCompetitionAsync(Competition competition) =>
            await this.storageBroker.UpdateCompetitionAsync(competition);

        public async ValueTask<Competition> RemoveCompetitionAsync(Competition competition) =>
            await this.storageBroker.DeleteCompetitionAsync(competition);

        public IQueryable<Competition> RetrieveAllCompetitions() =>
            this.storageBroker.SelectAllCompetitions();

        public async ValueTask<Competition> RetrieveCompetitionByIdAsync(Guid id) =>
            await this.storageBroker.SelectCompetitionByIdAsync(id);
    }
}