using MeneMarket.Models.Foundations.Messages;

namespace MeneMarket.Services.Foundations.Messages
{
    public interface IMessageService
    {
        ValueTask<Message> AddMessageAsync(Message message);
    }
}