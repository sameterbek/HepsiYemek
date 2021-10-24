using AutoMapper;
using HepsiYemek.Dto.Category;
using HepsiYemek.Dto.Product;
using HepsiYemek.Entities.Entites;

namespace HepsiYemek.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, GetCategoryDto>();
            CreateMap<Product, GetProductDto>();
        }
    }
}
