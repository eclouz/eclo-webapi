using AutoMapper;
using Eclo.Domain.Entities.Categories;
using Eclo.Domain.Entities.Discounts;
using Eclo.Domain.Entities.Products;
using Eclo.Persistence.Dtos.Categories;
using Eclo.Persistence.Dtos.Discounts;
using Eclo.Persistence.Dtos.Products;

namespace Eclo.WebApi.Configurations;

public class MapperConfiguration : Profile
{
    public MapperConfiguration()
    {
        CreateMap<CategoryCreateDto, Category>().ReverseMap();
        CreateMap<ProductCreateDto, Product>().ReverseMap();
        CreateMap<DiscountCreateDto, Discount>().ReverseMap();
    }
}