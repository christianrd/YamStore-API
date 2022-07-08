using System;
using System.Collections.Generic;

namespace API.Core.Application.Common.Dtos
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}