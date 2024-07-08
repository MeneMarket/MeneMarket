using MeneMarket.Models.Foundations.Competitions;
using Microsoft.EntityFrameworkCore;

namespace MeneMarket.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Competition> Competitions { get; set; }

        public async ValueTask<Competition> InsertCompetitionAsync(Competition competition) =>
            await InsertAsync(competition);

        public IQueryable<Competition> SelectAllCompetitions() =>
                this.Competitions
                .Include(p => p.Places)
                .AsQueryable();

        public async ValueTask<Competition> SelectCompetitionByIdAsync(Guid id) =>
                await this.Competitions
                .Include(p => p.Places)
               .FirstOrDefaultAsync(p => p.CompetitionId == id);

        public async ValueTask<Competition> UpdateCompetitionAsync(Competition competition) =>
            await UpdateAsync(competition);

        public async ValueTask<Competition> DeleteCompetitionAsync(Competition competition) =>
            await DeleteAsync(competition);
    }
}