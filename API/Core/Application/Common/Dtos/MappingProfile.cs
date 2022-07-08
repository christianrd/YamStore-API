using API.Core.Application.Basket.Commands.Create;
using API.Core.Application.Basket.Commands.Update;
using API.Core.Application.Product.Commands.Create;
using API.Core.Domain.Entities;
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
            CreateMap<Domain.Entities.Basket, BasketDto>().ReverseMap();
            CreateMap<AddItemBasketCommand, BasketItem>();
            CreateMap<BasketItem, BasketItemDto>();
        }
    }
}