using MeneMarket.Brokers.Storages;
using MeneMarket.Models.Foundations.Chats;
using MeneMarket.Models.Orchestrations.ChatsWithCounts;

namespace MeneMarket.Services.Foundations.Chats
{
    public class ChatService : IChatService
    {
        private readonly IStorageBroker storageBroker;

        public ChatService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Chat> AddChatAsync(Chat chat) =>
            await this.storageBroker.InsertChatAsync(chat);

        public ChatsWithCount RetrieveAllChats(int pageNumber)
        {
            int pageSize = 5;
            int usersToSkip = (pageNumber - 1) * pageSize;

            IQueryable<Chat> allChats = this.storageBroker.SelectAllChats();
            IQueryable<Chat> chats = this.storageBroker.SelectAllChats()
                                                        .Skip(usersToSkip)
                                                        .Take(pageSize);

            return new ChatsWithCount
            {
                Chats = chats,
                ChatsCount = (ulong)allChats.Count()
            };
        }

        public async ValueTask<Chat> RetrieveChatByIdAsync(Guid id) =>
            await this.storageBroker.SelectChatByIdAsync(id);

        public async ValueTask<Chat> RemoveChatAsync(Guid id)
        {
            var chat = await this.storageBroker.SelectChatByIdAsync(id);

            return await this.storageBroker.DeleteChatAsync(chat);
        }
    }
}
