using AutoMapper;
using PortfolioWebAPI.Data.DTOs;
using PortfolioWebAPI.Data.Models;

namespace PortfolioWebAPI.Tools;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TagType, TagTypeDTO>();
        CreateMap<Tag, TagDTO>();
        CreateMap<ProductTag, ProductTagDTO>();
        CreateMap<Product, ProductDTO>();
        CreateMap<Category, CategoryDTO>();
        CreateMap<SiteFilterTagType, SiteFilterTagTypeDTO>();
    }
}