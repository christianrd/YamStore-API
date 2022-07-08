using System;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Dtos;
using API.Core.Application.Common.Persistence;
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
        private readonly IUnitOfWork _unitOfWork;

        public GetBasketQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            /*var basket = await _context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == request.BuyerId, cancellationToken: cancellationToken);*/

            var basket = await _unitOfWork.Baskets.Get(x => x.BuyerId == request.BuyerId, "Items,Items.Product")
                .FirstOrDefaultAsync();
            return _mapper.Map<BasketDto>(basket);
        }
    }
}