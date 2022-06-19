using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Dtos;
using API.Core.Application.Common.Persistence;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Product.Queries
{
    public class GetAllProductQuery : IRequest<List<ProductDto>>
    {
    }
    
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<ProductDto>>(await _unitOfWork.Product.GetAllAsync());
        }
    }
}