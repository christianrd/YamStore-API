using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Dtos;
using API.Core.Application.Common.Persistence;
using AutoMapper;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Application.Product.Queries
{
    public class GetAllProductQuery : IRequest<List<ProductDto>>
    {
        public string OrderBy { get; set; }
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
            var query = _unitOfWork.Product.Get(x => x.Deleted == false).Sort(request.OrderBy);

            
            return _mapper.Map<List<ProductDto>>(await query.ToListAsync());
        }
    }
}