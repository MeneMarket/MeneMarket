using MeneMarket.Models.Foundations.Chats;
using Microsoft.EntityFrameworkCore;

namespace MeneMarket.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Chat> Chats { get; set; }

        public async ValueTask<Chat> InsertChatAsync(Chat chat) =>
            await InsertAsync(chat);

        public IQueryable<Chat> SelectAllChats()
        {
            return this.Chats
                .Include(p => p.ChatMessages)
                .AsQueryable();
        }

        public async ValueTask<Chat> SelectChatByIdAsync(Guid id)
        {
            return await this.Chats
                .Include(u => u.ChatMessages)
                    .FirstOrDefaultAsync(u => u.ChatId == id);
        }

        public async ValueTask<Chat> UpdateChatAsync(Chat chat) =>
            await UpdateAsync(chat);

        public async ValueTask<Chat> DeleteChatAsync(Chat chat) =>
            await DeleteAsync(chat);
    }
}
