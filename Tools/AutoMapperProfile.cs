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
        CreateMap<Product, ShoppingCartProductDTO>();
        CreateMap<ShoppingCart, ShoppingCartDTO>();
        CreateMap<ShoppingCartLineItem, ShoppingCartLineItemDTO>()
            .ForMember(
                    dest => dest.Product,
                    opt => opt.MapFrom((src, dest, destMember, context) => context.Mapper.Map<ShoppingCartProductDTO>(src.Product)));
    }
}