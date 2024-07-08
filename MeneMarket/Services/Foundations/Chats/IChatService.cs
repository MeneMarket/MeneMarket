using MeneMarket.Models.Foundations.Chats;
using MeneMarket.Models.Orchestrations.ChatsWithCounts;

namespace MeneMarket.Services.Foundations.Chats
{
    public interface IChatService
    {
        ValueTask<Chat> AddChatAsync(Chat chat);
        ChatsWithCount RetrieveAllChats(int pageNumber);
        ValueTask<Chat> RetrieveChatByIdAsync(Guid id);
        ValueTask<Chat> RemoveChatAsync(Guid id);
    }
}