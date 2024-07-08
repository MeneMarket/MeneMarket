using MeneMarket.Models.Foundations.Chats;

namespace MeneMarket.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Chat> InsertChatAsync(Chat chat);
        IQueryable<Chat> SelectAllChats();
        ValueTask<Chat> SelectChatByIdAsync(Guid id);
        ValueTask<Chat> UpdateChatAsync(Chat chat);
        ValueTask<Chat> DeleteChatAsync(Chat chat);
    }
}
