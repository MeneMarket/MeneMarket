using MeneMarket.Brokers.Storages;
using MeneMarket.Models.Foundations.Messages;

namespace MeneMarket.Services.Foundations.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IStorageBroker storageBroker;

        public MessageService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Message> AddMessageAsync(Message message)
        {
            message.Id = Guid.NewGuid();
            message.MessagedDate = DateTime.Now;

            return await this.storageBroker.InsertMessageAsync(message);
        }
    }
}