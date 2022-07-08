using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Dtos;
using API.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Application.Basket.Queries
{
    public class GetBasketQuery : IRequest<BasketDto>
    {
        public Guid BuyerId { get; set; }
    }

    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDto>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public GetBasketQueryHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<BasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == request.BuyerId, cancellationToken: cancellationToken);

            return _mapper.Map<BasketDto>(basket);
        }
    }
}