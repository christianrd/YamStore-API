using System.Threading;
using System.Threading.Tasks;
using API.Core.Application.Common.Persistence;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Product.Commands.Create
{
    public class CreateProductCommand : IRequest<Domain.Entities.Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string PictureUrl { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int QuantityInStock { get; set; }
    }
    
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Domain.Entities.Product>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Domain.Entities.Product>(request);
            await _unitOfWork.Product.Insert(product);
            var result= await _unitOfWork.Product.CommitChanges();
            
            if (result > 0)
                return product;

            return null;
        }
    }
}