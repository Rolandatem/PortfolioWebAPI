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
        CreateMap<Category, CategoryDTO>();
        CreateMap<SiteFilterTagType, SiteFilterTagTypeDTO>();
        CreateMap<ProductHighlight, ProductHighlightDTO>();
        CreateMap<ImageType, ImageTypeDTO>();
        CreateMap<ProductImage, ProductImageDTO>();
        CreateMap<Product, ProductDTO>();
        CreateMap<ShoppingCart, ShoppingCartDTO>();
        CreateMap<ShoppingCartLineItem, ShoppingCartLineItemDTO>();
    }
}