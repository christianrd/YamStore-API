using System;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Dtos;
using API.Core.Application.Common.Persistence;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Product.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }
    
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ProductDto>(await _unitOfWork.Product.GetById(request.Id));
        }
    }
}