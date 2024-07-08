using MeneMarket.Models.Foundations.Users;

namespace MeneMarket.Models.Orchestrations.UsersWithCountss
{
    public class UsersWithCount
    {
        public IQueryable<User> Users { get; set; }
        public ulong UsersCount { get; set; }
    }
}