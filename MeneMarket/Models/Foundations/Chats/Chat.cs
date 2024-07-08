using System.Text.Json.Serialization;
using MeneMarket.Models.Foundations.Messages;
using MeneMarket.Models.Foundations.Users;

namespace MeneMarket.Models.Foundations.Chats
{
    public class Chat
    {
        public Guid ChatId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public virtual ICollection<Message> ChatMessages { get; set; }
    }
}