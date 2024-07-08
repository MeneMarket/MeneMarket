using System.ComponentModel.DataAnnotations;

namespace MeneMarket.Models.Orchestrations.LoginUsers
{
    public class LoginUser
    {
        [EmailAddress]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}