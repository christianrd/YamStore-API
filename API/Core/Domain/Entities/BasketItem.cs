using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Core.Domain.Entities
{
    [Table("BasketItems")]
    public class BasketItem : Entity
    {
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}