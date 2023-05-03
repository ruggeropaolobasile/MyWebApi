namespace MyWebApi.model
{
    public class JobOffer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}