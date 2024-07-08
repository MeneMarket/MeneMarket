using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MeneMarket.Models.Foundations.Competitions;

namespace MeneMarket.Models.Foundations.Places
{
    public class Place
    {
        public Guid Id { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public ushort PlaceNumber {  get; set; }
        public Guid CompetitionId { get; set; }
        [JsonIgnore]
        public virtual Competition Competition { get; set; }
    }
}