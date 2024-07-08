using System.Text.Json.Serialization;
using MeneMarket.Models.Foundations.Users;

namespace MeneMarket.Models.Foundations.PaymentRequests
{
    public class PaymentRequest
    {
        public Guid Id { get; set; }
        public ulong Amount { get; set; }
        public ulong CardNumber { get; set; }
        public bool IsPayable { get; set; }
        public string UserImage { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}