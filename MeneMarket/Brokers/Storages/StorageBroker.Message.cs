using MeneMarket.Models.Foundations.Messages;
using Microsoft.EntityFrameworkCore;

namespace MeneMarket.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Message> Messages { get; set; }

        public async ValueTask<Message> InsertMessageAsync(Message message) =>
            await InsertAsync(message);
    }
}