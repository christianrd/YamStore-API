using System;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Application.BasketItem.Command.Add
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
            var item = _mapper.Map<Domain.Entities.BasketItem>(request);
            var items = await _unitOfWork.BasketItems.Get(x => x.BasketId == request.BasketId && x.ProductId == request.ProductId).FirstOrDefaultAsync();
            if (items == null)
                await _unitOfWork.BasketItems.Insert(item);
            else
            {
                items.Quantity += request.Quantity;
                await _unitOfWork.BasketItems.Update(items);
            }

            return await _unitOfWork.BasketItems.CommitChanges();
        }
    }
}