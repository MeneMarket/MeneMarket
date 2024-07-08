using System.Text.Json.Serialization;
using MeneMarket.Models.Foundations.Places;
using MeneMarket.Models.Foundations.Products;

namespace MeneMarket.Models.Foundations.Competitions
{
    public class Competition
    {
        public Guid CompetitionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ulong SoldedCount { get; set; }
        public string Image { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime StopedDate { get; set;}
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual ICollection<Place> Places { get; set; }
    }
}