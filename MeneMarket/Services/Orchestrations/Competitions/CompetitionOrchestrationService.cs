using MeneMarket.Models.Foundations.Competitions;
using MeneMarket.Services.Foundations.Competitions;
using MeneMarket.Services.Foundations.OfferLinks;
using MeneMarket.Services.Foundations.Places;
using MeneMarket.Services.Foundations.Users;

namespace MeneMarket.Services.Orchestrations.Competitions
{
    public class CompetitionOrchestrationService : ICompetitionOrchestrationService
    {
        private readonly ICompetitionService competitionService;
        private readonly IOfferLinkService offerLinkService;
        private readonly IPlaceService placeService;
        private readonly IUserService userService;

        public CompetitionOrchestrationService(
            IUserService userService,
            ICompetitionService competitionService, 
            IOfferLinkService offerLinkService, 
            IPlaceService placeService)
        {
            this.competitionService = competitionService;
            this.offerLinkService = offerLinkService;
            this.placeService = placeService;
            this.userService = userService;
        }

        public async ValueTask<Competition> AddCompetitionAsync(Competition competition)
        {
            competition.CompetitionId = Guid.NewGuid();

            if(competition.Places is null)
                throw new ArgumentNullException(nameof(competition.Places));

            foreach (var place in competition.Places)
                await this.placeService.AddPlaceAsync(place);

            return await this.competitionService.AddCompetitionAsync(competition);
        }

        public async ValueTask<Competition> ModifyCompetitionAsync(Competition competition) =>
            await this.competitionService.ModifyCompetitionAsync(competition);

        public async ValueTask<Competition> RemoveCompetitionAsync(Guid id)
        {
            var competition = await this.competitionService.RetrieveCompetitionByIdAsync(id);

            return await this.competitionService.RemoveCompetitionAsync(competition);
        }

        public IQueryable<Competition> RetrieveAllCompetitions() =>
            this.competitionService.RetrieveAllCompetitions();

        public async ValueTask<Competition> RetrieveCompetitionByIdAsync(Guid id)
        {
            var competition = await this.competitionService.RetrieveCompetitionByIdAsync(id);

            var offerLinks = this.offerLinkService.RetrieveAllOfferLinks().Where(o => 
            o.ProductId == competition.ProductId);

            var user = this.userService.RetrieveAllUsers();

            foreach (var offerLink in offerLinks)
            {

            }

            return await this.competitionService.RetrieveCompetitionByIdAsync(id);
        }
    }
}