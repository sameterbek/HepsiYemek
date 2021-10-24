namespace HepsiYemek.Dto.Product
{
    public class CreateProductDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public string categoryId { get; set; }
        public decimal? price { get; set; }
        public string currency { get; set; }
    }
}
