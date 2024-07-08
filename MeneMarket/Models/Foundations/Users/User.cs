﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MeneMarket.Models.Foundations.BalanceHistorys;
using MeneMarket.Models.Foundations.Chats;
using MeneMarket.Models.Foundations.DonatedUsers;
using MeneMarket.Models.Foundations.Messages;
using MeneMarket.Models.Foundations.OfferLinks;

namespace MeneMarket.Models.Foundations.Users
{
    public class User
    {
        [Required]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public ushort ProductsSold { get; set; }
        [JsonIgnore]
        public ulong InvitedUsersCount { get; set; }
        public bool IsArchived { get; set; }
        [Required]
        public Role Role { get; set; }
        public string Image { get; set; }
        public virtual ICollection<OfferLink> OfferLinks { get; set; }
        public virtual ICollection<BalanceHistory> BalanceHistorys { get; set; }
        [JsonIgnore]
        public virtual ICollection<DonatedUser> DonatedUsers { get; set; }
        public virtual ICollection<Chat> UserChats { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> UserMessages { get; set; }
    }
}