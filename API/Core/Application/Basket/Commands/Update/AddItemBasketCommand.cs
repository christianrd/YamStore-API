using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Persistence;
using API.Core.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Application.Basket.Commands.Update
{
    public class AddItemBasketCommand : IRequest<int>
    {
        public Guid ProductId { get; set; }
        public Guid BasketId { get; set; }
        public int Quantity { get; set; }
    }
    
    public class AddItemBasketCommandHandler : IRequestHandler<AddItemBasketCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddItemBasketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddItemBasketCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<BasketItem>(request);
            var items = await _unitOfWork.BasketItems.Get(x => x.BasketId == request.BasketId && x.ProductId == request.ProductId).ToListAsync();
            if (items.Count == 0)
                await _unitOfWork.BasketItems.Insert(item);
            
            var existingItem = await _unitOfWork.BasketItems.Get(x => x.ProductId == request.ProductId).FirstOrDefaultAsync();
            if (existingItem != null) existingItem.Quantity += request.Quantity;

            return await _unitOfWork.BasketItems.CommitChanges();
        }
    }
}