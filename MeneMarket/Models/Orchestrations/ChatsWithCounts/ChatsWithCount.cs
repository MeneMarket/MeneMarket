using MeneMarket.Models.Foundations.Chats;

namespace MeneMarket.Models.Orchestrations.ChatsWithCounts
{
    public class ChatsWithCount
    {
        public IQueryable<Chat> Chats { get; set; }
        public ulong ChatsCount { get; set; }
    }
}
