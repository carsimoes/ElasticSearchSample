namespace ElasticSearchSample.Models
{
    public class Costumer : BaseDocument
    {
        public string? Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Email { get; set; }
        public string? Document { get; set; }
        public string? Address { get; set; }
        public int Number { get; set; }
        public string? District { get; set; }
        public string? Zipcode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }       
    }
}
