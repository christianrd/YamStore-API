using System;

namespace API.Core.Application.Common.Dtos
{
    public class BasketItemDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        
        public Guid BasketId { get; set; }
        public BasketDto Basket { get; set; }
    }
}