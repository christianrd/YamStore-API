using System;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Application.BasketItem.Command.Remove
{
    public class RemoveItemCommand : IRequest<int>
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.BasketItems
                .Get(x => x.BasketId == request.BasketId && x.ProductId == request.ProductId).FirstOrDefaultAsync();
            if (item == null) return 0;

            item.Quantity -= request.Quantity;
            if (item.Quantity == 0)
                await _unitOfWork.BasketItems.Delete(item);
            else
                await _unitOfWork.BasketItems.Update(item);

            return await _unitOfWork.BasketItems.CommitChanges();
        }
    }
}