using System.Text.Json.Serialization;
using MeneMarket.Models.Foundations.Chats;
using MeneMarket.Models.Foundations.Users;

namespace MeneMarket.Models.Foundations.Messages
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset MessagedDate { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User MessagedUser { get; set; }
        public Guid ChatId { get; set; }
        [JsonIgnore]
        public virtual Chat Chat { get; set; }
    }
}
