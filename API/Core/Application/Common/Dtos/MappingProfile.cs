using API.Core.Application.Basket.Commands.Create;
using API.Core.Application.BasketItem.Command.Add;
using API.Core.Application.Product.Commands.Create;
using AutoMapper;

namespace API.Core.Application.Common.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductCommand, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, ProductDto>();
            CreateMap<CreateBasketCommand, Domain.Entities.Basket>();
            CreateMap<Domain.Entities.Basket, BasketDto>();
            CreateMap<AddItemBasketCommand, Domain.Entities.BasketItem>();
            CreateMap<Domain.Entities.BasketItem, BasketItemDto>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.PictureUrl, opts => opts.MapFrom(src => src.Product.PictureUrl))
                .ForMember(dest => dest.Brand, opts => opts.MapFrom(src => src.Product.Brand))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Product.Type));
        }
    }
}