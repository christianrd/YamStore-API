using System;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Dtos;
using API.Core.Application.Common.Persistence;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Basket.Commands.Create
{
    public class CreateBasketCommand : IRequest<BasketDto>
    {
        public Guid BuyerId { get; set; }
    }

    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, BasketDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBasketCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BasketDto> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = _mapper.Map<Domain.Entities.Basket>(request);
            basket.Id = Guid.NewGuid();
            await _unitOfWork.Baskets.Insert(basket);
            var result = await _unitOfWork.Baskets.CommitChanges();

            if (result > 0)
                return _mapper.Map<BasketDto>(basket);

            return null;
        }
    }
}