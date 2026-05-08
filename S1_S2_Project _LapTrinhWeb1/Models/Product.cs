namespace S1_S2_Project__LapTrinhWeb1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Detail { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsTrendingProduct { get; set; }
    }
}
