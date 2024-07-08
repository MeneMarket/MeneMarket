using MeneMarket.Models.Foundations.Competitions;
using MeneMarket.Models.Foundations.Users;

namespace MeneMarket.Models.Orchestrations.CompetitionsWithOfferLinks
{
    public class CompetitionWithOfferLinks
    {
        public Competition competition {  get; set; }
        public IQueryable<User> users { get; set; }
    }
}