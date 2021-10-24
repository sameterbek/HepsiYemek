using HepsiYemek.Dto.Category;

namespace HepsiYemek.Dto.Product
{
    public class GetProductDto : DocumentDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public string categoryId { get; set; }
        public GetCategoryDto Category { get; set; }
        public decimal price { get; set; }
        public string currency { get; set; }
    }
}
