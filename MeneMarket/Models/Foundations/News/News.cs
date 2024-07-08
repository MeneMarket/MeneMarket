namespace MeneMarket.Models.Foundations.News
{
    public class News
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PostedTime { get; set; }
        public string ImageFilePath { get; set; }
    }
}