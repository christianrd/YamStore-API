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
        }
    }
}