using MeneMarket.Models.Foundations.Messages;

namespace MeneMarket.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Message> InsertMessageAsync(Message message);
    }
}